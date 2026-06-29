using Microsoft.Extensions.AI;

namespace Tp_Investigacion_NLP_Logica.Interfaces;

/// <summary>Construye clientes independientes del SDK concreto del proveedor.</summary>
public interface IChatClientFactory
{
    /// <summary>Crea una canalización con function calling y logging.</summary>
    IChatClient Crear(LLMProvider provider);
}
