namespace Tp_Investigacion_NLP_Entidades.Entidades;

public class Carrera
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int DuracionAnios { get; set; }

    public int UniversidadId { get; set; }

    public Universidad Universidad { get; set; } = null!;

    public ICollection<Materia> Materias { get; set; } = new List<Materia>();
}