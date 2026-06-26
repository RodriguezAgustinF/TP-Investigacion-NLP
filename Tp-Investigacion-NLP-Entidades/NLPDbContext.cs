using Microsoft.EntityFrameworkCore;

namespace Tp_Investigacion_NLP_Entidades;

public class NLPDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Carrera> Carreras { get; set; }
    public DbSet<Conversacion> Conversaciones { get; set; }
    public DbSet<Correlativa> Correlativas { get; set; }
    public DbSet<Materia> Materia { get; set; }
    public DbSet<Mensaje> Mensajes { get; set; }
    public DbSet<Universidad> Universidades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.;Database=TP-NLP;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Correlativa>()
            .HasOne(c => c.Materia)
            .WithMany(m => m.Correlativas)
            .HasForeignKey(c => c.MateriaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Correlativa>()
            .HasOne(c => c.MateriaCorrelativa)
            .WithMany(m => m.EsCorrelativaDe)
            .HasForeignKey(c => c.MateriaCorrelativaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Universidad>().HasData(
            new Universidad
            {
                Id = 1,
                Nombre = "Universidad Nacional de La Matanza",
                Acronimo = "UNLAM",
                Ciudad = "San Justo",
                Pais = "Argentina"
            },
            new Universidad
            {
                Id = 2,
                Nombre = "Universidad de Buenos Aires",
                Acronimo = "UBA",
                Ciudad = "Buenos Aires",
                Pais = "Argentina"
            }
        );


        modelBuilder.Entity<Carrera>().HasData(
            new Carrera
            {
                Id = 1,
                Nombre = "Tecnicatura en Desarrollo Web",
                DuracionAnios = 3,
                UniversidadId = 1
            },
            new Carrera
            {
                Id = 2,
                Nombre = "Ingeniería en Informática",
                DuracionAnios = 5,
                UniversidadId = 1
            },
            new Carrera
            {
                Id = 3,
                Nombre = "Licenciatura en Administración",
                DuracionAnios = 4,
                UniversidadId = 1
            },
            new Carrera
            {
                Id = 4,
                Nombre = "Ingeniería en Sistemas",
                DuracionAnios = 5,
                UniversidadId = 2
            },
            new Carrera
            {
                Id = 5,
                Nombre = "Medicina",
                DuracionAnios = 6,
                UniversidadId = 2
            },
            new Carrera
            {
                Id = 6,
                Nombre = "Derecho",
                DuracionAnios = 5,
                UniversidadId = 2
            }
        );

        modelBuilder.Entity<Materia>().HasData(
            new Materia
            {
                Id = 1,
                Nombre = "Introducción a la Programación",
                Anio = 1,
                Cuatrimestre = 1,
                CarreraId = 1
            },
            new Materia
            {
                Id = 2,
                Nombre = "Programación I",
                Anio = 1,
                Cuatrimestre = 2,
                CarreraId = 1
            },
            new Materia
            {
                Id = 3,
                Nombre = "Bases de Datos",
                Anio = 2,
                Cuatrimestre = 1,
                CarreraId = 1
            },
            new Materia
            {
                Id = 4,
                Nombre = "Programación Web I",
                Anio = 2,
                Cuatrimestre = 2,
                CarreraId = 1
            },
            new Materia
            {
                Id = 5,
                Nombre = "Programación Web II",
                Anio = 3,
                Cuatrimestre = 1,
                CarreraId = 1
            },
            new Materia
            {
                Id = 6,
                Nombre = "Programación Web III",
                Anio = 3,
                Cuatrimestre = 2,
                CarreraId = 1
            },
            new Materia
            {
                Id = 7,
                Nombre = "Álgebra",
                Anio = 1,
                Cuatrimestre = 1,
                CarreraId = 2
            },
            new Materia
            {
                Id = 8,
                Nombre = "Análisis Matemático I",
                Anio = 1,
                Cuatrimestre = 1,
                CarreraId = 2
            },
            new Materia
            {
                Id = 9,
                Nombre = "Programación",
                Anio = 1,
                Cuatrimestre = 2,
                CarreraId = 2
            },
            new Materia
            {
                Id = 10,
                Nombre = "Arquitectura de Computadoras",
                Anio = 2,
                Cuatrimestre = 1,
                CarreraId = 2
            },
            new Materia
            {
                Id = 11,
                Nombre = "Bases de Datos",
                Anio = 2,
                Cuatrimestre = 2,
                CarreraId = 2
            },
            new Materia
            {
                Id = 12,
                Nombre = "Inteligencia Artificial",
                Anio = 4,
                Cuatrimestre = 2,
                CarreraId = 2
            },
            new Materia
            {
                Id = 13,
                Nombre = "Administración I",
                Anio = 1,
                Cuatrimestre = 1,
                CarreraId = 3
            },
            new Materia
            {
                Id = 14,
                Nombre = "Contabilidad",
                Anio = 1,
                Cuatrimestre = 2,
                CarreraId = 3
            },
            new Materia
            {   Id = 15,
                Nombre = "Economía",
                Anio = 2,
                Cuatrimestre = 1,
                CarreraId = 3
            },
            new Materia
            {   Id = 16,
                Nombre = "Marketing",
                Anio = 2,
                Cuatrimestre = 2,
                CarreraId = 3
            },
            new Materia
            {   Id = 17,
                Nombre = "Finanzas",
                Anio = 3,
                Cuatrimestre = 1,
                CarreraId = 3
            },
            new Materia
            {   Id = 18,
                Nombre = "Gestión Estratégica",
                Anio = 4,
                Cuatrimestre = 2,
                CarreraId = 3
            },
            new Materia
            {   Id = 19,
                Nombre = "Algoritmos",
                Anio = 1,
                Cuatrimestre = 1,
                CarreraId = 4
            },
            new Materia
            {   Id = 20,
                Nombre = "Matemática Discreta",
                Anio = 1,
                Cuatrimestre = 2,
                CarreraId = 4
            },
            new Materia
            {   Id = 21,
                Nombre = "Programación Avanzada",
                Anio = 2,
                Cuatrimestre = 1,
                CarreraId = 4
            },
            new Materia
            {   Id = 22,
                Nombre = "Bases de Datos",
                Anio = 2,
                Cuatrimestre = 2,
                CarreraId = 4
            },
            new Materia 
            {   Id = 23, 
                Nombre = "Redes", 
                Anio = 3, 
                Cuatrimestre = 1, 
                CarreraId = 4 
            },
            new Materia 
            {   Id = 24, 
                Nombre = "Ingeniería de Software", 
                Anio = 4, 
                Cuatrimestre = 2, 
                CarreraId = 4 
            },
            new Materia 
            {   Id = 25, 
                Nombre = "Anatomía", 
                Anio = 1, 
                Cuatrimestre = 1, 
                CarreraId = 5 
            },
            new Materia 
            {   Id = 26, 
                Nombre = "Histología", 
                Anio = 1, 
                Cuatrimestre = 2, 
                CarreraId = 5 
            },
            new Materia 
            {   Id = 27, 
                Nombre = "Fisiología", 
                Anio = 2, 
                Cuatrimestre = 1, 
                CarreraId = 5 
            },
            new Materia 
            {   Id = 28, 
                Nombre = "Patología", 
                Anio = 3, 
                Cuatrimestre = 1, 
                CarreraId = 5 
            },
            new Materia 
            {   Id = 29, 
                Nombre = "Farmacología", 
                Anio = 4, 
                Cuatrimestre = 1, 
                CarreraId = 5 
            },
            new Materia 
            {   Id = 30, 
                Nombre = "Clínica Médica", 
                Anio = 5, 
                Cuatrimestre = 2, 
                CarreraId = 5 
            },
            new Materia 
            {   Id = 31, 
                Nombre = "Introducción al Derecho", 
                Anio = 1, 
                Cuatrimestre = 1, 
                CarreraId = 6 
            },
            new Materia 
            {   Id = 32, 
                Nombre = "Derecho Civil", 
                Anio = 1, 
                Cuatrimestre = 2, 
                CarreraId = 6 
            },
            new Materia 
            {   Id = 33, 
                Nombre = "Derecho Penal", 
                Anio = 2, 
                Cuatrimestre = 1, 
                CarreraId = 6 
            },
            new Materia 
            {   Id = 34, 
                Nombre = "Derecho Constitucional", 
                Anio = 2, 
                Cuatrimestre = 2, 
                CarreraId = 6 
            },
            new Materia 
            {   Id = 35, 
                Nombre = "Derecho Laboral", 
                Anio = 3, 
                Cuatrimestre = 2, 
                CarreraId = 6 
            },
            new Materia 
            {   Id = 36, 
                Nombre = "Derecho Internacional", 
                Anio = 5, 
                Cuatrimestre = 1, 
                CarreraId = 6 
            }
        );

        modelBuilder.Entity<Correlativa>().HasData(
            // Desarrollo Web

            new Correlativa 
            { 
                Id = 1, 
                MateriaId = 2, 
                MateriaCorrelativaId = 1 
            },
            new Correlativa 
            { 
                Id = 2, 
                MateriaId = 3, 
                MateriaCorrelativaId = 2 
            },
            new Correlativa 
            { 
                Id = 3, 
                MateriaId = 4, 
                MateriaCorrelativaId = 2 
            },
            new Correlativa 
            { 
                Id = 4, 
                MateriaId = 5, 
                MateriaCorrelativaId = 4 
            },
            new Correlativa 
            { 
                Id = 5, 
                MateriaId = 6, 
                MateriaCorrelativaId = 5 
            },

            // Ingeniería Informática

            new Correlativa 
            { 
                Id = 6, 
                MateriaId = 9, 
                MateriaCorrelativaId = 7 
            },
            new Correlativa 
            { 
                Id = 7, 
                MateriaId = 10, 
                MateriaCorrelativaId = 9 
            },
            new Correlativa 
            { 
                Id = 8, 
                MateriaId = 11, 
                MateriaCorrelativaId = 9 
            },
            new Correlativa 
            { 
                Id = 9, 
                MateriaId = 12, 
                MateriaCorrelativaId = 11 
            },

            // Administración

            new Correlativa 
            { 
                Id = 10, 
                MateriaId = 15, 
                MateriaCorrelativaId = 13 
            },
            new Correlativa 
            { 
                Id = 11, 
                MateriaId = 16, 
                MateriaCorrelativaId = 15 
            },
            new Correlativa 
            { 
                Id = 12, 
                MateriaId = 17, 
                MateriaCorrelativaId = 16 
            },
            new Correlativa 
            { 
                Id = 13, 
                MateriaId = 18, 
                MateriaCorrelativaId = 17 
            },

            // Sistemas

            new Correlativa 
            { 
                Id = 14, 
                MateriaId = 21, 
                MateriaCorrelativaId = 19 
            },
            new Correlativa 
            { 
                Id = 15, 
                MateriaId = 22, 
                MateriaCorrelativaId = 21 
            },
            new Correlativa 
            { 
                Id = 16, 
                MateriaId = 23, 
                MateriaCorrelativaId = 22 
            },
            new Correlativa 
            { 
                Id = 17, 
                MateriaId = 24, 
                MateriaCorrelativaId = 23 
            },

            // Medicina

            new Correlativa 
            { 
                Id = 18, 
                MateriaId = 27, 
                MateriaCorrelativaId = 25 
            },
            new Correlativa 
            { 
                Id = 19, 
                MateriaId = 28, 
                MateriaCorrelativaId = 27 
            },
            new Correlativa 
            { 
                Id = 20, 
                MateriaId = 29, 
                MateriaCorrelativaId = 28 
            },
            new Correlativa 
            { 
                Id = 21, 
                MateriaId = 30, 
                MateriaCorrelativaId = 29 
            },

            // Derecho

            new Correlativa 
            { 
                Id = 22, 
                MateriaId = 33, 
                MateriaCorrelativaId = 31 
            },
            new Correlativa 
            { 
                Id = 23, 
                MateriaId = 34, 
                MateriaCorrelativaId = 32 
            },
            new Correlativa 
            { 
                Id = 24, 
                MateriaId = 35, 
                MateriaCorrelativaId = 33 
            },
            new Correlativa 
            { 
                Id = 25, 
                MateriaId = 36, 
                MateriaCorrelativaId = 35 
            }
        );
    }
}
