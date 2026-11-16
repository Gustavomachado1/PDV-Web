using System.Net;

namespace PDV.Web.Autenticacao;

internal static class MensagemErroHttp
{
    public static string Para(HttpStatusCode statusCode) => statusCode switch
    {
        HttpStatusCode.BadRequest => "Revise os dados informados e tente novamente.",
        HttpStatusCode.NotFound => "O registro solicitado não foi encontrado.",
        HttpStatusCode.Conflict => "O registro foi alterado. Atualize os dados e tente novamente.",
        _ when (int)statusCode >= 500 =>
            "O serviço encontrou um erro ao processar a solicitação. Tente novamente mais tarde.",
        _ => "Não foi possível concluir a solicitação. Tente novamente."
    };
}
