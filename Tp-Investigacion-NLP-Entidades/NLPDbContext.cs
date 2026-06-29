using Microsoft.EntityFrameworkCore;
using Tp_Investigacion_NLP_Entidades.Entidades;

namespace Tp_Investigacion_NLP_Entidades;

/// <summary>Unidad de trabajo de Entity Framework Core para usuarios y conversaciones.</summary>
public class NLPDbContext : DbContext
{
    public NLPDbContext()
    {
    }

    public NLPDbContext(DbContextOptions<NLPDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Conversacion> Conversaciones { get; set; }
    public DbSet<Mensaje> Mensajes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=TP-NLP;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}
