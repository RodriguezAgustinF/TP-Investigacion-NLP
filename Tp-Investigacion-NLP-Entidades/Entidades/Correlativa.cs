namespace Tp_Investigacion_NLP_Entidades.Entidades;

public class Correlativa
{
    public int Id { get; set; }

    public int MateriaId { get; set; }

    public Materia Materia { get; set; } = null!;

    public int MateriaCorrelativaId { get; set; }

    public Materia MateriaCorrelativa { get; set; } = null!;
}