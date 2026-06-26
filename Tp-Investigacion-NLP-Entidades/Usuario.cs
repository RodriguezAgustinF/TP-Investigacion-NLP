namespace Tp_Investigacion_NLP_Entidades;

public class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public ICollection<Conversacion> Conversaciones { get; set; } = new List<Conversacion>();
}
