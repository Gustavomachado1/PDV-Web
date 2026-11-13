using Microsoft.EntityFrameworkCore;
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

    private static PdvDbContext CriarContexto(Guid? empresaId)
    {
        var usuarioAtual = new UsuarioAtualFalso(empresaId);
        var interceptor = new AuditoriaInterceptor(usuarioAtual);
        var opcoes = new DbContextOptionsBuilder<PdvDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
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
