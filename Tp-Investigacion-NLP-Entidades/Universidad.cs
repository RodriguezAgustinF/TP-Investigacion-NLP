namespace Tp_Investigacion_NLP_Entidades;

public class Universidad
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Acronimo { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public ICollection<Carrera> Carreras { get; set; } = new List<Carrera>();
}
