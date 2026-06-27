using Tp_Investigacion_NLP_Logica.Interfaces;

namespace Tp_Investigacion_NLP_Logica.Logica;

public class ChatLogica : IChatLogica
{
    private readonly IMensajeLogica _mensajeLogica;
    private readonly IConversacionLogica _conversacionLogica;
    private readonly ILLMService _llmService;

    public ChatLogica(IMensajeLogica mensajeLogica, IConversacionLogica conversacionLogica, ILLMService llmService)
    {
        _mensajeLogica = mensajeLogica;
        _conversacionLogica = conversacionLogica;
        _llmService = llmService;
    }

    public async Task EnviarMensajeAsync(int conversacionId, string mensaje)
    {
        _mensajeLogica.AgregarMensajeUsuario(conversacionId, mensaje);

        var conversacion = _conversacionLogica.ObtenerConversacionCompleta(conversacionId);

        string respuesta = await _llmService.ObtenerRespuestaAsync(conversacion);

        _mensajeLogica.AgregarMensajeAsistente(conversacionId, respuesta);
    }
}
