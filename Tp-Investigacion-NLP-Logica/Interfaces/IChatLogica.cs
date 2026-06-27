namespace Tp_Investigacion_NLP_Logica.Interfaces;

public interface IChatLogica
{
    Task EnviarMensajeAsync(int conversacionId, string mensaje);
}
