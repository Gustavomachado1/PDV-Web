namespace PdvWeb.Domain;

public class Empresa : EntidadeBase, IAuditavel
{
    public string Codigo { get; set; } = "";          // identifica a empresa na tela de login
    public string RazaoSocial { get; set; } = "";
    public string? NomeFantasia { get; set; }
    public string Cnpj { get; set; } = "";            // somente dígitos
    public bool Ativa { get; set; } = true;
    public ICollection<Filial> Filiais { get; set; } = [];
}

public class Filial : EntidadeBase, IEmpresaScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public string Codigo { get; set; } = "";
    public string Nome { get; set; } = "";
    public bool Ativa { get; set; } = true;
    public Empresa? Empresa { get; set; }
}
