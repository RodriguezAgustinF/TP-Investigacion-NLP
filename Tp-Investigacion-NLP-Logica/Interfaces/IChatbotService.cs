using Tp_Investigacion_NLP_Entidades.Entidades;

namespace Tp_Investigacion_NLP_Logica.Interfaces;

/// <summary>Encapsula el contexto, las herramientas y la invocación del modelo.</summary>
public interface IChatbotService
{
    /// <summary>Genera una respuesta para una conversación con historial cargado.</summary>
    Task<string> ObtenerRespuestaAsync(
        Conversacion conversacion,
        LLMProvider provider,
        CancellationToken cancellationToken = default);
}
