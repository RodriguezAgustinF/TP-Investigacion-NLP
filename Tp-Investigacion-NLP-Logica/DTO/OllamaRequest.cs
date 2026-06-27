namespace Tp_Investigacion_NLP_Logica.DTO;

public class OllamaRequest
{
    public string Model { get; set; } = string.Empty;

    public List<OllamaMessage> Messages { get; set; } = new();

    public bool Stream { get; set; } = false;
}