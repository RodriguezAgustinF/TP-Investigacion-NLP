using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using Tp_Investigacion_NLP_Entidades.Entidades;
using Tp_Investigacion_NLP_Logica.DTO;
using Tp_Investigacion_NLP_Logica.Interfaces;

namespace Tp_Investigacion_NLP_Logica.Logica;

public class OllamaService : ILLMService
{
    private readonly HttpClient _httpClient;
    private readonly OllamaSettings _settings;

    public OllamaService(
        HttpClient httpClient,
        IOptions<OllamaSettings> options)
    {
        _httpClient = httpClient;
        _settings = options.Value;
    }

    public async Task<string> ObtenerRespuestaAsync(Conversacion conversacion)
    {
        var request = new OllamaRequest
        {
            Model = _settings.Model,
            Stream = false
        };

        request.Messages.Add(new OllamaMessage
        {
            Role = "system",
            Content =
"""
Sos un asistente universitario.

Respondé siempre en español.

Si no conocés una respuesta decilo.

Más adelante vas a poder consultar una base de datos con universidades, carreras y materias.
"""
        });

        foreach (var mensaje in conversacion.Mensajes.OrderBy(x => x.Fecha))
        {
            request.Messages.Add(new OllamaMessage
            {
                Role = mensaje.Rol,
                Content = mensaje.Contenido
            });
        }

        var json = JsonSerializer.Serialize(request);

        var response = await _httpClient.PostAsync(
            $"{_settings.BaseUrl}/api/chat",
            new StringContent(json, Encoding.UTF8, "application/json"));

        response.EnsureSuccessStatusCode();

        var contenido = await response.Content.ReadAsStringAsync();

        var respuesta = JsonSerializer.Deserialize<OllamaResponse>(
            contenido,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return respuesta?.Message?.Content ?? "";
    }
}
