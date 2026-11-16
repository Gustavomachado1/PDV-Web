using Microsoft.EntityFrameworkCore;
using PDV.Infrastructure;

namespace PDV.API.Servicos;

public sealed class AutorizacaoPermissaoService(PdvDbContext db)
{
    public async Task<bool> TemPermissaoAsync(
        Guid usuarioId, string codigo, CancellationToken cancellationToken)
    {
        var permissaoIndividual = await db.UsuariosPermissoes
            .Where(x => x.UsuarioId == usuarioId &&
                db.Permissoes.Any(p => p.Id == x.PermissaoId && p.Codigo == codigo))
            .Select(x => (bool?)x.Concedida)
            .SingleOrDefaultAsync(cancellationToken);

        if (permissaoIndividual is bool concedida)
            return concedida;

        var perfilId = await db.Usuarios
            .Where(x => x.Id == usuarioId)
            .Select(x => (Guid?)x.PerfilId)
            .SingleOrDefaultAsync(cancellationToken);

        return perfilId is Guid id && await db.PerfisPermissoes.AnyAsync(x =>
            x.PerfilId == id &&
            db.Permissoes.Any(p => p.Id == x.PermissaoId && p.Codigo == codigo),
            cancellationToken);
    }
}
