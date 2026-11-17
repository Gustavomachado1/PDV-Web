using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDV.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EstoqueNegativoPorEmpresaFilial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PermiteEstoqueNegativo",
                table: "Filiais",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PermiteEstoqueNegativo",
                table: "Empresas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql("""
                IF EXISTS (SELECT 1 FROM [Empresas])
                BEGIN
                    IF NOT EXISTS (SELECT 1 FROM [Permissoes] WHERE [Codigo] = N'estoque.negativo.configurar')
                        INSERT INTO [Permissoes] ([Id], [Codigo], [Descricao], [CriadoEmUtc])
                        VALUES (NEWID(), N'estoque.negativo.configurar', N'Configurar a regra de estoque negativo', SYSUTCDATETIME());

                    IF NOT EXISTS (SELECT 1 FROM [Permissoes] WHERE [Codigo] = N'estoque.negativo.permitir')
                        INSERT INTO [Permissoes] ([Id], [Codigo], [Descricao], [CriadoEmUtc])
                        VALUES (NEWID(), N'estoque.negativo.permitir', N'Autorizar operações que resultem em estoque negativo', SYSUTCDATETIME());

                    INSERT INTO [PerfisPermissoes] ([Id], [EmpresaId], [PerfilId], [PermissaoId], [CriadoEmUtc])
                    SELECT NEWID(), perfil.[EmpresaId], perfil.[Id], permissao.[Id], SYSUTCDATETIME()
                    FROM [Perfis] AS perfil
                    CROSS JOIN [Permissoes] AS permissao
                    WHERE perfil.[Tipo] = 1
                      AND permissao.[Codigo] IN (N'estoque.negativo.configurar', N'estoque.negativo.permitir')
                      AND NOT EXISTS (
                          SELECT 1
                          FROM [PerfisPermissoes] AS existente
                          WHERE existente.[PerfilId] = perfil.[Id]
                            AND existente.[PermissaoId] = permissao.[Id]);
                END
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DELETE usuarioPermissao
                FROM [UsuariosPermissoes] AS usuarioPermissao
                INNER JOIN [Permissoes] AS permissao ON permissao.[Id] = usuarioPermissao.[PermissaoId]
                WHERE permissao.[Codigo] IN (N'estoque.negativo.configurar', N'estoque.negativo.permitir');

                DELETE perfilPermissao
                FROM [PerfisPermissoes] AS perfilPermissao
                INNER JOIN [Permissoes] AS permissao ON permissao.[Id] = perfilPermissao.[PermissaoId]
                WHERE permissao.[Codigo] IN (N'estoque.negativo.configurar', N'estoque.negativo.permitir');

                DELETE FROM [Permissoes]
                WHERE [Codigo] IN (N'estoque.negativo.configurar', N'estoque.negativo.permitir');
                """);

            migrationBuilder.DropColumn(
                name: "PermiteEstoqueNegativo",
                table: "Filiais");

            migrationBuilder.DropColumn(
                name: "PermiteEstoqueNegativo",
                table: "Empresas");
        }
    }
}
