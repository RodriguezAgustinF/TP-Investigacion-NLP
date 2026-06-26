using Tp_Investigacion_NLP_Entidades.Entidades;

namespace Tp_Investigacion_NLP_Logica.Interfaces;

public interface IConversacionLogica
{
    Conversacion CrearConversacion(int usuarioId);
    List<Conversacion> ObtenerConversaciones(int usuarioId);
    Conversacion ObtenerConversacion(int conversacionId, int usuarioId);
}
