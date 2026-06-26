namespace Tp_Investigacion_NLP_Entidades;

public class Materia
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Anio { get; set; }

    public int Cuatrimestre { get; set; }

    public bool EsOptativa { get; set; }

    public int CarreraId { get; set; }

    public Carrera Carrera { get; set; } = null!;

    public ICollection<Correlativa> Correlativas { get; set; } = new List<Correlativa>();

    public ICollection<Correlativa> EsCorrelativaDe { get; set; } = new List<Correlativa>();
}