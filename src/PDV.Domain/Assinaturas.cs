namespace PDV.Domain;

public enum SituacaoAssinatura { Ativa = 1, Suspensa = 2, Cancelada = 3 }

/// <summary>Catálogo de planos (global). Cobrança, renovação e suspensão são manuais no MVP.</summary>
public class Plano : EntidadeBase
{
    public string Nome { get; set; } = "";
    public int MaxFiliais { get; set; }
    public int MaxCaixas { get; set; }
    public int MaxUsuarios { get; set; }
    public bool Ativo { get; set; } = true;
}

public class Assinatura : EntidadeBase, IEmpresaScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public Guid PlanoId { get; set; }
    public SituacaoAssinatura Situacao { get; set; } = SituacaoAssinatura.Ativa;
    // Limites e condições efetivamente contratados (podem diferir do plano).
    public int MaxFiliais { get; set; }
    public int MaxCaixas { get; set; }
    public int MaxUsuarios { get; set; }
    public decimal ValorMensal { get; set; }
    public string? CondicoesComerciais { get; set; }
    public DateTime InicioEmUtc { get; set; } = DateTime.UtcNow;
    public DateTime? FimEmUtc { get; set; }
    public Plano? Plano { get; set; }
}

public class AssinaturaHistorico : EntidadeBase, IEmpresaScoped
{
    public Guid EmpresaId { get; set; }
    public Guid AssinaturaId { get; set; }
    public string Descricao { get; set; } = "";
    public string? ValoresAnteriores { get; set; }
    public string? ValoresPosteriores { get; set; }
    public Guid? AlteradoPorUsuarioId { get; set; }
    public DateTime AlteradoEmUtc { get; set; } = DateTime.UtcNow;
}
