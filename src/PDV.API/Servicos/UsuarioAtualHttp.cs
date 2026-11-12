using PDV.Application;

namespace PDV.API.Servicos;

/// <summary>
/// Lê usuário, empresa e filial das claims da requisição. A autenticação (RF001) ainda não
/// está implementada: sem claims, o contexto fica vazio e os filtros de empresa não retornam dados.
/// </summary>
public class UsuarioAtualHttp(IHttpContextAccessor accessor) : IUsuarioAtual
{
    public const string ClaimUsuario = "sub";
    public const string ClaimEmpresa = "empresa_id";
    public const string ClaimFilial = "filial_id";

    public Guid? UsuarioId => Ler(ClaimUsuario);
    public Guid? EmpresaId => Ler(ClaimEmpresa);
    public Guid? FilialId => Ler(ClaimFilial);

    private Guid? Ler(string tipo)
    {
        var valor = accessor.HttpContext?.User.FindFirst(tipo)?.Value;
        return Guid.TryParse(valor, out var id) ? id : null;
    }
}
