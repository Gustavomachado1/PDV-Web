using PDV.Domain;

namespace PDV.API.Tests;

public class TipoMovimentoEstoqueTests
{
    [Theory]
    [InlineData(TipoMovimentoEstoque.EntradaManual, 1)]
    [InlineData(TipoMovimentoEstoque.SaidaManual, -1)]
    public void Sinal_retorna_o_valor_esperado(TipoMovimentoEstoque tipo, int esperado)
        => Assert.Equal((decimal)esperado, tipo.Sinal());

    [Fact]
    public void Sinal_lanca_excecao_para_ajuste_de_inventario()
        => Assert.Throws<InvalidOperationException>(
            () => TipoMovimentoEstoque.AjusteInventario.Sinal());
}
