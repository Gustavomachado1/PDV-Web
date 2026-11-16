namespace PDV.Domain;

public static class UnidadesMedidaProduto
{
    public static IReadOnlyList<string> Codigos { get; } = Array.AsReadOnly(
        new[] { "UN", "KG", "G", "L", "ML", "M", "M²", "CX", "PCT", "FD" });

    public static bool EhValida(string? codigo)
        => Codigos.Contains(codigo, StringComparer.Ordinal);

    public static bool PodeAtualizar(string? codigo, string codigoAtual)
        => EhValida(codigo) || string.Equals(codigo, codigoAtual, StringComparison.Ordinal);
}
