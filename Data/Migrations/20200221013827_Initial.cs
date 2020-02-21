using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Escritos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    texto = table.Column<string>(type: "text", nullable: false),
                    idEstadoEscrito = table.Column<int>(nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    idUsuario = table.Column<int>(nullable: false),
                    idJuicio = table.Column<int>(nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2(2)", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escritos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EscritosTexto",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    texto = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscritosTexto", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Escritos");

            migrationBuilder.DropTable(
                name: "EscritosTexto");
        }
    }
}
