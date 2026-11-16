using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDV.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EstoquePorFilial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Justificativa",
                table: "Autorizacoes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Filiais_EmpresaId_Id",
                table: "Filiais",
                columns: new[] { "EmpresaId", "Id" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Autorizacoes_EmpresaId_Id",
                table: "Autorizacoes",
                columns: new[] { "EmpresaId", "Id" });

            migrationBuilder.CreateTable(
                name: "InventariosEstoque",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Justificativa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ConcluidoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CriadoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventariosEstoque", x => x.Id);
                    table.UniqueConstraint("AK_InventariosEstoque_EmpresaId_Id", x => new { x.EmpresaId, x.Id });
                    table.ForeignKey(
                        name: "FK_InventariosEstoque_Filiais_EmpresaId_FilialId",
                        columns: x => new { x.EmpresaId, x.FilialId },
                        principalTable: "Filiais",
                        principalColumns: new[] { "EmpresaId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaldosEstoque",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    EstoqueMinimo = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    EstoqueIdeal = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    CriadoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaldosEstoque", x => x.Id);
                    table.UniqueConstraint("AK_SaldosEstoque_EmpresaId_Id", x => new { x.EmpresaId, x.Id });
                    table.ForeignKey(
                        name: "FK_SaldosEstoque_Filiais_EmpresaId_FilialId",
                        columns: x => new { x.EmpresaId, x.FilialId },
                        principalTable: "Filiais",
                        principalColumns: new[] { "EmpresaId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaldosEstoque_Produtos_EmpresaId_ProdutoId",
                        columns: x => new { x.EmpresaId, x.ProdutoId },
                        principalTable: "Produtos",
                        principalColumns: new[] { "EmpresaId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItensInventarioEstoque",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventarioEstoqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaldoAnterior = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    QuantidadeContada = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    Diferenca = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    CriadoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensInventarioEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensInventarioEstoque_InventariosEstoque_EmpresaId_InventarioEstoqueId",
                        columns: x => new { x.EmpresaId, x.InventarioEstoqueId },
                        principalTable: "InventariosEstoque",
                        principalColumns: new[] { "EmpresaId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItensInventarioEstoque_Produtos_EmpresaId_ProdutoId",
                        columns: x => new { x.EmpresaId, x.ProdutoId },
                        principalTable: "Produtos",
                        principalColumns: new[] { "EmpresaId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovimentosEstoque",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventarioEstoqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AutorizacaoOperacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SolicitanteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AutorizadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    SaldoAnterior = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    SaldoResultante = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    Justificativa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SolicitadoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcluidoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentosEstoque", x => x.Id);
                    table.UniqueConstraint("AK_MovimentosEstoque_EmpresaId_Id", x => new { x.EmpresaId, x.Id });
                    table.ForeignKey(
                        name: "FK_MovimentosEstoque_Autorizacoes_EmpresaId_AutorizacaoOperacaoId",
                        columns: x => new { x.EmpresaId, x.AutorizacaoOperacaoId },
                        principalTable: "Autorizacoes",
                        principalColumns: new[] { "EmpresaId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovimentosEstoque_Filiais_EmpresaId_FilialId",
                        columns: x => new { x.EmpresaId, x.FilialId },
                        principalTable: "Filiais",
                        principalColumns: new[] { "EmpresaId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovimentosEstoque_InventariosEstoque_EmpresaId_InventarioEstoqueId",
                        columns: x => new { x.EmpresaId, x.InventarioEstoqueId },
                        principalTable: "InventariosEstoque",
                        principalColumns: new[] { "EmpresaId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovimentosEstoque_Produtos_EmpresaId_ProdutoId",
                        columns: x => new { x.EmpresaId, x.ProdutoId },
                        principalTable: "Produtos",
                        principalColumns: new[] { "EmpresaId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventariosEstoque_EmpresaId_FilialId_ConcluidoEmUtc",
                table: "InventariosEstoque",
                columns: new[] { "EmpresaId", "FilialId", "ConcluidoEmUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_ItensInventarioEstoque_EmpresaId_InventarioEstoqueId_ProdutoId",
                table: "ItensInventarioEstoque",
                columns: new[] { "EmpresaId", "InventarioEstoqueId", "ProdutoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItensInventarioEstoque_EmpresaId_ProdutoId",
                table: "ItensInventarioEstoque",
                columns: new[] { "EmpresaId", "ProdutoId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosEstoque_AutorizacaoOperacaoId",
                table: "MovimentosEstoque",
                column: "AutorizacaoOperacaoId",
                unique: true,
                filter: "[AutorizacaoOperacaoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosEstoque_EmpresaId_AutorizacaoOperacaoId",
                table: "MovimentosEstoque",
                columns: new[] { "EmpresaId", "AutorizacaoOperacaoId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosEstoque_EmpresaId_FilialId_ProdutoId_SolicitadoEmUtc",
                table: "MovimentosEstoque",
                columns: new[] { "EmpresaId", "FilialId", "ProdutoId", "SolicitadoEmUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosEstoque_EmpresaId_InventarioEstoqueId",
                table: "MovimentosEstoque",
                columns: new[] { "EmpresaId", "InventarioEstoqueId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosEstoque_EmpresaId_ProdutoId",
                table: "MovimentosEstoque",
                columns: new[] { "EmpresaId", "ProdutoId" });

            migrationBuilder.CreateIndex(
                name: "IX_SaldosEstoque_EmpresaId_FilialId_ProdutoId",
                table: "SaldosEstoque",
                columns: new[] { "EmpresaId", "FilialId", "ProdutoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaldosEstoque_EmpresaId_ProdutoId",
                table: "SaldosEstoque",
                columns: new[] { "EmpresaId", "ProdutoId" });

            migrationBuilder.Sql("""
                IF EXISTS (SELECT 1 FROM [Perfis])
                BEGIN
                    DECLARE @Permissoes TABLE (
                        [Id] uniqueidentifier NOT NULL,
                        [Codigo] nvarchar(100) NOT NULL,
                        [Descricao] nvarchar(300) NOT NULL);

                    INSERT INTO @Permissoes ([Id], [Codigo], [Descricao]) VALUES
                        ('a0000000-0000-7000-8000-000000000001', N'estoque.consultar', N'Consultar estoque e movimentações'),
                        ('a0000000-0000-7000-8000-000000000002', N'estoque.configurar', N'Configurar estoque por filial'),
                        ('a0000000-0000-7000-8000-000000000003', N'estoque.movimentar.solicitar', N'Solicitar movimentação manual de estoque'),
                        ('a0000000-0000-7000-8000-000000000004', N'estoque.movimento.entrada.autorizar', N'Autorizar entradas manuais de estoque'),
                        ('a0000000-0000-7000-8000-000000000005', N'estoque.movimento.saida.autorizar', N'Autorizar saídas manuais de estoque'),
                        ('a0000000-0000-7000-8000-000000000006', N'estoque.inventario.gerenciar', N'Realizar e concluir inventários');

                    INSERT INTO [Permissoes] ([Id], [Codigo], [Descricao], [CriadoEmUtc])
                    SELECT nova.[Id], nova.[Codigo], nova.[Descricao], SYSUTCDATETIME()
                    FROM @Permissoes AS nova
                    WHERE NOT EXISTS (
                        SELECT 1 FROM [Permissoes] AS existente WHERE existente.[Codigo] = nova.[Codigo]);

                    INSERT INTO [PerfisPermissoes] ([Id], [EmpresaId], [PerfilId], [PermissaoId], [CriadoEmUtc])
                    SELECT NEWID(), perfil.[EmpresaId], perfil.[Id], permissoes.[Id], SYSUTCDATETIME()
                    FROM [Perfis] AS perfil
                    CROSS JOIN @Permissoes AS permissoes
                    WHERE (
                        perfil.[Tipo] = 1 OR
                        (perfil.[Tipo] = 2 AND permissoes.[Codigo] IN (
                            N'estoque.consultar', N'estoque.movimentar.solicitar',
                            N'estoque.movimento.entrada.autorizar', N'estoque.movimento.saida.autorizar')) OR
                        (perfil.[Tipo] = 4 AND permissoes.[Codigo] IN (
                            N'estoque.consultar', N'estoque.configurar',
                            N'estoque.movimentar.solicitar', N'estoque.inventario.gerenciar')))
                      AND EXISTS (
                        SELECT 1 FROM [Permissoes] AS existente
                        WHERE existente.[Id] = permissoes.[Id] AND existente.[Codigo] = permissoes.[Codigo])
                      AND NOT EXISTS (
                        SELECT 1 FROM [PerfisPermissoes] AS existente
                        WHERE existente.[PerfilId] = perfil.[Id]
                          AND existente.[PermissaoId] = permissoes.[Id]);
                END
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensInventarioEstoque");

            migrationBuilder.DropTable(
                name: "MovimentosEstoque");

            migrationBuilder.DropTable(
                name: "SaldosEstoque");

            migrationBuilder.DropTable(
                name: "InventariosEstoque");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Filiais_EmpresaId_Id",
                table: "Filiais");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Autorizacoes_EmpresaId_Id",
                table: "Autorizacoes");

            migrationBuilder.AlterColumn<string>(
                name: "Justificativa",
                table: "Autorizacoes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.Sql("""
                DELETE FROM [PerfisPermissoes]
                WHERE [PermissaoId] IN (
                    'a0000000-0000-7000-8000-000000000001',
                    'a0000000-0000-7000-8000-000000000002',
                    'a0000000-0000-7000-8000-000000000003',
                    'a0000000-0000-7000-8000-000000000004',
                    'a0000000-0000-7000-8000-000000000005',
                    'a0000000-0000-7000-8000-000000000006');
                DELETE FROM [UsuariosPermissoes]
                WHERE [PermissaoId] IN (
                    'a0000000-0000-7000-8000-000000000001',
                    'a0000000-0000-7000-8000-000000000002',
                    'a0000000-0000-7000-8000-000000000003',
                    'a0000000-0000-7000-8000-000000000004',
                    'a0000000-0000-7000-8000-000000000005',
                    'a0000000-0000-7000-8000-000000000006');
                DELETE FROM [Permissoes]
                WHERE [Id] IN (
                    'a0000000-0000-7000-8000-000000000001',
                    'a0000000-0000-7000-8000-000000000002',
                    'a0000000-0000-7000-8000-000000000003',
                    'a0000000-0000-7000-8000-000000000004',
                    'a0000000-0000-7000-8000-000000000005',
                    'a0000000-0000-7000-8000-000000000006');
                """);
        }
    }
}
