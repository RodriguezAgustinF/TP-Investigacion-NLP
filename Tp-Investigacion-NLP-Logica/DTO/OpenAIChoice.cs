using System.Text.Json.Serialization;

namespace Tp_Investigacion_NLP_Logica.DTO;

public class OpenAIChoice
{
    public OpenAIMessage Message { get; set; } = new();
}