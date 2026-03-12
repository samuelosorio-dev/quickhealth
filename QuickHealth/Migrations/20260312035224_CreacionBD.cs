using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHealth.Migrations
{
    /// <inheritdoc />
    public partial class CreacionBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrecuenciaCardiaca = table.Column<int>(type: "int", nullable: false),
                    Temperatura = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    PresionSistolica = table.Column<int>(type: "int", nullable: false),
                    PresionDiastolica = table.Column<int>(type: "int", nullable: false),
                    NivelPrioridad = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
