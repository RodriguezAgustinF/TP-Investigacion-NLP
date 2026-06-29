using System.Text.Json.Serialization;

namespace Tp_Investigacion_NLP_Logica.DTO;

public class OpenAIMessage
{
    public string Role { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}