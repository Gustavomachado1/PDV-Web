using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PDV.Application;
using PDV.Domain;

namespace PDV.Infrastructure;

/// <summary>
/// Executa a cada SaveChanges: (1) carimba/valida a empresa das entidades novas,
/// (2) impede alterar ou excluir registros de auditoria, (3) grava a trilha de auditoria
/// com valores anteriores e posteriores. Dados sensíveis (campos com "Hash") não são gravados.
/// </summary>
public sealed class AuditoriaInterceptor(IUsuarioAtual usuarioAtual) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        Processar(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        Processar(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void Processar(DbContext? contexto)
    {
        if (contexto is null) return;

        var agora = DateTime.UtcNow;
        var novos = new List<RegistroAuditoria>();

        foreach (var entry in contexto.ChangeTracker.Entries().ToList())
        {
            if (entry.Entity is RegistroAuditoria && (entry.State is EntityState.Modified or EntityState.Deleted))
                throw new InvalidOperationException("Registros de auditoria não podem ser alterados ou excluídos.");

            if (entry.Entity is IEmpresaScoped && entry.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            {
                if (usuarioAtual.EmpresaId is not Guid atual || atual == Guid.Empty)
                    throw new InvalidOperationException("Não é possível gravar dados de empresa sem uma empresa válida no contexto atual.");

                var empresa = entry.Property(nameof(IEmpresaScoped.EmpresaId));
                if (entry.State == EntityState.Added && Equals(empresa.CurrentValue, Guid.Empty))
                {
                    empresa.CurrentValue = atual;
                }
                else if (!Equals(empresa.CurrentValue, atual) ||
                         (entry.State != EntityState.Added && !Equals(empresa.OriginalValue, atual)))
                {
                    throw new InvalidOperationException("Tentativa de gravar dados em outra empresa.");
                }
            }

            if (entry.Entity is not IAuditavel || entry.Entity is not EntidadeBase entidade) continue;
            if (entry.State is not (EntityState.Added or EntityState.Modified or EntityState.Deleted)) continue;

            Guid? empresaId = entry.Entity switch
            {
                IEmpresaScoped e => e.EmpresaId,
                Empresa emp => emp.Id,
                _ => null
            };
            if (empresaId is null) continue;

            var anteriores = new Dictionary<string, object?>();
            var posteriores = new Dictionary<string, object?>();

            foreach (var p in entry.Properties)
            {
                var nome = p.Metadata.Name;
                if (nome == nameof(EntidadeBase.RowVersion) || nome.Contains("Hash")) continue;

                switch (entry.State)
                {
                    case EntityState.Added:
                        posteriores[nome] = p.CurrentValue;
                        break;
                    case EntityState.Deleted:
                        anteriores[nome] = p.OriginalValue;
                        break;
                    case EntityState.Modified when p.IsModified:
                        anteriores[nome] = p.OriginalValue;
                        posteriores[nome] = p.CurrentValue;
                        break;
                }
            }

            if (entry.State == EntityState.Modified && posteriores.Count == 0) continue;

            var tipo = entry.Entity.GetType().Name;
            var acao = entry.State switch
            {
                EntityState.Added => "Criar",
                EntityState.Deleted => "Excluir",
                _ => "Alterar"
            };

            novos.Add(new RegistroAuditoria
            {
                EmpresaId = empresaId.Value,
                FilialId = usuarioAtual.FilialId,
                UsuarioId = usuarioAtual.UsuarioId,
                Operacao = $"{tipo}.{acao}",
                Entidade = tipo,
                EntidadeId = entidade.Id.ToString(),
                ValoresAnteriores = anteriores.Count > 0 ? JsonSerializer.Serialize(anteriores) : null,
                ValoresPosteriores = posteriores.Count > 0 ? JsonSerializer.Serialize(posteriores) : null,
                OcorridoEmUtc = agora,
                RecebidoEmUtc = agora
            });
        }

        if (novos.Count > 0) contexto.Set<RegistroAuditoria>().AddRange(novos);
    }
}
