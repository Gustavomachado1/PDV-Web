using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDV.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HierarquiaCatalogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_EmpresaId_CategoriaId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Departamentos_EmpresaId_Nome",
                table: "Departamentos");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_EmpresaId_DepartamentoId_Nome",
                table: "Categorias");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Produtos",
                newName: "SubcategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_EmpresaId_CategoriaId",
                table: "Produtos",
                newName: "IX_Produtos_EmpresaId_SubcategoriaId");

            migrationBuilder.AddColumn<bool>(
                name: "UsoInterno",
                table: "Produtos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Segmento",
                table: "Departamentos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Subcategorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CriadoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategorias", x => x.Id);
                    table.UniqueConstraint("AK_Subcategorias_EmpresaId_Id", x => new { x.EmpresaId, x.Id });
                    table.ForeignKey(
                        name: "FK_Subcategorias_Categorias_EmpresaId_CategoriaId",
                        columns: x => new { x.EmpresaId, x.CategoriaId },
                        principalTable: "Categorias",
                        principalColumns: new[] { "EmpresaId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_EmpresaId_Nome",
                table: "Departamentos",
                columns: new[] { "EmpresaId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_EmpresaId_DepartamentoId_Nome",
                table: "Categorias",
                columns: new[] { "EmpresaId", "DepartamentoId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subcategorias_EmpresaId_CategoriaId_Nome",
                table: "Subcategorias",
                columns: new[] { "EmpresaId", "CategoriaId", "Nome" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Subcategorias_EmpresaId_SubcategoriaId",
                table: "Produtos",
                columns: new[] { "EmpresaId", "SubcategoriaId" },
                principalTable: "Subcategorias",
                principalColumns: new[] { "EmpresaId", "Id" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Subcategorias_EmpresaId_SubcategoriaId",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Subcategorias");

            migrationBuilder.DropIndex(
                name: "IX_Departamentos_EmpresaId_Nome",
                table: "Departamentos");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_EmpresaId_DepartamentoId_Nome",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "UsoInterno",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Segmento",
                table: "Departamentos");

            migrationBuilder.RenameColumn(
                name: "SubcategoriaId",
                table: "Produtos",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_EmpresaId_SubcategoriaId",
                table: "Produtos",
                newName: "IX_Produtos_EmpresaId_CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_EmpresaId_Nome",
                table: "Departamentos",
                columns: new[] { "EmpresaId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_EmpresaId_DepartamentoId_Nome",
                table: "Categorias",
                columns: new[] { "EmpresaId", "DepartamentoId", "Nome" });

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_EmpresaId_CategoriaId",
                table: "Produtos",
                columns: new[] { "EmpresaId", "CategoriaId" },
                principalTable: "Categorias",
                principalColumns: new[] { "EmpresaId", "Id" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
