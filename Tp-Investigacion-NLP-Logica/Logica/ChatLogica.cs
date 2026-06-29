using Tp_Investigacion_NLP_Logica.DTO;
using Tp_Investigacion_NLP_Logica.Interfaces;

namespace Tp_Investigacion_NLP_Logica.Logica;

public class ChatLogica : IChatLogica
{
    private readonly IMensajeLogica _mensajeLogica;
    private readonly IConversacionLogica _conversacionLogica;
    private readonly ILLMServiceFactory _llmServiceFactory;

    public ChatLogica(
        IMensajeLogica mensajeLogica,
        IConversacionLogica conversacionLogica,
        ILLMServiceFactory llmServiceFactory)
    {
        _mensajeLogica = mensajeLogica;
        _conversacionLogica = conversacionLogica;
        _llmServiceFactory = llmServiceFactory;
    }

    public async Task<ChatResponse> EnviarMensajeAsync(
    int conversacionId,
    string mensaje,
    LLMProvider provider)
    {
        _mensajeLogica.AgregarMensajeUsuario(conversacionId, mensaje);

        string tituloActualizado = _conversacionLogica.ActualizarTituloSiEsNueva(
            conversacionId,
            mensaje);

        var conversacion = _conversacionLogica.ObtenerConversacionCompleta(conversacionId);

        if (conversacion == null)
        {
            throw new InvalidOperationException("No se encontró la conversación.");
        }

        var llmService = _llmServiceFactory.GetService(provider);

        string respuesta = await llmService.ObtenerRespuestaAsync(conversacion);

        _mensajeLogica.AgregarMensajeAsistente(conversacionId, respuesta);

        return new ChatResponse
        {
            Respuesta = respuesta,
            Titulo = tituloActualizado
        };
    }
}