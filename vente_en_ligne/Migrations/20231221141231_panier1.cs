using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace venteenligne.Migrations
{
    /// <inheritdoc />
    public partial class panier1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PanierPrinc",
                columns: table => new
                {
                    PID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDU = table.Column<int>(type: "int", nullable: false),
                    DateCréation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PanierPrinc", x => x.PID);
                    table.ForeignKey(
                        name: "FK_PanierPrinc_Utilisateurs_IDU",
                        column: x => x.IDU,
                        principalTable: "Utilisateurs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PanierPrinc_IDU",
                table: "PanierPrinc",
                column: "IDU");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PanierPrinc");
        }
    }
}
