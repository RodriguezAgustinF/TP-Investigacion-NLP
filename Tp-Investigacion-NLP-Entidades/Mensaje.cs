namespace Tp_Investigacion_NLP_Entidades;

public class Mensaje
{
    public int Id { get; set; }

    public string Rol { get; set; } = null!; // user | assistant

    public string Contenido { get; set; } = null!;

    public DateTime Fecha { get; set; } = DateTime.UtcNow;

    public int ConversacionId { get; set; }

    public Conversacion Conversacion { get; set; } = null!;
}