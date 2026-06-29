namespace Tp_Investigacion_NLP_Web.Models;

public class EnviarMensajeRequest
{
    public int ConversacionId { get; set; }

    public string Mensaje { get; set; } = string.Empty;

    public int Provider { get; set; }
}
