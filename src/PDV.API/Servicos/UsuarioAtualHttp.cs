using PDV.Application;

namespace PDV.API.Servicos;

/// <summary>
/// Lê usuário, empresa e filial das claims da requisição. Durante o login, usa temporariamente
/// a empresa já resolvida para escopar somente os registros da tentativa de autenticação.
/// </summary>
public class UsuarioAtualHttp(IHttpContextAccessor accessor, EmpresaContextoLogin empresaContexto) : IUsuarioAtual
{
    public const string ClaimUsuario = "sub";
    public const string ClaimEmpresa = "empresa_id";
    public const string ClaimFilial = "filial_id";

    public Guid? UsuarioId => empresaContexto.EmpresaId is not null ? empresaContexto.UsuarioId : Ler(ClaimUsuario);
    public Guid? EmpresaId => empresaContexto.EmpresaId ?? Ler(ClaimEmpresa);
    public Guid? FilialId => empresaContexto.EmpresaId is not null ? empresaContexto.FilialId : Ler(ClaimFilial);

    private Guid? Ler(string tipo)
    {
        var valor = accessor.HttpContext?.User.FindFirst(tipo)?.Value;
        return Guid.TryParse(valor, out var id) ? id : null;
    }
}
