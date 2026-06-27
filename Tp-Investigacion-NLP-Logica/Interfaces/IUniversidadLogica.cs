using System;
using System.Collections.Generic;
using System.Text;
using Tp_Investigacion_NLP_Entidades.Entidades;

namespace Tp_Investigacion_NLP_Logica.Interfaces;

public interface IUniversidadLogica
{
    List<Carrera> ObtenerCarreras(string universidad);
    List<Materia> ObtenerMaterias(string universidad, string carrera);
    List<Materia> ObtenerCorrelativas(string materia);
    List<Universidad> ObtenerUniversidades();
}