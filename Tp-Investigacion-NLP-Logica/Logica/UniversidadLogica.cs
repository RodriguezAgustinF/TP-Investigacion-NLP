using Tp_Investigacion_NLP_Entidades;
using Tp_Investigacion_NLP_Entidades.Entidades;
using Tp_Investigacion_NLP_Logica.Interfaces;

namespace Tp_Investigacion_NLP_Logica.Logica;

public class UniversidadLogica : IUniversidadLogica
{
    private readonly NLPDbContext _db;

    public UniversidadLogica(NLPDbContext db)
    {
        _db = db;
    }
    public List<Carrera> ObtenerCarreras(string universidad)
    {
        return _db.Carreras.Where(c => c.Universidad.Nombre == universidad).ToList();
    }

    public List<Materia> ObtenerCorrelativas(string materia)
    {
        return _db.Materias
            .Where(m => m.Correlativas.Any(c => c.MateriaCorrelativa.Nombre == materia) || m.EsCorrelativaDe.Any(c => c.Materia.Nombre == materia))
            .ToList();
    }

    public List<Materia> ObtenerMaterias(string universidad, string carrera)
    {
        return _db.Materias.Where(m => m.Carrera.Nombre == carrera && m.Carrera.Universidad.Nombre == universidad ).ToList();
    }

    public List<Universidad> ObtenerUniversidades()
    {
        return _db.Universidades.ToList();
    }
}
