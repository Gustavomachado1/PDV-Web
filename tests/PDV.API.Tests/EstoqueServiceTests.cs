using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging.Abstractions;
using PDV.API.Servicos;
using PDV.Application;
using PDV.Domain;
using PDV.Infrastructure;
using Xunit;

namespace PDV.API.Tests;

public class EstoqueServiceTests
{
    [Fact]
    public async Task MovimentacaoManualSoAlteraSaldoAposAprovacaoERegistraAutorizador()
    {
        await using var contexto = await CriarContextoAsync();
        var produto = await CriarProdutoAsync(contexto.Db, "Produto A");

        var movimentoId = await contexto.Estoque.SolicitarMovimentoAsync(
            contexto.Filial.Id,
            new SolicitarMovimentoEstoqueRequest(
                produto.Id, TipoMovimentoEstoque.EntradaManual, 4, null),
            CancellationToken.None);

        Assert.Empty(await contexto.Db.SaldosEstoque.ToListAsync());
        Assert.Equal(SituacaoMovimentoEstoque.AguardandoAutorizacao,
            (await contexto.Db.MovimentosEstoque.SingleAsync()).Situacao);

        await contexto.Estoque.DecidirMovimentoAsync(
            contexto.Filial.Id,
            movimentoId,
            new DecidirMovimentoEstoqueRequest(true, "gerente", "SenhaForte!456"),
            CancellationToken.None);

        var saldo = await contexto.Db.SaldosEstoque.SingleAsync();
        var movimento = await contexto.Db.MovimentosEstoque.SingleAsync();
        var pedido = await contexto.Db.Autorizacoes.SingleAsync();
        Assert.Equal(4, saldo.Quantidade);
        Assert.Equal(0, movimento.SaldoAnterior);
        Assert.Equal(4, movimento.SaldoResultante);
        Assert.Equal(contexto.Autorizador.Id, movimento.AutorizadorId);
        Assert.Equal(ResultadoAutorizacao.Aprovada, pedido.Resultado);
        Assert.Equal(contexto.Autorizador.Id, pedido.AutorizadorId);
        Assert.Contains(await contexto.Db.Auditoria.ToListAsync(),
            x => x.FilialId == contexto.Filial.Id && x.Entidade == nameof(AutorizacaoOperacao));
    }

    [Fact]
    public async Task SaidaAutorizadaPodeGerarSaldoNegativoEAprovacaoPropriaERecusada()
    {
        await using var contexto = await CriarContextoAsync();
        var produto = await CriarProdutoAsync(contexto.Db, "Produto B");
        var movimentoId = await contexto.Estoque.SolicitarMovimentoAsync(
            contexto.Filial.Id,
            new SolicitarMovimentoEstoqueRequest(
                produto.Id, TipoMovimentoEstoque.SaidaManual, 2, "Avaria"),
            CancellationToken.None);

        var erro = await Assert.ThrowsAsync<EstoqueException>(() =>
            contexto.Estoque.DecidirMovimentoAsync(
                contexto.Filial.Id,
                movimentoId,
                new DecidirMovimentoEstoqueRequest(true, "operador", "SenhaForte!123"),
                CancellationToken.None));
        Assert.Equal(403, erro.StatusCode);
        Assert.Empty(await contexto.Db.SaldosEstoque.ToListAsync());

        await contexto.Estoque.DecidirMovimentoAsync(
            contexto.Filial.Id,
            movimentoId,
            new DecidirMovimentoEstoqueRequest(true, "gerente", "SenhaForte!456"),
            CancellationToken.None);

        Assert.Equal(-2, (await contexto.Db.SaldosEstoque.SingleAsync()).Quantidade);
    }

    [Fact]
    public async Task InventarioAplicaDiferencaComJustificativaERegistraSaldoAnterior()
    {
        await using var contexto = await CriarContextoAsync();
        var produto = await CriarProdutoAsync(contexto.Db, "Produto C");
        contexto.Db.SaldosEstoque.Add(new SaldoEstoque
        {
            EmpresaId = contexto.Empresa.Id,
            FilialId = contexto.Filial.Id,
            ProdutoId = produto.Id,
            Quantidade = 8
        });
        await contexto.Db.SaveChangesAsync();

        var semJustificativa = await Assert.ThrowsAsync<EstoqueException>(() =>
            contexto.Estoque.ConcluirInventarioAsync(
                contexto.Filial.Id,
                new ConcluirInventarioEstoqueRequest(
                    TipoInventarioEstoque.Completo,
                    [new ItemContagemEstoqueRequest(produto.Id, 5)],
                    null),
                CancellationToken.None));
        Assert.Equal(400, semJustificativa.StatusCode);
        Assert.Equal(8, (await contexto.Db.SaldosEstoque.SingleAsync()).Quantidade);

        var resultado = await contexto.Estoque.ConcluirInventarioAsync(
            contexto.Filial.Id,
            new ConcluirInventarioEstoqueRequest(
                TipoInventarioEstoque.Completo,
                [new ItemContagemEstoqueRequest(produto.Id, 5)],
                "Contagem física"),
            CancellationToken.None);

        var movimento = await contexto.Db.MovimentosEstoque.SingleAsync();
        var item = await contexto.Db.ItensInventarioEstoque.SingleAsync();
        Assert.Equal(1, resultado.ItensAjustados);
        Assert.Equal(5, (await contexto.Db.SaldosEstoque.SingleAsync()).Quantidade);
        Assert.Equal(8, movimento.SaldoAnterior);
        Assert.Equal(5, movimento.SaldoResultante);
        Assert.Equal(8, item.SaldoAnterior);
        Assert.Equal(5, item.QuantidadeContada);
        Assert.Equal(-3, item.Diferenca);
        Assert.Equal(contexto.Usuario.Id, movimento.SolicitanteId);
        Assert.Contains(await contexto.Db.Auditoria.ToListAsync(),
            x => x.FilialId == contexto.Filial.Id && x.Entidade == nameof(MovimentoEstoque));
    }

    [Fact]
    public async Task InventarioParcialNaoAlteraProdutosForaDaContagem()
    {
        await using var contexto = await CriarContextoAsync();
        var contado = await CriarProdutoAsync(contexto.Db, "Produto contado");
        var omitido = await CriarProdutoAsync(contexto.Db, "Produto omitido");
        contexto.Db.SaldosEstoque.AddRange(
            new SaldoEstoque
            {
                EmpresaId = contexto.Empresa.Id,
                FilialId = contexto.Filial.Id,
                ProdutoId = contado.Id,
                Quantidade = 6,
                Disponivel = true
            },
            new SaldoEstoque
            {
                EmpresaId = contexto.Empresa.Id,
                FilialId = contexto.Filial.Id,
                ProdutoId = omitido.Id,
                Quantidade = 9,
                Disponivel = true
            });
        await contexto.Db.SaveChangesAsync();

        await contexto.Estoque.ConcluirInventarioAsync(
            contexto.Filial.Id,
            new ConcluirInventarioEstoqueRequest(
                TipoInventarioEstoque.Parcial,
                [new ItemContagemEstoqueRequest(contado.Id, 4)],
                "Contagem parcial"),
            CancellationToken.None);

        Assert.Equal(4, (await contexto.Db.SaldosEstoque.SingleAsync(x => x.ProdutoId == contado.Id)).Quantidade);
        Assert.Equal(9, (await contexto.Db.SaldosEstoque.SingleAsync(x => x.ProdutoId == omitido.Id)).Quantidade);
        Assert.Single(await contexto.Db.ItensInventarioEstoque.ToListAsync());
    }

    [Fact]
    public async Task NegarMovimentacaoNaoAlteraSaldoERegistraResultado()
    {
        await using var contexto = await CriarContextoAsync();
        var produto = await CriarProdutoAsync(contexto.Db, "Produto E");
        var movimentoId = await contexto.Estoque.SolicitarMovimentoAsync(
            contexto.Filial.Id,
            new SolicitarMovimentoEstoqueRequest(
                produto.Id, TipoMovimentoEstoque.EntradaManual, 3, null),
            CancellationToken.None);

        await contexto.Estoque.DecidirMovimentoAsync(
            contexto.Filial.Id,
            movimentoId,
            new DecidirMovimentoEstoqueRequest(false, "gerente", "SenhaForte!456"),
            CancellationToken.None);

        Assert.Empty(await contexto.Db.SaldosEstoque.ToListAsync());
        Assert.Equal(SituacaoMovimentoEstoque.Negado,
            (await contexto.Db.MovimentosEstoque.SingleAsync()).Situacao);
        Assert.Equal(ResultadoAutorizacao.Negada,
            (await contexto.Db.Autorizacoes.SingleAsync()).Resultado);
    }

    [Fact]
    public async Task UsuarioNaoPodeConsultarEstoqueDeFilialSemVinculo()
    {
        await using var contexto = await CriarContextoAsync();
        var outraFilial = new Filial
        {
            EmpresaId = contexto.Empresa.Id,
            Codigo = "filial-2",
            Nome = "Filial 2"
        };
        contexto.Db.Filiais.Add(outraFilial);
        await contexto.Db.SaveChangesAsync();

        var erro = await Assert.ThrowsAsync<EstoqueException>(() =>
            contexto.Estoque.ListarProdutosAsync(outraFilial.Id, CancellationToken.None));

        Assert.Equal(404, erro.StatusCode);
    }

    [Fact]
    public async Task ListarFiliaisRetornaSomenteVinculosAtivosDoTenantEmOrdemDeNome()
    {
        await using var contexto = await CriarContextoAsync();
        var filialZulu = new Filial
        {
            EmpresaId = contexto.Empresa.Id,
            Codigo = "filial-z",
            Nome = "Zulu"
        };
        var filialInativa = new Filial
        {
            EmpresaId = contexto.Empresa.Id,
            Codigo = "filial-inativa",
            Nome = "Alfa inativa",
            Ativa = false
        };
        var filialSemVinculo = new Filial
        {
            EmpresaId = contexto.Empresa.Id,
            Codigo = "filial-sem-vinculo",
            Nome = "Beta"
        };
        contexto.Db.Filiais.AddRange(filialZulu, filialInativa, filialSemVinculo);
        contexto.Db.UsuariosFiliais.AddRange(
            new UsuarioFilial
            {
                EmpresaId = contexto.Empresa.Id,
                UsuarioId = contexto.Usuario.Id,
                FilialId = filialZulu.Id
            },
            new UsuarioFilial
            {
                EmpresaId = contexto.Empresa.Id,
                UsuarioId = contexto.Usuario.Id,
                FilialId = filialInativa.Id
            });
        await contexto.Db.SaveChangesAsync();

        var filiais = await contexto.Estoque.ListarFiliaisAsync(CancellationToken.None);

        Assert.Equal(["Matriz", "Zulu"], filiais.Select(x => x.Nome));
        Assert.Equal([contexto.Filial.Id, filialZulu.Id], filiais.Select(x => x.Id));
    }

    [Fact]
    public async Task ProdutoDoCatalogoFicaIndisponivelAteConfiguracaoNaFilial()
    {
        await using var contexto = await CriarContextoAsync();
        await CriarProdutoAsync(contexto.Db, "Produto D");

        var produto = Assert.Single(await contexto.Estoque.ListarProdutosAsync(
            contexto.Filial.Id, CancellationToken.None));

        Assert.False(produto.Disponivel);
    }

    private static async Task<ContextoTeste> CriarContextoAsync()
    {
        var empresa = new Empresa
        {
            Codigo = "empresa",
            RazaoSocial = "Empresa de teste",
            Cnpj = "11222333000181"
        };
        var filial = new Filial { EmpresaId = empresa.Id, Codigo = "filial-1", Nome = "Matriz" };
        var perfilOperador = new Perfil
        {
            EmpresaId = empresa.Id,
            Nome = "Operador",
            Tipo = TipoPerfil.Operador
        };
        var perfilGerente = new Perfil
        {
            EmpresaId = empresa.Id,
            Nome = "Gerente",
            Tipo = TipoPerfil.Gerente
        };
        var hasher = new PasswordHasher<Usuario>();
        var usuario = CriarUsuario(empresa.Id, perfilOperador.Id, "operador", "SenhaForte!123", hasher);
        var autorizador = CriarUsuario(empresa.Id, perfilGerente.Id, "gerente", "SenhaForte!456", hasher);
        var permissaoConsultar = new Permissao
        {
            Codigo = "estoque.consultar",
            Descricao = "Consultar estoque"
        };
        var permissaoSolicitar = new Permissao
        {
            Codigo = "estoque.movimentar.solicitar",
            Descricao = "Solicitar movimentação"
        };
        var permissaoInventario = new Permissao
        {
            Codigo = "estoque.inventario.gerenciar",
            Descricao = "Gerenciar inventário"
        };
        var permissaoEntrada = new Permissao
        {
            Codigo = "estoque.movimento.entrada.autorizar",
            Descricao = "Autorizar entrada"
        };
        var permissaoSaida = new Permissao
        {
            Codigo = "estoque.movimento.saida.autorizar",
            Descricao = "Autorizar saída"
        };
        var usuarioAtual = new UsuarioAtualFalso(empresa.Id, usuario.Id);
        var opcoes = new DbContextOptionsBuilder<PdvDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString(), new InMemoryDatabaseRoot())
            .AddInterceptors(new AuditoriaInterceptor(usuarioAtual))
            .Options;
        var db = new PdvDbContext(opcoes, usuarioAtual);
        db.Empresas.Add(empresa);
        db.Filiais.Add(filial);
        db.Perfis.AddRange(perfilOperador, perfilGerente);
        db.Usuarios.AddRange(usuario, autorizador);
        db.Permissoes.AddRange(
            permissaoConsultar, permissaoSolicitar, permissaoInventario, permissaoEntrada, permissaoSaida);
        db.UsuariosFiliais.AddRange(
            new UsuarioFilial { EmpresaId = empresa.Id, UsuarioId = usuario.Id, FilialId = filial.Id },
            new UsuarioFilial { EmpresaId = empresa.Id, UsuarioId = autorizador.Id, FilialId = filial.Id });
        db.PerfisPermissoes.AddRange(
            new PerfilPermissao { EmpresaId = empresa.Id, PerfilId = perfilOperador.Id, PermissaoId = permissaoConsultar.Id },
            new PerfilPermissao { EmpresaId = empresa.Id, PerfilId = perfilOperador.Id, PermissaoId = permissaoSolicitar.Id },
            new PerfilPermissao { EmpresaId = empresa.Id, PerfilId = perfilOperador.Id, PermissaoId = permissaoInventario.Id },
            new PerfilPermissao { EmpresaId = empresa.Id, PerfilId = perfilGerente.Id, PermissaoId = permissaoConsultar.Id },
            new PerfilPermissao { EmpresaId = empresa.Id, PerfilId = perfilGerente.Id, PermissaoId = permissaoEntrada.Id },
            new PerfilPermissao { EmpresaId = empresa.Id, PerfilId = perfilGerente.Id, PermissaoId = permissaoSaida.Id });
        await db.SaveChangesAsync();

        var autorizacaoPermissao = new AutorizacaoPermissaoService(db);
        var contextoLogin = new EmpresaContextoLogin();
        var autenticacao = new AutenticacaoService(
            db, hasher, contextoLogin, TimeProvider.System, NullLogger<AutenticacaoService>.Instance);
        var estoque = new EstoqueService(
            db, usuarioAtual, autorizacaoPermissao, autenticacao, TimeProvider.System);
        return new ContextoTeste(db, empresa, filial, usuario, autorizador, estoque);
    }

    private static Usuario CriarUsuario(
        Guid empresaId, Guid perfilId, string login, string senha, PasswordHasher<Usuario> hasher)
    {
        var usuario = new Usuario
        {
            EmpresaId = empresaId,
            Nome = login,
            Login = login,
            PerfilId = perfilId
        };
        usuario.SenhaHash = hasher.HashPassword(usuario, senha);
        return usuario;
    }

    private static async Task<Produto> CriarProdutoAsync(PdvDbContext db, string descricao)
    {
        var departamento = new Departamento { Nome = "Alimentos" };
        var categoria = new Categoria { Nome = "Mercearia", Departamento = departamento };
        var subcategoria = new Subcategoria { Nome = "Grãos", Categoria = categoria };
        var produto = new Produto
        {
            Descricao = descricao,
            UnidadeMedida = "UN",
            Subcategoria = subcategoria
        };
        db.Produtos.Add(produto);
        await db.SaveChangesAsync();
        return produto;
    }

    private sealed record ContextoTeste(
        PdvDbContext Db,
        Empresa Empresa,
        Filial Filial,
        Usuario Usuario,
        Usuario Autorizador,
        EstoqueService Estoque) : IAsyncDisposable
    {
        public ValueTask DisposeAsync() => Db.DisposeAsync();
    }

    private sealed class UsuarioAtualFalso(Guid empresaId, Guid usuarioId) : IUsuarioAtual
    {
        public Guid? UsuarioId => usuarioId;
        public Guid? EmpresaId => empresaId;
        public Guid? FilialId => null;
    }
}
