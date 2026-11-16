using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PDV.Application;
using PDV.Infrastructure;
using Xunit;

namespace PDV.Infrastructure.Tests;

public class CatalogoPadraoSeederTests
{
    [Fact]
    public async Task PopularCatalogoCompletoERepetirNaoDuplicaRegistros()
    {
        var empresaId = Guid.NewGuid();
        await using var contexto = CriarContexto(empresaId);
        var seeder = new CatalogoPadraoSeeder(contexto, new UsuarioAtualFalso(empresaId));

        var primeiraExecucao = await seeder.PopularAsync(empresaId, null, default);
        var segundaExecucao = await seeder.PopularAsync(empresaId, null, default);

        Assert.Equal(new ResultadoSeedCatalogo(6, 26, 98), primeiraExecucao);
        Assert.Equal(new ResultadoSeedCatalogo(0, 0, 0), segundaExecucao);
        Assert.Equal(6, await contexto.Departamentos.CountAsync());
        Assert.Equal(26, await contexto.Categorias.CountAsync());
        Assert.Equal(98, await contexto.Subcategorias.CountAsync());
        Assert.Contains(await contexto.Departamentos.ToListAsync(),
            x => x.Nome == "Controle Interno" && x.Segmento == "Geral - Uso Interno/Operacional");
    }

    [Fact]
    public async Task FiltrarPorSegmentoImportaSomenteSuaArvoreNaEmpresaAtual()
    {
        var databaseName = Guid.NewGuid().ToString();
        var root = new InMemoryDatabaseRoot();
        var empresaId = Guid.NewGuid();
        var outroId = Guid.NewGuid();
        await using (var contexto = CriarContexto(empresaId, databaseName, root))
        {
            var resultado = await new CatalogoPadraoSeeder(contexto, new UsuarioAtualFalso(empresaId))
                .PopularAsync(empresaId, "moda", default);

            Assert.Equal(new ResultadoSeedCatalogo(1, 5, 20), resultado);
        }

        await using (var contexto = CriarContexto(outroId, databaseName, root))
        {
            Assert.Empty(await contexto.Departamentos.ToListAsync());
            var seeder = new CatalogoPadraoSeeder(contexto, new UsuarioAtualFalso(outroId));
            await Assert.ThrowsAsync<ArgumentException>(() =>
                seeder.PopularAsync(outroId, "segmento-inexistente", default));
            Assert.Empty(await contexto.Departamentos.ToListAsync());
        }
    }

    private static PdvDbContext CriarContexto(
        Guid empresaId, string? databaseName = null, InMemoryDatabaseRoot? root = null)
    {
        var usuarioAtual = new UsuarioAtualFalso(empresaId);
        var options = new DbContextOptionsBuilder<PdvDbContext>()
            .UseInMemoryDatabase(databaseName ?? Guid.NewGuid().ToString(), root ?? new InMemoryDatabaseRoot())
            .AddInterceptors(new AuditoriaInterceptor(usuarioAtual))
            .Options;
        return new PdvDbContext(options, usuarioAtual);
    }

    private sealed class UsuarioAtualFalso(Guid empresaId) : IUsuarioAtual
    {
        public Guid? UsuarioId => null;
        public Guid? EmpresaId => empresaId;
        public Guid? FilialId => null;
    }
}
