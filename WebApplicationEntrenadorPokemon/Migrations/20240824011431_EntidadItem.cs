using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationEntrenadorPokemon.Migrations
{
    /// <inheritdoc />
    public partial class EntidadItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Entrenador",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntrenadorId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sprite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entrenador_ItemId",
                table: "Entrenador",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrenador_Item_ItemId",
                table: "Entrenador",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrenador_Item_ItemId",
                table: "Entrenador");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Entrenador_ItemId",
                table: "Entrenador");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Entrenador");
        }
    }
}
