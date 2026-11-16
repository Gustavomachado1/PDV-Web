using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDV.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CatalogoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CriadoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                    table.UniqueConstraint("AK_Departamentos_EmpresaId_Id", x => new { x.EmpresaId, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CriadoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                    table.UniqueConstraint("AK_Categorias_EmpresaId_Id", x => new { x.EmpresaId, x.Id });
                    table.ForeignKey(
                        name: "FK_Categorias_Departamentos_EmpresaId_DepartamentoId",
                        columns: x => new { x.EmpresaId, x.DepartamentoId },
                        principalTable: "Departamentos",
                        principalColumns: new[] { "EmpresaId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnidadeMedida = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CriadoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.UniqueConstraint("AK_Produtos_EmpresaId_Id", x => new { x.EmpresaId, x.Id });
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_EmpresaId_CategoriaId",
                        columns: x => new { x.EmpresaId, x.CategoriaId },
                        principalTable: "Categorias",
                        principalColumns: new[] { "EmpresaId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CodigosBarrasProdutos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CriadoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigosBarrasProdutos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodigosBarrasProdutos_Produtos_EmpresaId_ProdutoId",
                        columns: x => new { x.EmpresaId, x.ProdutoId },
                        principalTable: "Produtos",
                        principalColumns: new[] { "EmpresaId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_EmpresaId_DepartamentoId_Nome",
                table: "Categorias",
                columns: new[] { "EmpresaId", "DepartamentoId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_CodigosBarrasProdutos_EmpresaId_Codigo",
                table: "CodigosBarrasProdutos",
                columns: new[] { "EmpresaId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodigosBarrasProdutos_EmpresaId_ProdutoId",
                table: "CodigosBarrasProdutos",
                columns: new[] { "EmpresaId", "ProdutoId" });

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_EmpresaId_Nome",
                table: "Departamentos",
                columns: new[] { "EmpresaId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_EmpresaId_CategoriaId",
                table: "Produtos",
                columns: new[] { "EmpresaId", "CategoriaId" });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_EmpresaId_Descricao",
                table: "Produtos",
                columns: new[] { "EmpresaId", "Descricao" });

            migrationBuilder.Sql("""
                IF EXISTS (SELECT 1 FROM Perfis WHERE Tipo = 1)
                BEGIN
                    DECLARE @PermissaoId uniqueidentifier;
                    SELECT @PermissaoId = Id
                    FROM Permissoes
                    WHERE Codigo = N'catalogo.gerenciar';

                    IF @PermissaoId IS NULL
                    BEGIN
                        SET @PermissaoId = NEWID();
                        INSERT INTO Permissoes (Id, Codigo, Descricao, CriadoEmUtc)
                        VALUES (@PermissaoId, N'catalogo.gerenciar', N'Gerenciar catálogo de produtos', SYSUTCDATETIME());
                    END;

                    INSERT INTO PerfisPermissoes (Id, EmpresaId, PerfilId, PermissaoId, CriadoEmUtc)
                    SELECT NEWID(), perfil.EmpresaId, perfil.Id, @PermissaoId, SYSUTCDATETIME()
                    FROM Perfis AS perfil
                    WHERE perfil.Tipo = 1
                      AND NOT EXISTS (
                        SELECT 1
                        FROM PerfisPermissoes AS vinculo
                        WHERE vinculo.PerfilId = perfil.Id
                          AND vinculo.PermissaoId = @PermissaoId
                      );
                END;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodigosBarrasProdutos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Departamentos");
        }
    }
}
