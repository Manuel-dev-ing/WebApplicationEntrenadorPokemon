using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationEntrenadorPokemon.Migrations
{
    /// <inheritdoc />
    public partial class EntidadEntrenadoryPokemonRelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entrenador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntrenadorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneroId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrenador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entrenador_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntrenadorId = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    altura = table.Column<int>(type: "int", nullable: false),
                    peso = table.Column<int>(type: "int", nullable: false),
                    officialArtwork = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pokemon_Entrenador_EntrenadorId",
                        column: x => x.EntrenadorId,
                        principalTable: "Entrenador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entrenador_GeneroId",
                table: "Entrenador",
                column: "GeneroId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_EntrenadorId",
                table: "Pokemon",
                column: "EntrenadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pokemon");

            migrationBuilder.DropTable(
                name: "Entrenador");
        }
    }
}
