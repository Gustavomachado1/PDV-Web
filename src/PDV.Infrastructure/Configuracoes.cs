using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDV.Domain;

namespace PDV.Infrastructure;

internal class EmpresaConfig : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> b)
    {
        b.Property(x => x.Codigo).HasMaxLength(30).IsRequired();
        b.Property(x => x.RazaoSocial).HasMaxLength(200).IsRequired();
        b.Property(x => x.NomeFantasia).HasMaxLength(200);
        b.Property(x => x.Cnpj).HasMaxLength(14).IsRequired();
        b.HasIndex(x => x.Codigo).IsUnique();
        b.HasIndex(x => x.Cnpj).IsUnique();
    }
}

internal class FilialConfig : IEntityTypeConfiguration<Filial>
{
    public void Configure(EntityTypeBuilder<Filial> b)
    {
        b.Property(x => x.Codigo).HasMaxLength(30).IsRequired();
        b.Property(x => x.Nome).HasMaxLength(200).IsRequired();
        b.HasIndex(x => new { x.EmpresaId, x.Codigo }).IsUnique();
        b.HasOne(x => x.Empresa).WithMany(e => e.Filiais).HasForeignKey(x => x.EmpresaId);
    }
}

internal class DepartamentoConfig : IEntityTypeConfiguration<Departamento>
{
    public void Configure(EntityTypeBuilder<Departamento> b)
    {
        b.Property(x => x.Nome).HasMaxLength(100).IsRequired();
        b.Property(x => x.Segmento).HasMaxLength(100);
        b.HasAlternateKey(x => new { x.EmpresaId, x.Id });
        b.HasIndex(x => new { x.EmpresaId, x.Nome }).IsUnique();
    }
}

internal class CategoriaConfig : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> b)
    {
        b.Property(x => x.Nome).HasMaxLength(100).IsRequired();
        b.HasAlternateKey(x => new { x.EmpresaId, x.Id });
        b.HasIndex(x => new { x.EmpresaId, x.DepartamentoId, x.Nome }).IsUnique();
        b.HasOne(x => x.Departamento)
            .WithMany(x => x.Categorias)
            .HasForeignKey(x => new { x.EmpresaId, x.DepartamentoId })
            .HasPrincipalKey(x => new { x.EmpresaId, x.Id });
    }
}

internal class SubcategoriaConfig : IEntityTypeConfiguration<Subcategoria>
{
    public void Configure(EntityTypeBuilder<Subcategoria> b)
    {
        b.Property(x => x.Nome).HasMaxLength(100).IsRequired();
        b.HasAlternateKey(x => new { x.EmpresaId, x.Id });
        b.HasIndex(x => new { x.EmpresaId, x.CategoriaId, x.Nome }).IsUnique();
        b.HasOne(x => x.Categoria)
            .WithMany(x => x.Subcategorias)
            .HasForeignKey(x => new { x.EmpresaId, x.CategoriaId })
            .HasPrincipalKey(x => new { x.EmpresaId, x.Id });
    }
}

internal class ProdutoConfig : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> b)
    {
        b.Property(x => x.Descricao).HasMaxLength(200).IsRequired();
        b.Property(x => x.UnidadeMedida).HasMaxLength(30).IsRequired();
        b.Property(x => x.Status).IsRequired();
        b.Property(x => x.UsoInterno).IsRequired();
        b.HasAlternateKey(x => new { x.EmpresaId, x.Id });
        b.HasIndex(x => new { x.EmpresaId, x.Descricao });
        b.HasOne(x => x.Subcategoria)
            .WithMany(x => x.Produtos)
            .HasForeignKey(x => new { x.EmpresaId, x.SubcategoriaId })
            .HasPrincipalKey(x => new { x.EmpresaId, x.Id });
    }
}

internal class CodigoBarrasProdutoConfig : IEntityTypeConfiguration<CodigoBarrasProduto>
{
    public void Configure(EntityTypeBuilder<CodigoBarrasProduto> b)
    {
        b.Property(x => x.Codigo).HasMaxLength(100).IsRequired();
        b.Property(x => x.Ativo).HasDefaultValue(true);
        b.HasIndex(x => new { x.EmpresaId, x.Codigo }).IsUnique();
        b.HasOne(x => x.Produto)
            .WithMany(x => x.CodigosBarras)
            .HasForeignKey(x => new { x.EmpresaId, x.ProdutoId })
            .HasPrincipalKey(x => new { x.EmpresaId, x.Id });
    }
}

internal class PerfilConfig : IEntityTypeConfiguration<Perfil>
{
    public void Configure(EntityTypeBuilder<Perfil> b)
    {
        b.Property(x => x.Nome).HasMaxLength(100).IsRequired();
        b.HasIndex(x => new { x.EmpresaId, x.Nome }).IsUnique();
    }
}

internal class PermissaoConfig : IEntityTypeConfiguration<Permissao>
{
    public void Configure(EntityTypeBuilder<Permissao> b)
    {
        b.Property(x => x.Codigo).HasMaxLength(100).IsRequired();
        b.Property(x => x.Descricao).HasMaxLength(300).IsRequired();
        b.HasIndex(x => x.Codigo).IsUnique();
    }
}

internal class PerfilPermissaoConfig : IEntityTypeConfiguration<PerfilPermissao>
{
    public void Configure(EntityTypeBuilder<PerfilPermissao> b)
    {
        b.HasIndex(x => new { x.PerfilId, x.PermissaoId }).IsUnique();
        b.HasOne(x => x.Perfil).WithMany(p => p.Permissoes).HasForeignKey(x => x.PerfilId);
        b.HasOne(x => x.Permissao).WithMany().HasForeignKey(x => x.PermissaoId);
    }
}

internal class UsuarioConfig : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> b)
    {
        b.Property(x => x.Nome).HasMaxLength(200).IsRequired();
        b.Property(x => x.Login).HasMaxLength(100).IsRequired();
        b.Property(x => x.Email).HasMaxLength(200);
        b.Property(x => x.SenhaHash).HasMaxLength(500).IsRequired();
        b.Property(x => x.PinHash).HasMaxLength(500);
        b.HasIndex(x => new { x.EmpresaId, x.Login }).IsUnique();
        b.HasOne(x => x.Perfil).WithMany().HasForeignKey(x => x.PerfilId);
    }
}

internal class UsuarioPermissaoConfig : IEntityTypeConfiguration<UsuarioPermissao>
{
    public void Configure(EntityTypeBuilder<UsuarioPermissao> b)
        => b.HasIndex(x => new { x.UsuarioId, x.PermissaoId }).IsUnique();
}

internal class UsuarioFilialConfig : IEntityTypeConfiguration<UsuarioFilial>
{
    public void Configure(EntityTypeBuilder<UsuarioFilial> b)
        => b.HasIndex(x => new { x.UsuarioId, x.FilialId }).IsUnique();
}

internal class SessaoConfig : IEntityTypeConfiguration<Sessao>
{
    public void Configure(EntityTypeBuilder<Sessao> b)
    {
        b.Property(x => x.TokenHash).HasMaxLength(200).IsRequired();
        b.Property(x => x.Dispositivo).HasMaxLength(200);
        b.HasIndex(x => x.TokenHash).IsUnique();
    }
}

internal class LogAutenticacaoConfig : IEntityTypeConfiguration<LogAutenticacao>
{
    public void Configure(EntityTypeBuilder<LogAutenticacao> b)
    {
        b.Property(x => x.Login).HasMaxLength(100).IsRequired();
        b.Property(x => x.MotivoFalha).HasMaxLength(200);
        b.Property(x => x.Ip).HasMaxLength(64);
        b.HasIndex(x => new { x.EmpresaId, x.OcorridoEmUtc });
    }
}

internal class AutorizacaoConfig : IEntityTypeConfiguration<AutorizacaoOperacao>
{
    public void Configure(EntityTypeBuilder<AutorizacaoOperacao> b)
    {
        b.Property(x => x.Operacao).HasMaxLength(100).IsRequired();
        b.Property(x => x.Referencia).HasMaxLength(100);
        b.Property(x => x.Justificativa).HasMaxLength(500).IsRequired();
        b.HasIndex(x => new { x.EmpresaId, x.SolicitadaEmUtc });
    }
}

internal class RegistroAuditoriaConfig : IEntityTypeConfiguration<RegistroAuditoria>
{
    public void Configure(EntityTypeBuilder<RegistroAuditoria> b)
    {
        b.ToTable("Auditoria");
        b.Property(x => x.Operacao).HasMaxLength(100).IsRequired();
        b.Property(x => x.Entidade).HasMaxLength(100).IsRequired();
        b.Property(x => x.EntidadeId).HasMaxLength(100).IsRequired();
        b.Property(x => x.Justificativa).HasMaxLength(500);
        b.HasIndex(x => new { x.EmpresaId, x.OcorridoEmUtc });
        b.HasIndex(x => new { x.EmpresaId, x.Entidade, x.EntidadeId });
    }
}

internal class PlanoConfig : IEntityTypeConfiguration<Plano>
{
    public void Configure(EntityTypeBuilder<Plano> b)
    {
        b.Property(x => x.Nome).HasMaxLength(100).IsRequired();
        b.HasIndex(x => x.Nome).IsUnique();
    }
}

internal class AssinaturaConfig : IEntityTypeConfiguration<Assinatura>
{
    public void Configure(EntityTypeBuilder<Assinatura> b)
    {
        b.Property(x => x.ValorMensal).HasPrecision(18, 2);
        b.Property(x => x.CondicoesComerciais).HasMaxLength(1000);
        b.HasIndex(x => x.EmpresaId);
        b.HasOne(x => x.Plano).WithMany().HasForeignKey(x => x.PlanoId);
    }
}

internal class AssinaturaHistoricoConfig : IEntityTypeConfiguration<AssinaturaHistorico>
{
    public void Configure(EntityTypeBuilder<AssinaturaHistorico> b)
    {
        b.Property(x => x.Descricao).HasMaxLength(500).IsRequired();
        b.HasIndex(x => new { x.AssinaturaId, x.AlteradoEmUtc });
    }
}
