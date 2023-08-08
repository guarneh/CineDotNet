using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CIneDotNet.Migrations
{
    /// <inheritdoc />
    public partial class dale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IntentosFallidos",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "EsAdmin",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Credito",
                table: "Usuarios",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Bloqueado",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "id", "Apellido", "Bloqueado", "Credito", "DNI", "EsAdmin", "FechaNacimiento", "IntentosFallidos", "Mail", "Nombre", "Password" },
                values: new object[,]
                {
                    { 1, "Doe", false, 10000.0, 12345678, true, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "johndoe@gmail.com", "John", "1234" },
                    { 2, "Doe", false, 10000.0, 98765432, false, new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "janedoe@gmail.com", "Jane", "1234" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "IntentosFallidos",
                table: "Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "EsAdmin",
                table: "Usuarios",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<double>(
                name: "Credito",
                table: "Usuarios",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<bool>(
                name: "Bloqueado",
                table: "Usuarios",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
