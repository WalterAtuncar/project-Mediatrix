using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _SB_._MediatrixApi_._Infraestructura_.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriasEntidad",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    EstaEliminado = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasEntidad", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "EntidadesGubernamentales",
                columns: table => new
                {
                    EntidadId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Siglas = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    NombreEncargado = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    EstaEliminado = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntidadesGubernamentales", x => x.EntidadId);
                    table.ForeignKey(
                        name: "FK_EntidadesGubernamentales_CategoriasEntidad_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "CategoriasEntidad",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntidadesGubernamentales_CategoriaId",
                table: "EntidadesGubernamentales",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntidadesGubernamentales");

            migrationBuilder.DropTable(
                name: "CategoriasEntidad");
        }
    }
}
