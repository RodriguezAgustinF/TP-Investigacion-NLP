using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Tp_Investigacion_NLP_Entidades.Entidades;
using Tp_Investigacion_NLP_Logica.DTO;
using Tp_Investigacion_NLP_Logica.Interfaces;

namespace Tp_Investigacion_NLP_Logica.Logica;

public class GithubModelsService : ILLMService
{
    private readonly HttpClient _httpClient;
    private readonly GithubModelsSettings _settings;

    public GithubModelsService(
        HttpClient httpClient,
        IOptions<GithubModelsSettings> options)
    {
        _httpClient = httpClient;
        _settings = options.Value;
    }

    public async Task<string> ObtenerRespuestaAsync(Conversacion conversacion)
    {
        var request = new GithubModelsRequest
        {
            Model = _settings.Model,
            Temperature = 0.7,
            Max_Tokens = 800
        };

        request.Messages.Add(new GithubModelsMessage
        {
            Role = "system",
            Content =
"""
Sos un asistente de inteligencia artificial.

Respondé siempre en español.

Respondé de forma clara, útil y natural.

Si no sabés algo o no tenés información actualizada, aclaralo.
"""
        });

        foreach (var mensaje in conversacion.Mensajes.OrderBy(m => m.Fecha))
        {
            request.Messages.Add(new GithubModelsMessage
            {
                Role = mensaje.Rol,
                Content = mensaje.Contenido
            });
        }

        var json = JsonSerializer.Serialize(
            request,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });

        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            $"{_settings.BaseUrl}/chat/completions");

        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", _settings.Token);

        httpRequest.Headers.Add("X-GitHub-Api-Version", "2022-11-28");

        httpRequest.Content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.SendAsync(httpRequest);

        string contenido = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Error de GitHub Models: {response.StatusCode} - {contenido}");
        }

        var respuesta = JsonSerializer.Deserialize<GithubModelsResponse>(
            contenido,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return respuesta?
            .Choices?
            .FirstOrDefault()?
            .Message?
            .Content?
            .Trim()
            ?? "GitHub Models no devolvió una respuesta.";
    }
}