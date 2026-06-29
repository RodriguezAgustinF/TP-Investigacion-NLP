using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Tp_Investigacion_NLP_Logica.DTO;

public class OpenAIResponse
{
    [JsonPropertyName("choices")]
    public List<OpenAIChoice> Choices { get; set; } = new();
}