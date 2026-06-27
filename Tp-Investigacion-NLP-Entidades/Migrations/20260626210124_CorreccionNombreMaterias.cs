using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tp_Investigacion_NLP_Entidades.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionNombreMaterias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Correlativas_Materia_MateriaCorrelativaId",
                table: "Correlativas");

            migrationBuilder.DropForeignKey(
                name: "FK_Correlativas_Materia_MateriaId",
                table: "Correlativas");

            migrationBuilder.DropTable(
                name: "Materia");

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    Cuatrimestre = table.Column<int>(type: "int", nullable: false),
                    EsOptativa = table.Column<bool>(type: "bit", nullable: false),
                    CarreraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materias_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Materias",
                columns: new[] { "Id", "Anio", "CarreraId", "Cuatrimestre", "EsOptativa", "Nombre" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, false, "Introducción a la Programación" },
                    { 2, 1, 1, 2, false, "Programación I" },
                    { 3, 2, 1, 1, false, "Bases de Datos" },
                    { 4, 2, 1, 2, false, "Programación Web I" },
                    { 5, 3, 1, 1, false, "Programación Web II" },
                    { 6, 3, 1, 2, false, "Programación Web III" },
                    { 7, 1, 2, 1, false, "Álgebra" },
                    { 8, 1, 2, 1, false, "Análisis Matemático I" },
                    { 9, 1, 2, 2, false, "Programación" },
                    { 10, 2, 2, 1, false, "Arquitectura de Computadoras" },
                    { 11, 2, 2, 2, false, "Bases de Datos" },
                    { 12, 4, 2, 2, false, "Inteligencia Artificial" },
                    { 13, 1, 3, 1, false, "Administración I" },
                    { 14, 1, 3, 2, false, "Contabilidad" },
                    { 15, 2, 3, 1, false, "Economía" },
                    { 16, 2, 3, 2, false, "Marketing" },
                    { 17, 3, 3, 1, false, "Finanzas" },
                    { 18, 4, 3, 2, false, "Gestión Estratégica" },
                    { 19, 1, 4, 1, false, "Algoritmos" },
                    { 20, 1, 4, 2, false, "Matemática Discreta" },
                    { 21, 2, 4, 1, false, "Programación Avanzada" },
                    { 22, 2, 4, 2, false, "Bases de Datos" },
                    { 23, 3, 4, 1, false, "Redes" },
                    { 24, 4, 4, 2, false, "Ingeniería de Software" },
                    { 25, 1, 5, 1, false, "Anatomía" },
                    { 26, 1, 5, 2, false, "Histología" },
                    { 27, 2, 5, 1, false, "Fisiología" },
                    { 28, 3, 5, 1, false, "Patología" },
                    { 29, 4, 5, 1, false, "Farmacología" },
                    { 30, 5, 5, 2, false, "Clínica Médica" },
                    { 31, 1, 6, 1, false, "Introducción al Derecho" },
                    { 32, 1, 6, 2, false, "Derecho Civil" },
                    { 33, 2, 6, 1, false, "Derecho Penal" },
                    { 34, 2, 6, 2, false, "Derecho Constitucional" },
                    { 35, 3, 6, 2, false, "Derecho Laboral" },
                    { 36, 5, 6, 1, false, "Derecho Internacional" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materias_CarreraId",
                table: "Materias",
                column: "CarreraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Correlativas_Materias_MateriaCorrelativaId",
                table: "Correlativas",
                column: "MateriaCorrelativaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Correlativas_Materias_MateriaId",
                table: "Correlativas",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Correlativas_Materias_MateriaCorrelativaId",
                table: "Correlativas");

            migrationBuilder.DropForeignKey(
                name: "FK_Correlativas_Materias_MateriaId",
                table: "Correlativas");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarreraId = table.Column<int>(type: "int", nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    Cuatrimestre = table.Column<int>(type: "int", nullable: false),
                    EsOptativa = table.Column<bool>(type: "bit", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materia_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Materia",
                columns: new[] { "Id", "Anio", "CarreraId", "Cuatrimestre", "EsOptativa", "Nombre" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, false, "Introducción a la Programación" },
                    { 2, 1, 1, 2, false, "Programación I" },
                    { 3, 2, 1, 1, false, "Bases de Datos" },
                    { 4, 2, 1, 2, false, "Programación Web I" },
                    { 5, 3, 1, 1, false, "Programación Web II" },
                    { 6, 3, 1, 2, false, "Programación Web III" },
                    { 7, 1, 2, 1, false, "Álgebra" },
                    { 8, 1, 2, 1, false, "Análisis Matemático I" },
                    { 9, 1, 2, 2, false, "Programación" },
                    { 10, 2, 2, 1, false, "Arquitectura de Computadoras" },
                    { 11, 2, 2, 2, false, "Bases de Datos" },
                    { 12, 4, 2, 2, false, "Inteligencia Artificial" },
                    { 13, 1, 3, 1, false, "Administración I" },
                    { 14, 1, 3, 2, false, "Contabilidad" },
                    { 15, 2, 3, 1, false, "Economía" },
                    { 16, 2, 3, 2, false, "Marketing" },
                    { 17, 3, 3, 1, false, "Finanzas" },
                    { 18, 4, 3, 2, false, "Gestión Estratégica" },
                    { 19, 1, 4, 1, false, "Algoritmos" },
                    { 20, 1, 4, 2, false, "Matemática Discreta" },
                    { 21, 2, 4, 1, false, "Programación Avanzada" },
                    { 22, 2, 4, 2, false, "Bases de Datos" },
                    { 23, 3, 4, 1, false, "Redes" },
                    { 24, 4, 4, 2, false, "Ingeniería de Software" },
                    { 25, 1, 5, 1, false, "Anatomía" },
                    { 26, 1, 5, 2, false, "Histología" },
                    { 27, 2, 5, 1, false, "Fisiología" },
                    { 28, 3, 5, 1, false, "Patología" },
                    { 29, 4, 5, 1, false, "Farmacología" },
                    { 30, 5, 5, 2, false, "Clínica Médica" },
                    { 31, 1, 6, 1, false, "Introducción al Derecho" },
                    { 32, 1, 6, 2, false, "Derecho Civil" },
                    { 33, 2, 6, 1, false, "Derecho Penal" },
                    { 34, 2, 6, 2, false, "Derecho Constitucional" },
                    { 35, 3, 6, 2, false, "Derecho Laboral" },
                    { 36, 5, 6, 1, false, "Derecho Internacional" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materia_CarreraId",
                table: "Materia",
                column: "CarreraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Correlativas_Materia_MateriaCorrelativaId",
                table: "Correlativas",
                column: "MateriaCorrelativaId",
                principalTable: "Materia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Correlativas_Materia_MateriaId",
                table: "Correlativas",
                column: "MateriaId",
                principalTable: "Materia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
