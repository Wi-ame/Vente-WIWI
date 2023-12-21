using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace venteenligne.Migrations
{
    /// <inheritdoc />
    public partial class Panier2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PanierPrinc_Utilisateurs_IDU",
                table: "PanierPrinc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PanierPrinc",
                table: "PanierPrinc");

            migrationBuilder.RenameTable(
                name: "PanierPrinc",
                newName: "PanierPrincs");

            migrationBuilder.RenameIndex(
                name: "IX_PanierPrinc_IDU",
                table: "PanierPrincs",
                newName: "IX_PanierPrincs_IDU");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PanierPrincs",
                table: "PanierPrincs",
                column: "PID");

            migrationBuilder.CreateTable(
                name: "Panier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPa = table.Column<int>(type: "int", nullable: false),
                    IDPro = table.Column<int>(type: "int", nullable: false),
                    Quantité = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Panier_PanierPrincs_IDPa",
                        column: x => x.IDPa,
                        principalTable: "PanierPrincs",
                        principalColumn: "PID");
                    table.ForeignKey(
                        name: "FK_Panier_Produits_IDPro",
                        column: x => x.IDPro,
                        principalTable: "Produits",
                        principalColumn: "IdPr");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Panier_IDPa",
                table: "Panier",
                column: "IDPa");

            migrationBuilder.CreateIndex(
                name: "IX_Panier_IDPro",
                table: "Panier",
                column: "IDPro");

            migrationBuilder.AddForeignKey(
                name: "FK_PanierPrincs_Utilisateurs_IDU",
                table: "PanierPrincs",
                column: "IDU",
                principalTable: "Utilisateurs",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PanierPrincs_Utilisateurs_IDU",
                table: "PanierPrincs");

            migrationBuilder.DropTable(
                name: "Panier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PanierPrincs",
                table: "PanierPrincs");

            migrationBuilder.RenameTable(
                name: "PanierPrincs",
                newName: "PanierPrinc");

            migrationBuilder.RenameIndex(
                name: "IX_PanierPrincs_IDU",
                table: "PanierPrinc",
                newName: "IX_PanierPrinc_IDU");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PanierPrinc",
                table: "PanierPrinc",
                column: "PID");

            migrationBuilder.AddForeignKey(
                name: "FK_PanierPrinc_Utilisateurs_IDU",
                table: "PanierPrinc",
                column: "IDU",
                principalTable: "Utilisateurs",
                principalColumn: "ID");
        }
    }
}
