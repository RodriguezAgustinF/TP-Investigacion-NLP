using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Tp_Investigacion_NLP_Logica.DTO;

public class OpenAIRequest
{
    public string Model { get; set; } = string.Empty;
    public List<OpenAIMessage> Messages { get; set; } = new();
    public double Temperature { get; set; } = 0.7;
    public int MaxTokens { get; set; } = 800;
}
