namespace PdvWeb.Domain;

/// <summary>
/// Registro de auditoria. É somente-inserção: o interceptor recusa qualquer alteração ou
/// exclusão (RF024). OcorridoEmUtc é o momento na origem; RecebidoEmUtc é quando o servidor gravou.
/// </summary>
public class RegistroAuditoria : EntidadeBase, IEmpresaScoped
{
    public Guid EmpresaId { get; set; }
    public Guid? FilialId { get; set; }
    public Guid? UsuarioId { get; set; }
    public Guid? AutorizadorId { get; set; }
    public string Operacao { get; set; } = "";         // ex.: "Usuario.Alterar"
    public string Entidade { get; set; } = "";
    public string EntidadeId { get; set; } = "";
    public string? ValoresAnteriores { get; set; }     // JSON (RF019)
    public string? ValoresPosteriores { get; set; }    // JSON
    public string? Justificativa { get; set; }
    public DateTime OcorridoEmUtc { get; set; } = DateTime.UtcNow;
    public DateTime RecebidoEmUtc { get; set; } = DateTime.UtcNow;
}
