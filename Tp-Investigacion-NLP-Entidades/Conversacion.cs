namespace Tp_Investigacion_NLP_Entidades;

public class Conversacion
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public int UsuarioId { get; set; }

    public Usuario Usuario { get; set; } = null!;

    public ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();
}