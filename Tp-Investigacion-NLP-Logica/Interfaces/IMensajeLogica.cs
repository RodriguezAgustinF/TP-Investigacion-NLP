using Tp_Investigacion_NLP_Entidades.Entidades;

namespace Tp_Investigacion_NLP_Logica.Interfaces;

public interface IMensajeLogica
{
    void AgregarMensajeUsuario(int conversacionId, string contenido);

    void AgregarMensajeAsistente(int conversacionId, string contenido);

}
