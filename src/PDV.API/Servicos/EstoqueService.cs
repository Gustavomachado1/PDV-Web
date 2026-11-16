using System.Data;
using Microsoft.EntityFrameworkCore;
using PDV.Application;
using PDV.Domain;
using PDV.Infrastructure;

namespace PDV.API.Servicos;

public sealed class EstoqueService(
    PdvDbContext db,
    IUsuarioAtual usuarioAtual,
    AutorizacaoPermissaoService autorizacao,
    AutenticacaoService autenticacao,
    TimeProvider timeProvider)
{
    private const decimal QuantidadeMaxima = 999999999999.999999m;

    public async Task<IReadOnlyList<FilialEstoqueDto>> ListarFiliaisAsync(CancellationToken ct)
    {
        var usuarioId = ExigirUsuario();
        var empresaId = ExigirEmpresa();
        await ExigirPermissaoAsync(usuarioId, "estoque.consultar", ct);
        var filiais = await db.UsuariosFiliais.AsNoTracking()
            .Where(x => x.UsuarioId == usuarioId && x.EmpresaId == empresaId)
            .Join(db.Filiais.AsNoTracking().Where(x => x.EmpresaId == empresaId && x.Ativa),
                vinculo => vinculo.FilialId,
                filial => filial.Id,
                (_, filial) => new { filial.Id, filial.Codigo, filial.Nome })
            .OrderBy(x => x.Nome)
            .ToListAsync(ct);
        return filiais.Select(x => new FilialEstoqueDto(x.Id, x.Codigo, x.Nome)).ToArray();
    }

    public async Task<ConfiguracaoEstoqueNegativoDto> ObterConfiguracaoEstoqueNegativoAsync(
        Guid filialId, CancellationToken ct)
    {
        await ExigirAcessoAsync(filialId, "estoque.consultar", ct);
        var filial = await db.Filiais.AsNoTracking()
            .Where(x => x.Id == filialId)
            .Select(x => new { x.EmpresaId, x.PermiteEstoqueNegativo })
            .SingleOrDefaultAsync(ct)
            ?? throw EstoqueException.NaoEncontrado("A filial não foi encontrada para este usuário.");
        var permiteNaEmpresa = await db.Empresas.AsNoTracking()
            .Where(x => x.Id == filial.EmpresaId)
            .Select(x => x.PermiteEstoqueNegativo)
            .SingleAsync(ct);
        return new ConfiguracaoEstoqueNegativoDto(
            permiteNaEmpresa,
            filial.PermiteEstoqueNegativo,
            filial.PermiteEstoqueNegativo ?? permiteNaEmpresa);
    }

    public async Task AtualizarConfiguracaoEstoqueNegativoEmpresaAsync(
        Guid filialId, ConfigurarEstoqueNegativoEmpresaRequest? request, CancellationToken ct)
    {
        await ExigirAcessoAsync(filialId, "estoque.negativo.configurar", ct);
        if (request is null || request.PermiteEstoqueNegativo is null)
            throw EstoqueException.Validacao("Informe se a empresa permite estoque negativo.");

        var empresa = await db.Empresas.SingleOrDefaultAsync(ct)
            ?? throw EstoqueException.NaoEncontrado("A empresa da sessão não foi encontrada.");
        empresa.PermiteEstoqueNegativo = request.PermiteEstoqueNegativo.Value;
        await db.SaveChangesAsync(ct);
    }

    public async Task AtualizarConfiguracaoEstoqueNegativoFilialAsync(
        Guid filialId, ConfigurarEstoqueNegativoFilialRequest? request, CancellationToken ct)
    {
        await ExigirAcessoAsync(filialId, "estoque.negativo.configurar", ct);
        if (request is null)
            throw EstoqueException.Validacao("Informe a configuração da filial ou selecione a herança da empresa.");

        var filial = await db.Filiais.SingleOrDefaultAsync(x => x.Id == filialId, ct)
            ?? throw EstoqueException.NaoEncontrado("A filial não foi encontrada para este usuário.");
        filial.PermiteEstoqueNegativo = request.PermiteEstoqueNegativo;
        await db.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<ProdutoEstoqueDto>> ListarProdutosAsync(Guid filialId, CancellationToken ct)
    {
        await ExigirAcessoAsync(filialId, "estoque.consultar", ct);
        var produtos = await db.Produtos.AsNoTracking()
            .OrderBy(x => x.Descricao)
            .Select(x => new { x.Id, x.Descricao, x.UnidadeMedida, x.Status })
            .ToListAsync(ct);
        var saldos = await db.SaldosEstoque.AsNoTracking()
            .Where(x => x.FilialId == filialId)
            .ToDictionaryAsync(x => x.ProdutoId, ct);

        return produtos.Select(produto =>
        {
            saldos.TryGetValue(produto.Id, out var saldo);
            var quantidade = saldo?.Quantidade ?? 0;
            return new ProdutoEstoqueDto(
                produto.Id,
                produto.Descricao,
                produto.UnidadeMedida,
                produto.Status,
                quantidade,
                saldo?.EstoqueMinimo,
                saldo?.EstoqueIdeal,
                saldo?.Disponivel ?? false,
                saldo?.EstoqueMinimo is decimal minimo && quantidade < minimo);
        }).ToArray();
    }

    public async Task<IReadOnlyList<MovimentoEstoqueDto>> ListarMovimentosAsync(
        Guid filialId, CancellationToken ct)
    {
        await ExigirAcessoAsync(filialId, "estoque.consultar", ct);
        var movimentos = await db.MovimentosEstoque.AsNoTracking()
            .Where(x => x.FilialId == filialId && x.Situacao == SituacaoMovimentoEstoque.Concluido)
            .OrderByDescending(x => x.ConcluidoEmUtc)
            .Take(100)
            .ToListAsync(ct);
        var produtos = await NomesProdutosAsync(movimentos.Select(x => x.ProdutoId), ct);
        var usuarios = await NomesUsuariosAsync(movimentos.SelectMany(x =>
            x.AutorizadorId is Guid autorizadorId
                ? new[] { x.SolicitanteId, autorizadorId }
                : new[] { x.SolicitanteId }), ct);
        return movimentos.Select(movimento => new MovimentoEstoqueDto(
            movimento.Id,
            movimento.ProdutoId,
            produtos[movimento.ProdutoId],
            movimento.Tipo,
            movimento.Quantidade,
            movimento.SaldoAnterior!.Value,
            movimento.SaldoResultante!.Value,
            movimento.SolicitanteId,
            usuarios[movimento.SolicitanteId],
            movimento.AutorizadorId,
            movimento.AutorizadorId is Guid autorizadorId ? usuarios[autorizadorId] : null,
            movimento.Justificativa,
            movimento.ConcluidoEmUtc!.Value)).ToArray();
    }

    public async Task<IReadOnlyList<MovimentoPendenteDto>> ListarPendentesAsync(
        Guid filialId, CancellationToken ct)
    {
        await ExigirAcessoAsync(filialId, "estoque.consultar", ct);
        var movimentos = await db.MovimentosEstoque.AsNoTracking()
            .Where(x => x.FilialId == filialId &&
                x.Situacao == SituacaoMovimentoEstoque.AguardandoAutorizacao)
            .OrderBy(x => x.SolicitadoEmUtc)
            .ToListAsync(ct);
        var produtos = await NomesProdutosAsync(movimentos.Select(x => x.ProdutoId), ct);
        var usuarios = await NomesUsuariosAsync(movimentos.Select(x => x.SolicitanteId), ct);
        return movimentos.Select(movimento => new MovimentoPendenteDto(
            movimento.Id,
            movimento.ProdutoId,
            produtos[movimento.ProdutoId],
            movimento.Tipo,
            movimento.Quantidade,
            movimento.SolicitanteId,
            usuarios[movimento.SolicitanteId],
            movimento.Justificativa,
            movimento.SolicitadoEmUtc)).ToArray();
    }

    public async Task<Guid> SolicitarMovimentoAsync(
        Guid filialId, SolicitarMovimentoEstoqueRequest? request, CancellationToken ct)
    {
        var usuarioId = await ExigirAcessoAsync(filialId, "estoque.movimentar.solicitar", ct);
        if (request is null || request.ProdutoId == Guid.Empty ||
            request.Quantidade <= 0 || request.Quantidade > QuantidadeMaxima ||
            request.Tipo is not (TipoMovimentoEstoque.EntradaManual or TipoMovimentoEstoque.SaidaManual) ||
            request.Justificativa?.Length > 500 ||
            (request.Tipo == TipoMovimentoEstoque.SaidaManual &&
             string.IsNullOrWhiteSpace(request.Justificativa)))
            throw EstoqueException.Validacao(
                "Informe o produto, a quantidade positiva e os dados válidos da movimentação. Saídas exigem justificativa.");

        if (!await db.Produtos.AnyAsync(x => x.Id == request.ProdutoId, ct))
            throw EstoqueException.NaoEncontrado("O produto não foi encontrado nesta empresa.");

        if (request.Tipo == TipoMovimentoEstoque.SaidaManual)
        {
            var saldoAtual = await db.SaldosEstoque.AsNoTracking()
                .Where(x => x.FilialId == filialId && x.ProdutoId == request.ProdutoId)
                .Select(x => (decimal?)x.Quantidade)
                .SingleOrDefaultAsync(ct) ?? 0;
            if (saldoAtual - request.Quantidade < 0 &&
                !await PermiteEstoqueNegativoAsync(filialId, ct))
                throw EstoqueException.Conflito("A operação resultaria em estoque negativo, desabilitado para esta filial.");
        }

        var codigoPermissao = CodigoPermissaoMovimento(request.Tipo);
        var momento = timeProvider.GetUtcNow().UtcDateTime;
        var movimento = new MovimentoEstoque
        {
            EmpresaId = ExigirEmpresa(),
            FilialId = filialId,
            ProdutoId = request.ProdutoId,
            SolicitanteId = usuarioId,
            Tipo = request.Tipo,
            Situacao = SituacaoMovimentoEstoque.AguardandoAutorizacao,
            Quantidade = request.Quantidade,
            Justificativa = NormalizarOpcional(request.Justificativa),
            SolicitadoEmUtc = momento
        };
        var pedidoAutorizacao = new AutorizacaoOperacao
        {
            EmpresaId = ExigirEmpresa(),
            FilialId = filialId,
            SolicitanteId = usuarioId,
            Operacao = codigoPermissao,
            Referencia = movimento.Id.ToString(),
            Justificativa = movimento.Justificativa,
            Resultado = ResultadoAutorizacao.Pendente,
            SolicitadaEmUtc = momento
        };
        movimento.AutorizacaoOperacaoId = pedidoAutorizacao.Id;
        db.Autorizacoes.Add(pedidoAutorizacao);
        db.MovimentosEstoque.Add(movimento);
        await db.SaveChangesAsync(ct);
        return movimento.Id;
    }

    public async Task DecidirMovimentoAsync(
        Guid filialId, Guid movimentoId, DecidirMovimentoEstoqueRequest? request, CancellationToken ct)
    {
        await ExigirAcessoAsync(filialId, "estoque.consultar", ct);
        if (request is null || request.Aprovar is null || string.IsNullOrWhiteSpace(request.Login) ||
            string.IsNullOrEmpty(request.Senha) || request.Login.Length > 100 || request.Senha.Length > 1024)
            throw EstoqueException.Validacao("Informe as credenciais do autorizador.");

        var movimento = await db.MovimentosEstoque
            .Include(x => x.AutorizacaoOperacao)
            .SingleOrDefaultAsync(x => x.Id == movimentoId && x.FilialId == filialId, ct);
        if (movimento is null || movimento.AutorizacaoOperacao is null)
            throw EstoqueException.NaoEncontrado("A solicitação não foi encontrada nesta filial.");
        if (movimento.Situacao != SituacaoMovimentoEstoque.AguardandoAutorizacao)
            throw EstoqueException.Conflito("A solicitação já foi decidida.");

        var aprovador = await autenticacao.ReautenticarParaAutorizacaoAsync(
            ExigirEmpresa(), request.Login, request.Senha, ct);
        if (aprovador is null)
            throw EstoqueException.NaoAutorizado("As credenciais do autorizador são inválidas.");
        if (aprovador.Id == movimento.SolicitanteId)
            throw EstoqueException.Proibido("Quem solicitou a operação não pode autorizá-la.");
        if (!await db.UsuariosFiliais.AnyAsync(
                x => x.UsuarioId == aprovador.Id && x.FilialId == filialId, ct))
            throw EstoqueException.Proibido("O autorizador não tem acesso a esta filial.");
        if (!await autorizacao.TemPermissaoAsync(
                aprovador.Id, movimento.AutorizacaoOperacao.Operacao, ct))
            throw EstoqueException.Proibido("O autorizador não possui a permissão específica desta operação.");

        await using var transacao = db.Database.IsRelational()
            ? await db.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct)
            : null;
        movimento = await db.MovimentosEstoque
            .Include(x => x.AutorizacaoOperacao)
            .SingleOrDefaultAsync(x => x.Id == movimentoId && x.FilialId == filialId, ct);
        if (movimento is null || movimento.AutorizacaoOperacao is null)
            throw EstoqueException.NaoEncontrado("A solicitação não foi encontrada nesta filial.");
        if (movimento.Situacao != SituacaoMovimentoEstoque.AguardandoAutorizacao ||
            movimento.AutorizacaoOperacao.Resultado != ResultadoAutorizacao.Pendente)
            throw EstoqueException.Conflito("A solicitação já foi decidida.");
        if (!await autorizacao.TemPermissaoAsync(
                aprovador.Id, movimento.AutorizacaoOperacao.Operacao, ct))
            throw EstoqueException.Proibido("O autorizador não possui a permissão específica desta operação.");

        var agora = timeProvider.GetUtcNow().UtcDateTime;
        if (request.Aprovar.Value)
        {
            await AplicarMovimentoAsync(movimento, aprovador.Id, ct);
            movimento.Situacao = SituacaoMovimentoEstoque.Concluido;
            movimento.ConcluidoEmUtc = agora;
            movimento.AutorizacaoOperacao.Resultado = ResultadoAutorizacao.Aprovada;
        }
        else
        {
            movimento.Situacao = SituacaoMovimentoEstoque.Negado;
            movimento.AutorizacaoOperacao.Resultado = ResultadoAutorizacao.Negada;
        }

        movimento.AutorizadorId = aprovador.Id;
        movimento.AutorizacaoOperacao.AutorizadorId = aprovador.Id;
        movimento.AutorizacaoOperacao.DecididaEmUtc = agora;
        await db.SaveChangesAsync(ct);
        if (transacao is not null)
            await transacao.CommitAsync(ct);
    }

    public async Task AtualizarConfiguracaoAsync(
        Guid filialId, Guid produtoId, ConfigurarEstoqueRequest? request, CancellationToken ct)
    {
        var _ = await ExigirAcessoAsync(filialId, "estoque.configurar", ct);
        if (request is null || request.Disponivel is null ||
            request.EstoqueMinimo < 0 || request.EstoqueIdeal < 0 ||
            request.EstoqueMinimo > 999999999999.999999m ||
            request.EstoqueIdeal > 999999999999.999999m)
            throw EstoqueException.Validacao("Os parâmetros de estoque devem ser quantidades não negativas válidas.");
        if (!await db.Produtos.AnyAsync(x => x.Id == produtoId, ct))
            throw EstoqueException.NaoEncontrado("O produto não foi encontrado nesta empresa.");

        var saldo = await db.SaldosEstoque.SingleOrDefaultAsync(
            x => x.FilialId == filialId && x.ProdutoId == produtoId, ct);
        if (saldo is null)
        {
            saldo = new SaldoEstoque
            {
                EmpresaId = ExigirEmpresa(),
                FilialId = filialId,
                ProdutoId = produtoId
            };
            db.SaldosEstoque.Add(saldo);
        }

        saldo.EstoqueMinimo = request.EstoqueMinimo;
        saldo.EstoqueIdeal = request.EstoqueIdeal;
        saldo.Disponivel = request.Disponivel.Value;
        await db.SaveChangesAsync(ct);
    }

    public async Task<InventarioConcluidoDto> ConcluirInventarioAsync(
        Guid filialId, ConcluirInventarioEstoqueRequest? request, CancellationToken ct)
    {
        var usuarioId = await ExigirAcessoAsync(filialId, "estoque.inventario.gerenciar", ct);
        if (request is null || !Enum.IsDefined(request.Tipo) || request.Itens is null ||
            request.Itens.Count == 0 || request.Itens.Any(x =>
                x.ProdutoId == Guid.Empty || x.QuantidadeContada < 0 ||
                x.QuantidadeContada > QuantidadeMaxima) ||
            request.Itens.Select(x => x.ProdutoId).Distinct().Count() != request.Itens.Count ||
            request.Justificativa?.Length > 500)
            throw EstoqueException.Validacao("Informe itens de inventário válidos, sem duplicatas e com quantidades não negativas.");

        await using var transacao = db.Database.IsRelational()
            ? await db.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct)
            : null;

        var produtos = await db.Produtos.AsNoTracking().Select(x => x.Id).ToListAsync(ct);
        var idsContados = request.Itens.Select(x => x.ProdutoId).ToHashSet();
        if (idsContados.Any(id => !produtos.Contains(id)))
            throw EstoqueException.NaoEncontrado("Um ou mais produtos não foram encontrados nesta empresa.");
        if (request.Tipo == TipoInventarioEstoque.Completo &&
            (idsContados.Count != produtos.Count || produtos.Any(id => !idsContados.Contains(id))))
            throw EstoqueException.Validacao("O inventário completo deve incluir todos os produtos da empresa.");

        var saldos = await db.SaldosEstoque
            .Where(x => x.FilialId == filialId && idsContados.Contains(x.ProdutoId))
            .ToDictionaryAsync(x => x.ProdutoId, ct);
        var contagens = request.Itens.ToDictionary(x => x.ProdutoId);
        var diferencaExiste = contagens.Any(x =>
            x.Value.QuantidadeContada != (saldos.TryGetValue(x.Key, out var saldo) ? saldo.Quantidade : 0));
        var justificativa = NormalizarOpcional(request.Justificativa);
        if (diferencaExiste && justificativa is null)
            throw EstoqueException.Validacao("Informe a justificativa para os ajustes do inventário.");
        var itensAjustados = contagens.Count(x =>
            x.Value.QuantidadeContada != (saldos.TryGetValue(x.Key, out var saldo) ? saldo.Quantidade : 0));

        var agora = timeProvider.GetUtcNow().UtcDateTime;
        var inventario = new InventarioEstoque
        {
            EmpresaId = ExigirEmpresa(),
            FilialId = filialId,
            UsuarioId = usuarioId,
            Tipo = request.Tipo,
            Justificativa = justificativa,
            ConcluidoEmUtc = agora
        };
        db.InventariosEstoque.Add(inventario);

        foreach (var item in request.Itens)
        {
            if (!saldos.TryGetValue(item.ProdutoId, out var saldo))
            {
                saldo = new SaldoEstoque
                {
                    EmpresaId = ExigirEmpresa(),
                    FilialId = filialId,
                    ProdutoId = item.ProdutoId
                };
                saldos.Add(item.ProdutoId, saldo);
                db.SaldosEstoque.Add(saldo);
            }

            var anterior = saldo.Quantidade;
            var diferenca = item.QuantidadeContada - anterior;
            saldo.Quantidade = item.QuantidadeContada;
            inventario.Itens.Add(new ItemInventarioEstoque
            {
                EmpresaId = ExigirEmpresa(),
                FilialId = filialId,
                InventarioEstoqueId = inventario.Id,
                ProdutoId = item.ProdutoId,
                SaldoAnterior = anterior,
                QuantidadeContada = item.QuantidadeContada,
                Diferenca = diferenca
            });

            if (diferenca != 0)
            {
                db.MovimentosEstoque.Add(new MovimentoEstoque
                {
                    EmpresaId = ExigirEmpresa(),
                    FilialId = filialId,
                    ProdutoId = item.ProdutoId,
                    InventarioEstoqueId = inventario.Id,
                    SolicitanteId = usuarioId,
                    Tipo = TipoMovimentoEstoque.AjusteInventario,
                    Situacao = SituacaoMovimentoEstoque.Concluido,
                    Quantidade = Math.Abs(diferenca),
                    SaldoAnterior = anterior,
                    SaldoResultante = item.QuantidadeContada,
                    Justificativa = justificativa,
                    SolicitadoEmUtc = agora,
                    ConcluidoEmUtc = agora
                });
            }
        }

        await db.SaveChangesAsync(ct);
        if (transacao is not null)
            await transacao.CommitAsync(ct);
        return new InventarioConcluidoDto(
            inventario.Id, inventario.Tipo, inventario.ConcluidoEmUtc, request.Itens.Count, itensAjustados);
    }

    public async Task<IReadOnlyList<InventarioResumoDto>> ListarInventariosAsync(
        Guid filialId, CancellationToken ct)
    {
        await ExigirAcessoAsync(filialId, "estoque.consultar", ct);
        var inventarios = await db.InventariosEstoque.AsNoTracking()
            .Where(x => x.FilialId == filialId)
            .OrderByDescending(x => x.ConcluidoEmUtc)
            .Take(50)
            .Select(x => new
            {
                x.Id,
                x.Tipo,
                x.UsuarioId,
                x.ConcluidoEmUtc,
                ItensContados = x.Itens.Count,
                ItensAjustados = x.Itens.Count(i => i.Diferenca != 0),
                x.Justificativa
            })
            .ToListAsync(ct);
        var usuarios = await NomesUsuariosAsync(inventarios.Select(x => x.UsuarioId), ct);
        return inventarios.Select(x => new InventarioResumoDto(
            x.Id, x.Tipo, x.UsuarioId, usuarios[x.UsuarioId], x.ConcluidoEmUtc, x.ItensContados,
            x.ItensAjustados, x.Justificativa)).ToArray();
    }

    private async Task AplicarMovimentoAsync(
        MovimentoEstoque movimento, Guid autorizadorId, CancellationToken ct)
    {
        var saldo = await db.SaldosEstoque.SingleOrDefaultAsync(
            x => x.FilialId == movimento.FilialId && x.ProdutoId == movimento.ProdutoId, ct);
        var novoSaldo = saldo is null;
        if (saldo is null)
        {
            saldo = new SaldoEstoque
            {
                EmpresaId = movimento.EmpresaId,
                FilialId = movimento.FilialId,
                ProdutoId = movimento.ProdutoId
            };
        }

        var anterior = saldo.Quantidade;
        var sinal = movimento.Tipo == TipoMovimentoEstoque.EntradaManual ? 1m : -1m;
        var resultante = anterior + sinal * movimento.Quantidade;
        if (resultante > QuantidadeMaxima || resultante < -QuantidadeMaxima)
            throw EstoqueException.Validacao("O saldo resultante excede a faixa numérica suportada.");
        if (resultante < 0)
        {
            if (!await PermiteEstoqueNegativoAsync(movimento.FilialId, ct))
                throw EstoqueException.Conflito(
                    "A operação resultaria em estoque negativo, desabilitado para esta filial.");
            if (!await autorizacao.TemPermissaoAsync(
                    autorizadorId, "estoque.negativo.permitir", ct))
                throw EstoqueException.Proibido(
                    "O autorizador não possui permissão para permitir estoque negativo.");
        }
        saldo.Quantidade = resultante;
        if (novoSaldo)
            db.SaldosEstoque.Add(saldo);
        movimento.SaldoAnterior = anterior;
        movimento.SaldoResultante = resultante;
    }

    private async Task<bool> PermiteEstoqueNegativoAsync(Guid filialId, CancellationToken ct)
    {
        var filial = await db.Filiais.AsNoTracking()
            .Where(x => x.Id == filialId)
            .Select(x => new { x.EmpresaId, x.PermiteEstoqueNegativo })
            .SingleOrDefaultAsync(ct)
            ?? throw EstoqueException.NaoEncontrado("A filial não foi encontrada para este usuário.");
        if (filial.PermiteEstoqueNegativo is bool permiteFilial)
            return permiteFilial;

        return await db.Empresas.AsNoTracking()
            .Where(x => x.Id == filial.EmpresaId)
            .Select(x => x.PermiteEstoqueNegativo)
            .SingleAsync(ct);
    }

    private async Task<Guid> ExigirAcessoAsync(Guid filialId, string permissao, CancellationToken ct)
    {
        var usuarioId = ExigirUsuario();
        if (!await db.Filiais.AnyAsync(x => x.Id == filialId && x.Ativa, ct) ||
            !await db.UsuariosFiliais.AnyAsync(
                x => x.UsuarioId == usuarioId && x.FilialId == filialId, ct))
            throw EstoqueException.NaoEncontrado("A filial não foi encontrada para este usuário.");
        await ExigirPermissaoAsync(usuarioId, permissao, ct);
        return usuarioId;
    }

    private async Task ExigirPermissaoAsync(Guid usuarioId, string permissao, CancellationToken ct)
    {
        if (!await autorizacao.TemPermissaoAsync(usuarioId, permissao, ct))
            throw EstoqueException.Proibido("Seu usuário não tem permissão para esta operação.");
    }

    private async Task<Dictionary<Guid, string>> NomesProdutosAsync(
        IEnumerable<Guid> produtos, CancellationToken ct)
    {
        var ids = produtos.Distinct().ToArray();
        return await db.Produtos.AsNoTracking().Where(x => ids.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, x => x.Descricao, ct);
    }

    private async Task<Dictionary<Guid, string>> NomesUsuariosAsync(
        IEnumerable<Guid> usuarios, CancellationToken ct)
    {
        var ids = usuarios.Distinct().ToArray();
        return await db.Usuarios.AsNoTracking().Where(x => ids.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, x => x.Nome, ct);
    }

    private Guid ExigirUsuario()
        => usuarioAtual.UsuarioId is Guid id && id != Guid.Empty
            ? id
            : throw EstoqueException.NaoAutorizado("A sessão do usuário não está disponível.");

    private Guid ExigirEmpresa()
        => usuarioAtual.EmpresaId is Guid id && id != Guid.Empty
            ? id
            : throw EstoqueException.NaoAutorizado("A empresa da sessão não está disponível.");

    private static string CodigoPermissaoMovimento(TipoMovimentoEstoque tipo)
        => tipo switch
        {
            TipoMovimentoEstoque.EntradaManual => "estoque.movimento.entrada.autorizar",
            TipoMovimentoEstoque.SaidaManual => "estoque.movimento.saida.autorizar",
            _ => throw EstoqueException.Validacao("Tipo de movimentação inválido.")
        };

    private static string? NormalizarOpcional(string? valor)
        => string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
}

public sealed record FilialEstoqueDto(Guid Id, string Codigo, string Nome);
public sealed record ProdutoEstoqueDto(
    Guid ProdutoId, string Descricao, string UnidadeMedida, StatusProduto Status,
    decimal Quantidade, decimal? EstoqueMinimo, decimal? EstoqueIdeal, bool Disponivel, bool EstoqueBaixo);
public sealed record MovimentoEstoqueDto(
    Guid Id, Guid ProdutoId, string Produto, TipoMovimentoEstoque Tipo, decimal Quantidade,
    decimal SaldoAnterior, decimal SaldoResultante, Guid SolicitanteId, string SolicitanteNome,
    Guid? AutorizadorId, string? AutorizadorNome,
    string? Justificativa, DateTime ConcluidoEmUtc);
public sealed record MovimentoPendenteDto(
    Guid Id, Guid ProdutoId, string Produto, TipoMovimentoEstoque Tipo, decimal Quantidade,
    Guid SolicitanteId, string SolicitanteNome, string? Justificativa, DateTime SolicitadoEmUtc);
public sealed record SolicitarMovimentoEstoqueRequest(
    Guid ProdutoId, TipoMovimentoEstoque Tipo, decimal Quantidade, string? Justificativa);
public sealed record DecidirMovimentoEstoqueRequest(
    bool? Aprovar, string Login, string Senha);
public sealed record ConfigurarEstoqueRequest(
    decimal? EstoqueMinimo, decimal? EstoqueIdeal, bool? Disponivel);
public sealed record ConfiguracaoEstoqueNegativoDto(
    bool PermiteEstoqueNegativoEmpresa,
    bool? PermiteEstoqueNegativoFilial,
    bool PermiteEstoqueNegativoEfetivo);
public sealed record ConfigurarEstoqueNegativoEmpresaRequest(bool? PermiteEstoqueNegativo);
public sealed record ConfigurarEstoqueNegativoFilialRequest(bool? PermiteEstoqueNegativo);
public sealed record ItemContagemEstoqueRequest(Guid ProdutoId, decimal QuantidadeContada);
public sealed record ConcluirInventarioEstoqueRequest(
    TipoInventarioEstoque Tipo, IReadOnlyList<ItemContagemEstoqueRequest>? Itens, string? Justificativa);
public sealed record InventarioConcluidoDto(
    Guid Id, TipoInventarioEstoque Tipo, DateTime ConcluidoEmUtc, int ItensContados, int ItensAjustados);
public sealed record InventarioResumoDto(
    Guid Id, TipoInventarioEstoque Tipo, Guid UsuarioId, string UsuarioNome, DateTime ConcluidoEmUtc,
    int ItensContados, int ItensAjustados, string? Justificativa);

public sealed class EstoqueException(int statusCode, string campo, string mensagem) : Exception(mensagem)
{
    public int StatusCode { get; } = statusCode;
    public string Campo { get; } = campo;

    public static EstoqueException Validacao(string mensagem) => new(400, "estoque", mensagem);
    public static EstoqueException NaoEncontrado(string mensagem) => new(404, "estoque", mensagem);
    public static EstoqueException NaoAutorizado(string mensagem) => new(401, "estoque", mensagem);
    public static EstoqueException Proibido(string mensagem) => new(403, "estoque", mensagem);
    public static EstoqueException Conflito(string mensagem) => new(409, "estoque", mensagem);
}
