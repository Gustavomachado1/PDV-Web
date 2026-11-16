using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PDV.Application;
using PDV.Domain;
using PDV.Infrastructure;
using Xunit;

namespace PDV.Infrastructure.Tests;

public class AuditoriaInterceptorTests
{
    private static readonly Guid EmpresaAtual = Guid.NewGuid();
    private static readonly Guid OutraEmpresa = Guid.NewGuid();

    [Fact]
    public async Task GravarEntidadeEscopadaSemEmpresaAtualFalha()
    {
        await using var contexto = CriarContexto(null);
        var filial = new Filial { EmpresaId = EmpresaAtual };
        contexto.Filiais.Add(filial);

        var erro = await Assert.ThrowsAsync<InvalidOperationException>(() => contexto.SaveChangesAsync());

        Assert.Contains("sem uma empresa válida", erro.Message);
        Assert.Equal(EntityState.Added, contexto.Entry(filial).State);
    }

    [Fact]
    public async Task NovaEntidadeEscopadaRecebeEmpresaAtualQuandoEmpresaIdEstaVazio()
    {
        await using var contexto = CriarContexto(EmpresaAtual);
        var filial = new Filial();
        contexto.Filiais.Add(filial);

        await contexto.SaveChangesAsync();

        Assert.Equal(EmpresaAtual, filial.EmpresaId);
        Assert.Equal(filial, await contexto.Filiais.SingleAsync());
    }

    [Fact]
    public async Task GravarEntidadeDeOutraEmpresaFalha()
    {
        await using var contexto = CriarContexto(EmpresaAtual);
        contexto.Filiais.Add(new Filial { EmpresaId = OutraEmpresa });

        var erro = await Assert.ThrowsAsync<InvalidOperationException>(() => contexto.SaveChangesAsync());

        Assert.Contains("outra empresa", erro.Message);
    }

    [Theory]
    [InlineData(EntityState.Modified)]
    [InlineData(EntityState.Deleted)]
    public async Task AlterarOuExcluirEntidadeEscopadaSemEmpresaAtualFalha(EntityState estado)
    {
        await using var contexto = CriarContexto(null);
        var filial = new Filial { EmpresaId = EmpresaAtual };
        contexto.Attach(filial);
        contexto.Entry(filial).State = estado;

        var erro = await Assert.ThrowsAsync<InvalidOperationException>(() => contexto.SaveChangesAsync());

        Assert.Contains("sem uma empresa válida", erro.Message);
    }

    [Fact]
    public async Task AlterarEmpresaDeEntidadeExistenteFalha()
    {
        await using var contexto = CriarContexto(EmpresaAtual);
        var filial = new Filial { EmpresaId = EmpresaAtual };
        contexto.Attach(filial);
        filial.EmpresaId = OutraEmpresa;

        var erro = await Assert.ThrowsAsync<InvalidOperationException>(() => contexto.SaveChangesAsync());

        Assert.Contains("outra empresa", erro.Message);
    }

    [Fact]
    public async Task EntidadeGlobalPodeSerGravadaSemEmpresaAtual()
    {
        await using var contexto = CriarContexto(null);
        var permissao = new Permissao { Codigo = "venda.criar", Descricao = "Criar venda" };
        contexto.Permissoes.Add(permissao);

        await contexto.SaveChangesAsync();

        Assert.Equal(permissao, await contexto.Permissoes.SingleAsync());
    }

    [Fact]
    public async Task EmpresaPodeSerCriadaSemEmpresaAtual()
    {
        await using var contexto = CriarContexto(null);
        var empresa = new Empresa { Codigo = "empresa-1", RazaoSocial = "Empresa de teste", Cnpj = "12345678000199" };
        contexto.Empresas.Add(empresa);

        await contexto.SaveChangesAsync();

        Assert.Equal(EntityState.Unchanged, contexto.Entry(empresa).State);
    }

    [Fact]
    public async Task AtualizarEExcluirEntidadeDaEmpresaAtualContinuamPermitidos()
    {
        await using var contexto = CriarContexto(EmpresaAtual);
        var filial = new Filial { EmpresaId = EmpresaAtual, Codigo = "filial-1", Nome = "Original" };
        contexto.Filiais.Add(filial);
        await contexto.SaveChangesAsync();

        filial.Nome = "Atualizada";
        await contexto.SaveChangesAsync();
        contexto.Filiais.Remove(filial);
        await contexto.SaveChangesAsync();

        Assert.Empty(await contexto.Filiais.ToListAsync());
    }

    [Fact]
    public async Task CatalogoFicaEscopadoPorEmpresaEBarcodesSaoUnicosNoModelo()
    {
        var root = new InMemoryDatabaseRoot();
        var nomeBanco = Guid.NewGuid().ToString();
        await using (var contexto = CriarContexto(EmpresaAtual, nomeBanco, root))
        {
            var departamento = new Departamento { Nome = "Alimentos" };
            var categoria = new Categoria { Nome = "Mercearia", Departamento = departamento };
            var subcategoria = new Subcategoria { Nome = "Grãos", Categoria = categoria };
            var produto = new Produto
            {
                Descricao = "Arroz",
                UnidadeMedida = "un",
                Subcategoria = subcategoria,
                CodigosBarras = [new CodigoBarrasProduto { Codigo = "7891234567890" }]
            };
            contexto.Produtos.Add(produto);
            await contexto.SaveChangesAsync();
        }

        await using (var contexto = CriarContexto(OutraEmpresa, nomeBanco, root))
        {
            Assert.Empty(await contexto.Departamentos.ToListAsync());
            Assert.Empty(await contexto.Categorias.ToListAsync());
            Assert.Empty(await contexto.Subcategorias.ToListAsync());
            Assert.Empty(await contexto.Produtos.ToListAsync());
            Assert.Empty(await contexto.CodigosBarrasProdutos.ToListAsync());
        }

        await using (var contexto = CriarContexto(EmpresaAtual, nomeBanco, root))
        {
            Assert.Single(await contexto.Departamentos.ToListAsync());
            Assert.Single(await contexto.Categorias.ToListAsync());
            Assert.Single(await contexto.Subcategorias.ToListAsync());
            Assert.Single(await contexto.Produtos.ToListAsync());
            Assert.Single(await contexto.CodigosBarrasProdutos.ToListAsync());

            var codigo = await contexto.CodigosBarrasProdutos.SingleAsync();
            codigo.Ativo = false;
            await contexto.SaveChangesAsync();
            Assert.Contains(await contexto.Auditoria.ToListAsync(),
                x => x.Entidade == nameof(CodigoBarrasProduto) && x.Operacao == "CodigoBarrasProduto.Alterar");
            Assert.Single(await contexto.CodigosBarrasProdutos.ToListAsync());

            var indice = contexto.Model.FindEntityType(typeof(CodigoBarrasProduto))!.GetIndexes()
                .Single(x => x.Properties.Select(p => p.Name).SequenceEqual(["EmpresaId", "Codigo"]));
            Assert.True(indice.IsUnique);
            Assert.Equal(true, contexto.Model.FindEntityType(typeof(CodigoBarrasProduto))!
                .FindProperty(nameof(CodigoBarrasProduto.Ativo))!.GetDefaultValue());

            AssertForeignKeyIncluiEmpresa(contexto, typeof(Categoria), typeof(Departamento), "DepartamentoId");
            AssertForeignKeyIncluiEmpresa(contexto, typeof(Subcategoria), typeof(Categoria), "CategoriaId");
            var relacaoProdutoSubcategoria = contexto.Model.FindEntityType(typeof(Produto))!.GetForeignKeys()
                .Single(x => x.PrincipalEntityType.ClrType == typeof(Subcategoria));
            Assert.Equal(new[] { "EmpresaId", "SubcategoriaId" },
                relacaoProdutoSubcategoria.Properties.Select(x => x.Name));
            Assert.Equal(new[] { "EmpresaId", "Id" },
                relacaoProdutoSubcategoria.PrincipalKey.Properties.Select(x => x.Name));
            AssertForeignKeyIncluiEmpresa(contexto, typeof(CodigoBarrasProduto), typeof(Produto), "ProdutoId");
        }
    }

    private static void AssertForeignKeyIncluiEmpresa(
        PdvDbContext contexto, Type entidade, Type principal, string propriedade)
    {
        var foreignKey = contexto.Model.FindEntityType(entidade)!.GetForeignKeys()
            .Single(x => x.PrincipalEntityType.ClrType == principal);
        Assert.Equal(new[] { "EmpresaId", propriedade }, foreignKey.Properties.Select(x => x.Name));
        Assert.Equal(new[] { "EmpresaId", "Id" }, foreignKey.PrincipalKey.Properties.Select(x => x.Name));
    }

    private static PdvDbContext CriarContexto(
        Guid? empresaId, string? nomeBanco = null, InMemoryDatabaseRoot? root = null)
    {
        var usuarioAtual = new UsuarioAtualFalso(empresaId);
        var interceptor = new AuditoriaInterceptor(usuarioAtual);
        var opcoes = new DbContextOptionsBuilder<PdvDbContext>()
            .UseInMemoryDatabase(nomeBanco ?? Guid.NewGuid().ToString(), root ?? new InMemoryDatabaseRoot())
            .AddInterceptors(interceptor)
            .Options;

        return new PdvDbContext(opcoes, usuarioAtual);
    }

    private sealed class UsuarioAtualFalso(Guid? empresaId) : IUsuarioAtual
    {
        public Guid? UsuarioId => null;
        public Guid? EmpresaId => empresaId;
        public Guid? FilialId => null;
    }
}
