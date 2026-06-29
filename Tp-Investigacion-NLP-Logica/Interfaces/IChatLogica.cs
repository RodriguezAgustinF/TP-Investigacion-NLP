using Tp_Investigacion_NLP_Logica.DTO;

namespace Tp_Investigacion_NLP_Logica.Interfaces;

public interface IChatLogica
{
    Task<ChatResponse> EnviarMensajeAsync(int conversacionId, string mensaje, LLMProvider provider);
}
