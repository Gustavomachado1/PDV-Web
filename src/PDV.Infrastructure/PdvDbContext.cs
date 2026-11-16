using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PDV.Application;
using PDV.Domain;

namespace PDV.Infrastructure;

public class PdvDbContext(DbContextOptions<PdvDbContext> options, IUsuarioAtual usuarioAtual)
    : DbContext(options)
{
    // Guid.Empty não corresponde a nenhuma empresa: sem tenant, nada é retornado.
    private Guid EmpresaAtualId => usuarioAtual.EmpresaId ?? Guid.Empty;

    public DbSet<Empresa> Empresas => Set<Empresa>();
    public DbSet<Filial> Filiais => Set<Filial>();
    public DbSet<Departamento> Departamentos => Set<Departamento>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Subcategoria> Subcategorias => Set<Subcategoria>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<CodigoBarrasProduto> CodigosBarrasProdutos => Set<CodigoBarrasProduto>();
    public DbSet<SaldoEstoque> SaldosEstoque => Set<SaldoEstoque>();
    public DbSet<MovimentoEstoque> MovimentosEstoque => Set<MovimentoEstoque>();
    public DbSet<InventarioEstoque> InventariosEstoque => Set<InventarioEstoque>();
    public DbSet<ItemInventarioEstoque> ItensInventarioEstoque => Set<ItemInventarioEstoque>();
    public DbSet<Perfil> Perfis => Set<Perfil>();
    public DbSet<Permissao> Permissoes => Set<Permissao>();
    public DbSet<PerfilPermissao> PerfisPermissoes => Set<PerfilPermissao>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<UsuarioPermissao> UsuariosPermissoes => Set<UsuarioPermissao>();
    public DbSet<UsuarioFilial> UsuariosFiliais => Set<UsuarioFilial>();
    public DbSet<Sessao> Sessoes => Set<Sessao>();
    public DbSet<LogAutenticacao> LogsAutenticacao => Set<LogAutenticacao>();
    public DbSet<AutorizacaoOperacao> Autorizacoes => Set<AutorizacaoOperacao>();
    public DbSet<RegistroAuditoria> Auditoria => Set<RegistroAuditoria>();
    public DbSet<Plano> Planos => Set<Plano>();
    public DbSet<Assinatura> Assinaturas => Set<Assinatura>();
    public DbSet<AssinaturaHistorico> AssinaturasHistorico => Set<AssinaturaHistorico>();

    private static readonly MethodInfo FiltroEmpresaMetodo =
        typeof(PdvDbContext).GetMethod(nameof(AplicarFiltroEmpresa), BindingFlags.Instance | BindingFlags.NonPublic)!;

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.ApplyConfigurationsFromAssembly(typeof(PdvDbContext).Assembly);

        mb.Entity<Empresa>().HasQueryFilter(e => e.Id == EmpresaAtualId);

        foreach (var tipo in mb.Model.GetEntityTypes().Select(t => t.ClrType).ToList())
        {
            if (typeof(IEmpresaScoped).IsAssignableFrom(tipo))
                FiltroEmpresaMetodo.MakeGenericMethod(tipo).Invoke(this, new object[] { mb });

            if (typeof(EntidadeBase).IsAssignableFrom(tipo))
                mb.Entity(tipo).Property(nameof(EntidadeBase.RowVersion)).IsRowVersion();
        }

        // SQL Server recusa múltiplos caminhos de cascata; exclusões ficam explícitas.
        foreach (var fk in mb.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()))
            fk.DeleteBehavior = DeleteBehavior.Restrict;
    }

    private void AplicarFiltroEmpresa<T>(ModelBuilder mb) where T : class, IEmpresaScoped
        => mb.Entity<T>().HasQueryFilter(e => e.EmpresaId == EmpresaAtualId);
}
