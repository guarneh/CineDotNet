using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CIneDotNet.Migrations
{
    /// <inheritdoc />
    public partial class peliculas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Peliculas",
                columns: new[] { "id", "Duracion", "Nombre", "Poster", "Sinopsis" },
                values: new object[,]
                {
                    { 1, 120, "Barbie", "ken.jpg", "Después de ser expulsada de Barbieland por no ser una muñeca de aspecto perfecto, Barbie parte hacia el mundo humano para encontrar la verdadera felicidad.\r\n" },
                    { 2, 180, "Oppenheimer", "oppenhaimer.jpeg", "El físico J Robert Oppenheimer trabaja con un equipo de científicos durante el Proyecto Manhattan, que condujo al desarrollo de la bomba atómica.\r\n" },
                    { 3, 105, "Sound of Freedom", "freedom.jpg", "Sonido De Libertad, basada en una increíble historia real, trae luz y esperanza al obscuro mundo del trafico de menores. Después de rescatar a un niño de los traficantes, un agente federal descubre que la hermana del niño todavía está cautiva y decide embarcarse en una peligrosa misión para salvarla. Con el tiempo en su contra, renuncia a su trabajo y se adentra en lo profundo de la selva colombiana, poniendo su vida en riesgo para liberarla y traerla de vuelta a casa.\r\n" }
                });

            migrationBuilder.InsertData(
                table: "Salas",
                columns: new[] { "id", "capacidad", "ubicacion" },
                values: new object[,]
                {
                    { 1, 50, "Sala A" },
                    { 2, 30, "Sala B" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Salas",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Salas",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
