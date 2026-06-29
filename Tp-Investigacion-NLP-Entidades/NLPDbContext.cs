using Microsoft.EntityFrameworkCore;
using Tp_Investigacion_NLP_Entidades.Entidades;

namespace Tp_Investigacion_NLP_Entidades;

public class NLPDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Conversacion> Conversaciones { get; set; }
    public DbSet<Mensaje> Mensajes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.;Database=TP-NLP;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}
