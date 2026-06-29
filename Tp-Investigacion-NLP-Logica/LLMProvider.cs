using System;
using System.Collections.Generic;
using System.Text;

namespace Tp_Investigacion_NLP_Logica;

/// <summary>
/// Proveedores seleccionables. Sus valores numéricos forman parte del contrato REST.
/// </summary>
public enum LLMProvider
{
    /// <summary>Modelo local mediante Ollama.</summary>
    Ollama = 1,
    /// <summary>Modelos publicados mediante GitHub Models.</summary>
    Github = 2,
    /// <summary>API pública de OpenAI.</summary>
    OpenAI = 3
}
