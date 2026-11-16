using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDV.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IntegridadeFilialEstoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventariosEstoque_Filiais_EmpresaId_FilialId",
                table: "InventariosEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensInventarioEstoque_InventariosEstoque_EmpresaId_InventarioEstoqueId",
                table: "ItensInventarioEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentosEstoque_Filiais_EmpresaId_FilialId",
                table: "MovimentosEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentosEstoque_InventariosEstoque_EmpresaId_InventarioEstoqueId",
                table: "MovimentosEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_SaldosEstoque_Filiais_EmpresaId_FilialId",
                table: "SaldosEstoque");

            migrationBuilder.DropIndex(
                name: "IX_MovimentosEstoque_EmpresaId_InventarioEstoqueId",
                table: "MovimentosEstoque");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Filiais_EmpresaId_Id",
                table: "Filiais");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Usuarios_EmpresaId_Id",
                table: "Usuarios",
                columns: new[] { "EmpresaId", "Id" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_InventariosEstoque_EmpresaId_FilialId_Id",
                table: "InventariosEstoque",
                columns: new[] { "EmpresaId", "FilialId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_SaldosEstoque_FilialId",
                table: "SaldosEstoque",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosEstoque_EmpresaId_AutorizadorId",
                table: "MovimentosEstoque",
                columns: new[] { "EmpresaId", "AutorizadorId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosEstoque_EmpresaId_FilialId_InventarioEstoqueId",
                table: "MovimentosEstoque",
                columns: new[] { "EmpresaId", "FilialId", "InventarioEstoqueId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosEstoque_EmpresaId_SolicitanteId",
                table: "MovimentosEstoque",
                columns: new[] { "EmpresaId", "SolicitanteId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosEstoque_FilialId",
                table: "MovimentosEstoque",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensInventarioEstoque_EmpresaId_FilialId_InventarioEstoqueId",
                table: "ItensInventarioEstoque",
                columns: new[] { "EmpresaId", "FilialId", "InventarioEstoqueId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventariosEstoque_EmpresaId_UsuarioId",
                table: "InventariosEstoque",
                columns: new[] { "EmpresaId", "UsuarioId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventariosEstoque_FilialId",
                table: "InventariosEstoque",
                column: "FilialId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventariosEstoque_Filiais_FilialId",
                table: "InventariosEstoque",
                column: "FilialId",
                principalTable: "Filiais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventariosEstoque_Usuarios_EmpresaId_UsuarioId",
                table: "InventariosEstoque",
                columns: new[] { "EmpresaId", "UsuarioId" },
                principalTable: "Usuarios",
                principalColumns: new[] { "EmpresaId", "Id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensInventarioEstoque_InventariosEstoque_EmpresaId_FilialId_InventarioEstoqueId",
                table: "ItensInventarioEstoque",
                columns: new[] { "EmpresaId", "FilialId", "InventarioEstoqueId" },
                principalTable: "InventariosEstoque",
                principalColumns: new[] { "EmpresaId", "FilialId", "Id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentosEstoque_Filiais_FilialId",
                table: "MovimentosEstoque",
                column: "FilialId",
                principalTable: "Filiais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentosEstoque_InventariosEstoque_EmpresaId_FilialId_InventarioEstoqueId",
                table: "MovimentosEstoque",
                columns: new[] { "EmpresaId", "FilialId", "InventarioEstoqueId" },
                principalTable: "InventariosEstoque",
                principalColumns: new[] { "EmpresaId", "FilialId", "Id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentosEstoque_Usuarios_EmpresaId_AutorizadorId",
                table: "MovimentosEstoque",
                columns: new[] { "EmpresaId", "AutorizadorId" },
                principalTable: "Usuarios",
                principalColumns: new[] { "EmpresaId", "Id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentosEstoque_Usuarios_EmpresaId_SolicitanteId",
                table: "MovimentosEstoque",
                columns: new[] { "EmpresaId", "SolicitanteId" },
                principalTable: "Usuarios",
                principalColumns: new[] { "EmpresaId", "Id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaldosEstoque_Filiais_FilialId",
                table: "SaldosEstoque",
                column: "FilialId",
                principalTable: "Filiais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventariosEstoque_Filiais_FilialId",
                table: "InventariosEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_InventariosEstoque_Usuarios_EmpresaId_UsuarioId",
                table: "InventariosEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensInventarioEstoque_InventariosEstoque_EmpresaId_FilialId_InventarioEstoqueId",
                table: "ItensInventarioEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentosEstoque_Filiais_FilialId",
                table: "MovimentosEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentosEstoque_InventariosEstoque_EmpresaId_FilialId_InventarioEstoqueId",
                table: "MovimentosEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentosEstoque_Usuarios_EmpresaId_AutorizadorId",
                table: "MovimentosEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentosEstoque_Usuarios_EmpresaId_SolicitanteId",
                table: "MovimentosEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_SaldosEstoque_Filiais_FilialId",
                table: "SaldosEstoque");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Usuarios_EmpresaId_Id",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_SaldosEstoque_FilialId",
                table: "SaldosEstoque");

            migrationBuilder.DropIndex(
                name: "IX_MovimentosEstoque_EmpresaId_AutorizadorId",
                table: "MovimentosEstoque");

            migrationBuilder.DropIndex(
                name: "IX_MovimentosEstoque_EmpresaId_FilialId_InventarioEstoqueId",
                table: "MovimentosEstoque");

            migrationBuilder.DropIndex(
                name: "IX_MovimentosEstoque_EmpresaId_SolicitanteId",
                table: "MovimentosEstoque");

            migrationBuilder.DropIndex(
                name: "IX_MovimentosEstoque_FilialId",
                table: "MovimentosEstoque");

            migrationBuilder.DropIndex(
                name: "IX_ItensInventarioEstoque_EmpresaId_FilialId_InventarioEstoqueId",
                table: "ItensInventarioEstoque");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_InventariosEstoque_EmpresaId_FilialId_Id",
                table: "InventariosEstoque");

            migrationBuilder.DropIndex(
                name: "IX_InventariosEstoque_EmpresaId_UsuarioId",
                table: "InventariosEstoque");

            migrationBuilder.DropIndex(
                name: "IX_InventariosEstoque_FilialId",
                table: "InventariosEstoque");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Filiais_EmpresaId_Id",
                table: "Filiais",
                columns: new[] { "EmpresaId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosEstoque_EmpresaId_InventarioEstoqueId",
                table: "MovimentosEstoque",
                columns: new[] { "EmpresaId", "InventarioEstoqueId" });

            migrationBuilder.AddForeignKey(
                name: "FK_InventariosEstoque_Filiais_EmpresaId_FilialId",
                table: "InventariosEstoque",
                columns: new[] { "EmpresaId", "FilialId" },
                principalTable: "Filiais",
                principalColumns: new[] { "EmpresaId", "Id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensInventarioEstoque_InventariosEstoque_EmpresaId_InventarioEstoqueId",
                table: "ItensInventarioEstoque",
                columns: new[] { "EmpresaId", "InventarioEstoqueId" },
                principalTable: "InventariosEstoque",
                principalColumns: new[] { "EmpresaId", "Id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentosEstoque_Filiais_EmpresaId_FilialId",
                table: "MovimentosEstoque",
                columns: new[] { "EmpresaId", "FilialId" },
                principalTable: "Filiais",
                principalColumns: new[] { "EmpresaId", "Id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentosEstoque_InventariosEstoque_EmpresaId_InventarioEstoqueId",
                table: "MovimentosEstoque",
                columns: new[] { "EmpresaId", "InventarioEstoqueId" },
                principalTable: "InventariosEstoque",
                principalColumns: new[] { "EmpresaId", "Id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaldosEstoque_Filiais_EmpresaId_FilialId",
                table: "SaldosEstoque",
                columns: new[] { "EmpresaId", "FilialId" },
                principalTable: "Filiais",
                principalColumns: new[] { "EmpresaId", "Id" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
