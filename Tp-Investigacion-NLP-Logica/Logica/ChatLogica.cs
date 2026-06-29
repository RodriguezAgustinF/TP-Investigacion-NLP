using Tp_Investigacion_NLP_Logica.DTO;
using Tp_Investigacion_NLP_Logica.Interfaces;

namespace Tp_Investigacion_NLP_Logica.Logica;

public class ChatLogica : IChatLogica
{
    private readonly IMensajeLogica _mensajeLogica;
    private readonly IConversacionLogica _conversacionLogica;
    private readonly IChatbotService _chatbotService;

    public ChatLogica(
        IMensajeLogica mensajeLogica,
        IConversacionLogica conversacionLogica,
        IChatbotService chatbotService)
    {
        _mensajeLogica = mensajeLogica;
        _conversacionLogica = conversacionLogica;
        _chatbotService = chatbotService;
    }

    public async Task<ChatResponse> EnviarMensajeAsync(
        int conversacionId,
        int usuarioId,
        string mensaje,
        LLMProvider provider,
        CancellationToken cancellationToken = default)
    {
        if (mensaje.Length > 4000)
        {
            throw new ArgumentException("El mensaje no puede superar los 4000 caracteres.", nameof(mensaje));
        }

        var conversacionAutorizada = _conversacionLogica.ObtenerConversacion(
            conversacionId,
            usuarioId);

        if (conversacionAutorizada == null)
        {
            throw new KeyNotFoundException("La conversación no existe o no pertenece al usuario.");
        }

        _mensajeLogica.AgregarMensajeUsuario(conversacionId, mensaje);

        string tituloActualizado = _conversacionLogica.ActualizarTituloSiEsNueva(
            conversacionId,
            mensaje);

        var conversacion = _conversacionLogica.ObtenerConversacionCompleta(conversacionId);

        if (conversacion == null)
        {
            throw new InvalidOperationException("No se encontró la conversación.");
        }

        string respuesta = await _chatbotService.ObtenerRespuestaAsync(
            conversacion,
            provider,
            cancellationToken);

        _mensajeLogica.AgregarMensajeAsistente(conversacionId, respuesta);

        return new ChatResponse
        {
            Respuesta = respuesta,
            Titulo = tituloActualizado
        };
    }
}
