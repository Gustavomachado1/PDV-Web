namespace PDV.Domain;

public enum StatusProduto { Ativo = 1, Inativo = 2, Bloqueado = 3 }

public class Departamento : EntidadeBase, IEmpresaScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public string Nome { get; set; } = "";
    public string? Segmento { get; set; }
    public ICollection<Categoria> Categorias { get; set; } = [];
}

public class Categoria : EntidadeBase, IEmpresaScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public Guid DepartamentoId { get; set; }
    public string Nome { get; set; } = "";
    public Departamento? Departamento { get; set; }
    public ICollection<Subcategoria> Subcategorias { get; set; } = [];
}

public class Subcategoria : EntidadeBase, IEmpresaScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public Guid CategoriaId { get; set; }
    public string Nome { get; set; } = "";
    public Categoria? Categoria { get; set; }
    public ICollection<Produto> Produtos { get; set; } = [];
}

public class Produto : EntidadeBase, IEmpresaScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public Guid SubcategoriaId { get; set; }
    public string Descricao { get; set; } = "";
    public string UnidadeMedida { get; set; } = "";
    public StatusProduto Status { get; set; } = StatusProduto.Ativo;
    public bool UsoInterno { get; set; }
    public Subcategoria? Subcategoria { get; set; }
    public ICollection<CodigoBarrasProduto> CodigosBarras { get; set; } = [];
}

public class CodigoBarrasProduto : EntidadeBase, IEmpresaScoped, IAuditavel
{
    public Guid EmpresaId { get; set; }
    public Guid ProdutoId { get; set; }
    public string Codigo { get; set; } = "";
    public bool Ativo { get; set; } = true;
    public Produto? Produto { get; set; }
}
