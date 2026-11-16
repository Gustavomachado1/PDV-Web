using PDV.Domain;
using Xunit;

namespace PDV.API.Tests;

public class UnidadesMedidaProdutoTests
{
    [Fact]
    public void CatalogoContemSomenteAsUnidadesDeVarejoDefinidas()
    {
        Assert.Equal(
            new[] { "UN", "KG", "G", "L", "ML", "M", "M²", "CX", "PCT", "FD" },
            UnidadesMedidaProduto.Codigos);
        Assert.All(UnidadesMedidaProduto.Codigos, codigo =>
            Assert.True(UnidadesMedidaProduto.EhValida(codigo)));
    }

    [Theory]
    [InlineData("un")]
    [InlineData("KG ")]
    [InlineData("UNIDADE")]
    [InlineData("")]
    public void RejeitaUnidadesForaDoCatalogo(string codigo)
    {
        Assert.False(UnidadesMedidaProduto.EhValida(codigo));
    }

    [Fact]
    public void PermiteManterUnidadeLegadaSomenteSeNaoForAlterada()
    {
        Assert.True(UnidadesMedidaProduto.PodeAtualizar("UN", "UNIDADE"));
        Assert.True(UnidadesMedidaProduto.PodeAtualizar("UNIDADE", "UNIDADE"));
        Assert.False(UnidadesMedidaProduto.PodeAtualizar("UNIDADE NOVA", "UNIDADE"));
    }
}
