using Tp_Investigacion_NLP_Entidades.Entidades;

namespace Tp_Investigacion_NLP_Web.Models;

public class ChatViewModel
{
    public Conversacion? ConversacionActual { get; set; }

    public List<Conversacion> Conversaciones { get; set; } = new();

    public string NuevoMensaje { get; set; } = string.Empty;
}
