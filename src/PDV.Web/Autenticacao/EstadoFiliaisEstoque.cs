namespace PDV.Web.Autenticacao;

public static class EstadoFiliaisEstoque
{
    public enum Estado
    {
        Carregando,
        Falha,
        SemAcesso,
        Disponivel
    }

    public static Estado Avaliar(bool carregando, bool falha, int quantidadeFiliais)
    {
        if (carregando)
            return Estado.Carregando;
        if (falha)
            return Estado.Falha;
        return quantidadeFiliais == 0 ? Estado.SemAcesso : Estado.Disponivel;
    }
}
