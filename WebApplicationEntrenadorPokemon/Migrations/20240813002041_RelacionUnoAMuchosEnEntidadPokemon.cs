using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationEntrenadorPokemon.Migrations
{
    /// <inheritdoc />
    public partial class RelacionUnoAMuchosEnEntidadPokemon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_Entrenador_EntrenadorId",
                table: "Pokemon");

            migrationBuilder.DropIndex(
                name: "IX_Pokemon_EntrenadorId",
                table: "Pokemon");

            migrationBuilder.AddColumn<int>(
                name: "Pokemonid",
                table: "Entrenador",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entrenador_Pokemonid",
                table: "Entrenador",
                column: "Pokemonid");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrenador_Pokemon_Pokemonid",
                table: "Entrenador",
                column: "Pokemonid",
                principalTable: "Pokemon",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrenador_Pokemon_Pokemonid",
                table: "Entrenador");

            migrationBuilder.DropIndex(
                name: "IX_Entrenador_Pokemonid",
                table: "Entrenador");

            migrationBuilder.DropColumn(
                name: "Pokemonid",
                table: "Entrenador");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_EntrenadorId",
                table: "Pokemon",
                column: "EntrenadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemon_Entrenador_EntrenadorId",
                table: "Pokemon",
                column: "EntrenadorId",
                principalTable: "Entrenador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
