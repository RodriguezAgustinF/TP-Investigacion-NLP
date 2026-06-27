using Tp_Investigacion_NLP_Entidades.Entidades;

namespace Tp_Investigacion_NLP_Logica.Interfaces;

public interface ILLMService
{
    Task<string> ObtenerRespuestaAsync(Conversacion conversacion);
}
