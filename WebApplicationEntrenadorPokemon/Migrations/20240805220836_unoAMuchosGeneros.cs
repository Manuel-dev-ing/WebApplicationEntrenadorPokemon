using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationEntrenadorPokemon.Migrations
{
    /// <inheritdoc />
    public partial class unoAMuchosGeneros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrenador_Generos_GeneroId",
                table: "Entrenador");

            migrationBuilder.DropIndex(
                name: "IX_Entrenador_GeneroId",
                table: "Entrenador");

            migrationBuilder.AddColumn<int>(
                name: "EntrenadorId",
                table: "Generos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Generos_EntrenadorId",
                table: "Generos",
                column: "EntrenadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Generos_Entrenador_EntrenadorId",
                table: "Generos",
                column: "EntrenadorId",
                principalTable: "Entrenador",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Generos_Entrenador_EntrenadorId",
                table: "Generos");

            migrationBuilder.DropIndex(
                name: "IX_Generos_EntrenadorId",
                table: "Generos");

            migrationBuilder.DropColumn(
                name: "EntrenadorId",
                table: "Generos");

            migrationBuilder.CreateIndex(
                name: "IX_Entrenador_GeneroId",
                table: "Entrenador",
                column: "GeneroId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Entrenador_Generos_GeneroId",
                table: "Entrenador",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
