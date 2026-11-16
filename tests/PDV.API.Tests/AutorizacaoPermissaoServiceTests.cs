using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PDV.API.Servicos;
using PDV.Application;
using PDV.Domain;
using PDV.Infrastructure;
using Xunit;

namespace PDV.API.Tests;

public class AutorizacaoPermissaoServiceTests
{
    [Fact]
    public async Task PermissaoDoPerfilPodeSerRevogadaOuConcedidaPorExcecaoDoUsuario()
    {
        var empresaId = Guid.NewGuid();
        var outroUsuario = Guid.NewGuid();
        var perfil = new Perfil
        {
            EmpresaId = empresaId,
            Nome = "Administrador",
            Tipo = TipoPerfil.Administrador
        };
        var usuario = new Usuario
        {
            Id = outroUsuario,
            EmpresaId = empresaId,
            Nome = "Admin",
            Login = "admin",
            SenhaHash = "hash",
            PerfilId = perfil.Id
        };
        var permissao = new Permissao
        {
            Codigo = "catalogo.gerenciar",
            Descricao = "Gerenciar catálogo"
        };
        var nomeBanco = Guid.NewGuid().ToString();
        var databaseRoot = new InMemoryDatabaseRoot();
        var options = new DbContextOptionsBuilder<PdvDbContext>()
            .UseInMemoryDatabase(nomeBanco, databaseRoot)
            .Options;
        var usuarioAtual = new UsuarioAtualFalso(empresaId);
        await using var contexto = new PdvDbContext(options, usuarioAtual);
        contexto.Perfis.Add(perfil);
        contexto.Usuarios.Add(usuario);
        contexto.Permissoes.Add(permissao);
        contexto.PerfisPermissoes.Add(new PerfilPermissao
        {
            EmpresaId = empresaId,
            PerfilId = perfil.Id,
            PermissaoId = permissao.Id
        });
        await contexto.SaveChangesAsync();

        var autorizacao = new AutorizacaoPermissaoService(contexto);
        Assert.True(await autorizacao.TemPermissaoAsync(usuario.Id, "catalogo.gerenciar", default));

        contexto.UsuariosPermissoes.Add(new UsuarioPermissao
        {
            EmpresaId = empresaId,
            UsuarioId = usuario.Id,
            PermissaoId = permissao.Id,
            Concedida = false
        });
        await contexto.SaveChangesAsync();
        Assert.False(await autorizacao.TemPermissaoAsync(usuario.Id, "catalogo.gerenciar", default));

        contexto.UsuariosPermissoes.Single().Concedida = true;
        await contexto.SaveChangesAsync();
        Assert.True(await autorizacao.TemPermissaoAsync(usuario.Id, "catalogo.gerenciar", default));

        var usuarioOutraEmpresa = new UsuarioAtualFalso(Guid.NewGuid());
        var opcoesOutraEmpresa = new DbContextOptionsBuilder<PdvDbContext>()
            .UseInMemoryDatabase(nomeBanco, databaseRoot)
            .Options;
        await using var contextoOutraEmpresa = new PdvDbContext(opcoesOutraEmpresa, usuarioOutraEmpresa);
        Assert.False(await new AutorizacaoPermissaoService(contextoOutraEmpresa)
            .TemPermissaoAsync(usuario.Id, "catalogo.gerenciar", default));
    }

    private sealed class UsuarioAtualFalso(Guid empresaId) : IUsuarioAtual
    {
        public Guid? UsuarioId => null;
        public Guid? EmpresaId => empresaId;
        public Guid? FilialId => null;
    }
}
