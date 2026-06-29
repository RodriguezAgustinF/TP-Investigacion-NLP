using Tp_Investigacion_NLP_Logica.DTO;

namespace Tp_Investigacion_NLP_Logica.Interfaces;

/// <summary>Coordina el caso de uso completo de envío de mensajes.</summary>
public interface IChatLogica
{
    /// <summary>Autoriza, persiste el mensaje, genera la respuesta y la guarda.</summary>
    Task<ChatResponse> EnviarMensajeAsync(
        int conversacionId,
        int usuarioId,
        string mensaje,
        LLMProvider provider,
        CancellationToken cancellationToken = default);
}
