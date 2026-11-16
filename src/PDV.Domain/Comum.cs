namespace PDV.Domain;

/// <summary>
/// Base de todas as entidades. O Id é um Guid gerado na origem (não depende do banco),
/// requisito para a futura operação offline. RowVersion é o token de concorrência otimista.
/// </summary>
public abstract class EntidadeBase
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public DateTime CriadoEmUtc { get; set; } = DateTime.UtcNow;
    public byte[] RowVersion { get; set; } = [];
}

/// <summary>Entidade que pertence a uma empresa (tenant). Recebe filtro global de isolamento (RF015).</summary>
public interface IEmpresaScoped
{
    Guid EmpresaId { get; set; }
}

public interface IFilialScoped
{
    Guid FilialId { get; set; }
}

/// <summary>Entidade cujas alterações são registradas na trilha de auditoria (RF018/RF019).</summary>
public interface IAuditavel
{
}
