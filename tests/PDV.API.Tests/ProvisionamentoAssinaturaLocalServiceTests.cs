using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using PDV.API.Servicos;
using PDV.Application;
using PDV.Domain;
using PDV.Infrastructure;
using Xunit;

namespace PDV.API.Tests;

public class ProvisionamentoAssinaturaLocalServiceTests
{
    [Fact]
    public async Task CriaPlanoEAssinaturaEssencialAtivaSemCobranca()
    {
        await using var contexto = await CriarContextoAsync();

        await contexto.Servico.ExecutarAsync(true, CancellationToken.None);

        var plano = await contexto.Db.Planos.SingleAsync();
        var assinatura = await contexto.Db.Assinaturas.IgnoreQueryFilters().SingleAsync();
        Assert.Equal("Essencial", plano.Nome);
        Assert.True(plano.Ativo);
        Assert.Equal((1, 2, 3), (plano.MaxFiliais, plano.MaxCaixas, plano.MaxUsuarios));
        Assert.Equal(contexto.EmpresaId, assinatura.EmpresaId);
        Assert.Equal(plano.Id, assinatura.PlanoId);
        Assert.Equal(SituacaoAssinatura.Ativa, assinatura.Situacao);
        Assert.Equal((1, 2, 3), (assinatura.MaxFiliais, assinatura.MaxCaixas, assinatura.MaxUsuarios));
        Assert.Equal(0m, assinatura.ValorMensal);
        Assert.Contains("somente para desenvolvimento local", assinatura.CondicoesComerciais);
    }

    [Fact]
    public async Task ReutilizaPlanoEssencialCompativelSemAlterarPlano()
    {
        await using var contexto = await CriarContextoAsync();
        var plano = CriarPlanoEssencial();
        contexto.Db.Planos.Add(plano);
        await contexto.Db.SaveChangesAsync();
        var criadoEmUtc = plano.CriadoEmUtc;
        contexto.Db.ChangeTracker.Clear();

        await contexto.Servico.ExecutarAsync(true, CancellationToken.None);

        var planoFinal = await contexto.Db.Planos.SingleAsync();
        Assert.Equal(plano.Id, planoFinal.Id);
        Assert.Equal(criadoEmUtc, planoFinal.CriadoEmUtc);
        Assert.Equal(plano.MaxCaixas, planoFinal.MaxCaixas);
        Assert.Single(await contexto.Db.Assinaturas.IgnoreQueryFilters().ToListAsync());
    }

    [Fact]
    public async Task RecusaForaDeDevelopmentSemAlterarDados()
    {
        await using var contexto = await CriarContextoAsync("Production");

        var erro = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            contexto.Servico.ExecutarAsync(true, CancellationToken.None));

        Assert.Contains("Development", erro.Message);
        Assert.Empty(await contexto.Db.Planos.ToListAsync());
        Assert.Empty(await contexto.Db.Assinaturas.IgnoreQueryFilters().ToListAsync());
    }

    [Fact]
    public async Task ExigeConfirmacaoDoBancoLocal()
    {
        await using var contexto = await CriarContextoAsync();

        var erro = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            contexto.Servico.ExecutarAsync(false, CancellationToken.None));

        Assert.Contains("--confirmar-banco-local", erro.Message);
        Assert.Empty(await contexto.Db.Planos.ToListAsync());
        Assert.Empty(await contexto.Db.Assinaturas.IgnoreQueryFilters().ToListAsync());
    }

    [Fact]
    public async Task RecusaEmpresasAmbiguasSemCriarPlanoOuAssinatura()
    {
        await using var contexto = await CriarContextoAsync();
        contexto.Db.Empresas.Add(new Empresa
        {
            Codigo = "empresa-2",
            RazaoSocial = "Segunda empresa",
            Cnpj = "11222333000181"
        });
        await contexto.Db.SaveChangesAsync();
        contexto.Db.ChangeTracker.Clear();

        var erro = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            contexto.Servico.ExecutarAsync(true, CancellationToken.None));

        Assert.Contains("exatamente uma empresa", erro.Message);
        Assert.Empty(await contexto.Db.Planos.ToListAsync());
        Assert.Empty(await contexto.Db.Assinaturas.IgnoreQueryFilters().ToListAsync());
    }

    [Fact]
    public async Task RecusaAssinaturaExistenteSemCriarOutra()
    {
        await using var contexto = await CriarContextoAsync();
        var plano = CriarPlanoEssencial();
        contexto.Db.Planos.Add(plano);
        contexto.Db.Assinaturas.Add(new Assinatura
        {
            EmpresaId = contexto.EmpresaId,
            PlanoId = plano.Id,
            MaxFiliais = 1,
            MaxCaixas = 2,
            MaxUsuarios = 3,
            ValorMensal = 0m
        });
        await contexto.Db.SaveChangesAsync();
        contexto.Db.ChangeTracker.Clear();

        var erro = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            contexto.Servico.ExecutarAsync(true, CancellationToken.None));

        Assert.Contains("Já existe uma assinatura", erro.Message);
        Assert.Single(await contexto.Db.Planos.ToListAsync());
        Assert.Single(await contexto.Db.Assinaturas.IgnoreQueryFilters().ToListAsync());
    }

    [Fact]
    public async Task RecusaPlanoEssencialDivergenteSemAlteracaoParcial()
    {
        await using var contexto = await CriarContextoAsync();
        var plano = CriarPlanoEssencial();
        plano.MaxCaixas = 4;
        contexto.Db.Planos.Add(plano);
        await contexto.Db.SaveChangesAsync();
        contexto.Db.ChangeTracker.Clear();

        var erro = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            contexto.Servico.ExecutarAsync(true, CancellationToken.None));

        Assert.Contains("diverge do padrão", erro.Message);
        Assert.Single(await contexto.Db.Planos.ToListAsync());
        Assert.Equal(4, (await contexto.Db.Planos.SingleAsync()).MaxCaixas);
        Assert.Empty(await contexto.Db.Assinaturas.IgnoreQueryFilters().ToListAsync());
    }

    private static Plano CriarPlanoEssencial() => new()
    {
        Nome = "Essencial",
        MaxFiliais = 1,
        MaxCaixas = 2,
        MaxUsuarios = 3
    };

    private static async Task<ContextoTeste> CriarContextoAsync(string ambiente = "Development")
    {
        var empresaContexto = new EmpresaContextoLogin();
        var usuarioAtual = new UsuarioAtualFalso(empresaContexto);
        var opcoes = new DbContextOptionsBuilder<PdvDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .AddInterceptors(new AuditoriaInterceptor(usuarioAtual))
            .Options;
        var db = new PdvDbContext(opcoes, usuarioAtual);
        var empresa = new Empresa
        {
            Codigo = "empresa-teste",
            RazaoSocial = "Empresa de teste",
            Cnpj = "11222333000181"
        };
        empresaContexto.Definir(empresa.Id);
        db.Empresas.Add(empresa);
        db.Filiais.Add(new Filial
        {
            EmpresaId = empresa.Id,
            Codigo = "filial-1",
            Nome = "Matriz"
        });
        await db.SaveChangesAsync();
        db.ChangeTracker.Clear();

        var servico = new ProvisionamentoAssinaturaLocalService(
            db, empresaContexto, new AmbienteTeste(ambiente), TimeProvider.System);
        return new ContextoTeste(db, servico, empresa.Id);
    }

    private sealed class ContextoTeste(
        PdvDbContext db,
        ProvisionamentoAssinaturaLocalService servico,
        Guid empresaId) : IAsyncDisposable
    {
        public PdvDbContext Db { get; } = db;
        public ProvisionamentoAssinaturaLocalService Servico { get; } = servico;
        public Guid EmpresaId { get; } = empresaId;
        public ValueTask DisposeAsync() => Db.DisposeAsync();
    }

    private sealed class AmbienteTeste(string ambiente) : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = ambiente;
        public string ApplicationName { get; set; } = "PDV.API.Tests";
        public string ContentRootPath { get; set; } = "";
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }

    private sealed class UsuarioAtualFalso(EmpresaContextoLogin contexto) : IUsuarioAtual
    {
        public Guid? UsuarioId => contexto.UsuarioId;
        public Guid? EmpresaId => contexto.EmpresaId;
        public Guid? FilialId => contexto.FilialId;
    }
}
