using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tp_Investigacion_NLP_Entidades.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablasEInicializacionDeDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Universidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Acronimo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DuracionAnios = table.Column<int>(type: "int", nullable: false),
                    UniversidadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carreras_Universidades_UniversidadId",
                        column: x => x.UniversidadId,
                        principalTable: "Universidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conversaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materia",
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
                    table.PrimaryKey("PK_Materia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materia_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mensajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConversacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensajes_Conversaciones_ConversacionId",
                        column: x => x.ConversacionId,
                        principalTable: "Conversaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Correlativas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    MateriaCorrelativaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correlativas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Correlativas_Materia_MateriaCorrelativaId",
                        column: x => x.MateriaCorrelativaId,
                        principalTable: "Materia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Correlativas_Materia_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Universidades",
                columns: new[] { "Id", "Acronimo", "Ciudad", "Nombre", "Pais" },
                values: new object[,]
                {
                    { 1, "UNLAM", "San Justo", "Universidad Nacional de La Matanza", "Argentina" },
                    { 2, "UBA", "Buenos Aires", "Universidad de Buenos Aires", "Argentina" }
                });

            migrationBuilder.InsertData(
                table: "Carreras",
                columns: new[] { "Id", "DuracionAnios", "Nombre", "UniversidadId" },
                values: new object[,]
                {
                    { 1, 3, "Tecnicatura en Desarrollo Web", 1 },
                    { 2, 5, "Ingeniería en Informática", 1 },
                    { 3, 4, "Licenciatura en Administración", 1 },
                    { 4, 5, "Ingeniería en Sistemas", 2 },
                    { 5, 6, "Medicina", 2 },
                    { 6, 5, "Derecho", 2 }
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

            migrationBuilder.InsertData(
                table: "Correlativas",
                columns: new[] { "Id", "MateriaCorrelativaId", "MateriaId" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 2, 3 },
                    { 3, 2, 4 },
                    { 4, 4, 5 },
                    { 5, 5, 6 },
                    { 6, 7, 9 },
                    { 7, 9, 10 },
                    { 8, 9, 11 },
                    { 9, 11, 12 },
                    { 10, 13, 15 },
                    { 11, 15, 16 },
                    { 12, 16, 17 },
                    { 13, 17, 18 },
                    { 14, 19, 21 },
                    { 15, 21, 22 },
                    { 16, 22, 23 },
                    { 17, 23, 24 },
                    { 18, 25, 27 },
                    { 19, 27, 28 },
                    { 20, 28, 29 },
                    { 21, 29, 30 },
                    { 22, 31, 33 },
                    { 23, 32, 34 },
                    { 24, 33, 35 },
                    { 25, 35, 36 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_UniversidadId",
                table: "Carreras",
                column: "UniversidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversaciones_UsuarioId",
                table: "Conversaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Correlativas_MateriaCorrelativaId",
                table: "Correlativas",
                column: "MateriaCorrelativaId");

            migrationBuilder.CreateIndex(
                name: "IX_Correlativas_MateriaId",
                table: "Correlativas",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Materia_CarreraId",
                table: "Materia",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_ConversacionId",
                table: "Mensajes",
                column: "ConversacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Correlativas");

            migrationBuilder.DropTable(
                name: "Mensajes");

            migrationBuilder.DropTable(
                name: "Materia");

            migrationBuilder.DropTable(
                name: "Conversaciones");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Universidades");
        }
    }
}
