using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PDV.Infrastructure.Migrations;
using Xunit;

namespace PDV.Infrastructure.Tests;

public class EstoqueNegativoPorEmpresaFilialMigrationTests
{
    [Fact]
    public void UpEDownUsamNomeCorretoParaTabelaDePermissoesDePerfil()
    {
        var sqlUp = string.Join(Environment.NewLine, ObterComandos("Up"));
        var sqlDown = string.Join(Environment.NewLine, ObterComandos("Down"));

        Assert.Contains("[PerfisPermissoes]", sqlUp, StringComparison.Ordinal);
        Assert.DoesNotContain("[PerfilPermissoes]", sqlUp, StringComparison.Ordinal);
        Assert.Contains("[PerfisPermissoes]", sqlDown, StringComparison.Ordinal);
        Assert.DoesNotContain("[PerfilPermissoes]", sqlDown, StringComparison.Ordinal);
    }

    private static IEnumerable<string> ObterComandos(string metodo)
    {
        var upDown = typeof(EstoqueNegativoPorEmpresaFilial).GetMethod(
            metodo, BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.NotNull(upDown);

        var migrationBuilder = new MigrationBuilder("Microsoft.EntityFrameworkCore.SqlServer");
        upDown.Invoke(new EstoqueNegativoPorEmpresaFilial(), [migrationBuilder]);

        return migrationBuilder.Operations
            .OfType<SqlOperation>()
            .Select(operacao => operacao.Sql);
    }
}
