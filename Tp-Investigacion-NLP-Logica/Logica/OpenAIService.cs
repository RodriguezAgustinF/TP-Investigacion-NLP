using Microsoft.Extensions.Options;
using OpenAI.Chat;
using Tp_Investigacion_NLP_Entidades.Entidades;
using Tp_Investigacion_NLP_Logica.Interfaces;

namespace Tp_Investigacion_NLP_Logica.Logica;

public class OpenAIService : ILLMService
{
    private readonly ChatClient _client;
    private readonly OpenAISettings _settings;

    public OpenAIService(IOptions<OpenAISettings> options)
    {
        _settings = options.Value;

        _client = new ChatClient(
            model: _settings.Model,
            apiKey: _settings.ApiKey);
    }

    public async Task<string> ObtenerRespuestaAsync(Conversacion conversacion)
    {
        var mensajes = new List<ChatMessage>();

        mensajes.Add(new SystemChatMessage("""
            Sos un asistente universitario.

            Podés responder preguntas generales.

            Más adelante vas a poder consultar una base de datos con universidades, carreras y materias.
            """));

        foreach (var mensaje in conversacion.Mensajes.OrderBy(m => m.Fecha))
        {
            if (mensaje.Rol == "user")
                mensajes.Add(new UserChatMessage(mensaje.Contenido));
            else
                mensajes.Add(new AssistantChatMessage(mensaje.Contenido));
        }

        ChatCompletion completion =
            await _client.CompleteChatAsync(mensajes);

        return completion.Content[0].Text;
    }
}
