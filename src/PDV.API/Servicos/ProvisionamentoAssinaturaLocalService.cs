using System.Data;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;
using PDV.Domain;
using PDV.Infrastructure;

namespace PDV.API.Servicos;

public sealed class ProvisionamentoAssinaturaLocalService(
    PdvDbContext db,
    EmpresaContextoLogin empresaContexto,
    IHostEnvironment ambiente,
    TimeProvider timeProvider)
{
    private const string PlanoNome = "Essencial";
    private const int PlanoMaxFiliais = 1;
    private const int PlanoMaxCaixas = 2;
    private const int PlanoMaxUsuarios = 3;

    public async Task ExecutarAsync(bool confirmouBancoLocal, CancellationToken cancellationToken)
    {
        if (!ambiente.IsDevelopment())
            throw new InvalidOperationException(
                "A recuperação da assinatura só pode ser executada com ASPNETCORE_ENVIRONMENT=Development.");

        if (!confirmouBancoLocal)
            throw new InvalidOperationException(
                "Confirme que a connection string aponta para o banco local usando --confirmar-banco-local.");

        await using var transacao = db.Database.IsRelational()
            ? await db.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken)
            : null;

        if (db.Database.IsSqlServer())
            await AdquirirBloqueioAsync(cancellationToken);

        var empresa = await ObterEmpresaElegivelAsync(cancellationToken);
        await GarantirSemAssinaturasAsync(cancellationToken);
        var plano = await ObterPlanoEssencialAsync(cancellationToken);

        empresaContexto.Definir(empresa.Id);
        if (plano is null)
        {
            plano = new Plano
            {
                Nome = PlanoNome,
                MaxFiliais = PlanoMaxFiliais,
                MaxCaixas = PlanoMaxCaixas,
                MaxUsuarios = PlanoMaxUsuarios
            };
            db.Planos.Add(plano);
        }

        db.Assinaturas.Add(new Assinatura
        {
            EmpresaId = empresa.Id,
            PlanoId = plano.Id,
            Situacao = SituacaoAssinatura.Ativa,
            MaxFiliais = PlanoMaxFiliais,
            MaxCaixas = PlanoMaxCaixas,
            MaxUsuarios = PlanoMaxUsuarios,
            ValorMensal = 0m,
            CondicoesComerciais = "R$ 0,00 somente para desenvolvimento local; sem cobrança.",
            InicioEmUtc = timeProvider.GetUtcNow().UtcDateTime
        });

        try
        {
            await db.SaveChangesAsync(cancellationToken);
            if (transacao is not null)
                await transacao.CommitAsync(cancellationToken);
        }
        finally
        {
            empresaContexto.Limpar();
        }
    }

    private async Task<Empresa> ObterEmpresaElegivelAsync(CancellationToken cancellationToken)
    {
        var empresas = await db.Empresas.IgnoreQueryFilters().AsNoTracking().ToListAsync(cancellationToken);
        if (empresas.Count != 1)
            throw new InvalidOperationException(
                "A recuperação exige exatamente uma empresa no banco. Identifique a empresa correta e resolva a ambiguidade manualmente.");

        var empresa = empresas[0];
        if (!empresa.Ativa)
            throw new InvalidOperationException(
                "A empresa única está inativa. Revise seu estado antes de provisionar uma assinatura.");

        if (!await db.Filiais.IgnoreQueryFilters().AnyAsync(
                filial => filial.EmpresaId == empresa.Id && filial.Ativa, cancellationToken))
            throw new InvalidOperationException(
                "A empresa não possui filial ativa. Corrija o provisionamento da empresa antes de criar a assinatura.");

        return empresa;
    }

    private async Task GarantirSemAssinaturasAsync(CancellationToken cancellationToken)
    {
        if (await db.Assinaturas.IgnoreQueryFilters().AsNoTracking().AnyAsync(cancellationToken))
            throw new InvalidOperationException(
                "Já existe uma assinatura no banco. Revise manualmente a relação entre empresa, assinatura e plano; nenhum dado foi alterado.");

        if (await db.AssinaturasHistorico.IgnoreQueryFilters().AsNoTracking().AnyAsync(cancellationToken))
            throw new InvalidOperationException(
                "Há histórico de assinatura sem uma assinatura correspondente. Resolva o estado parcial manualmente antes de tentar novamente.");
    }

    private async Task<Plano?> ObterPlanoEssencialAsync(CancellationToken cancellationToken)
    {
        var planos = await db.Planos.AsNoTracking().ToListAsync(cancellationToken);
        var candidatos = planos
            .Where(plano => string.Equals(plano.Nome, PlanoNome, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (candidatos.Count > 1)
            throw new InvalidOperationException(
                "Há mais de um plano correspondente a Essencial. Resolva a ambiguidade manualmente antes de tentar novamente.");

        if (candidatos.Count == 0)
            return null;

        var existente = candidatos[0];
        if (existente.Nome != PlanoNome ||
            !existente.Ativo ||
            existente.MaxFiliais != PlanoMaxFiliais ||
            existente.MaxCaixas != PlanoMaxCaixas ||
            existente.MaxUsuarios != PlanoMaxUsuarios)
            throw new InvalidOperationException(
                "O plano Essencial existente diverge do padrão ativo de 1 filial, 2 caixas e 3 usuários. Revise-o manualmente; nenhum dado foi alterado.");

        return existente;
    }

    private async Task AdquirirBloqueioAsync(CancellationToken cancellationToken)
    {
        var conexao = db.Database.GetDbConnection();
        await using var comando = conexao.CreateCommand();
        comando.Transaction = db.Database.CurrentTransaction?.GetDbTransaction();
        comando.CommandText = """
            DECLARE @resultado int;
            EXEC @resultado = sys.sp_getapplock
                @Resource = N'PDV-Web:ProvisionamentoAssinaturaLocal',
                @LockMode = N'Exclusive',
                @LockOwner = N'Transaction',
                @LockTimeout = 60000;
            SELECT @resultado;
            """;

        var resultado = Convert.ToInt32(
            await comando.ExecuteScalarAsync(cancellationToken), CultureInfo.InvariantCulture);
        if (resultado < 0)
            throw new InvalidOperationException(
                "Não foi possível obter o bloqueio para recuperar a assinatura local.");
    }
}
