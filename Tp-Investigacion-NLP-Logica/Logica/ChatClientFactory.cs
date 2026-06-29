using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OllamaSharp;
using OpenAI;
using System.ClientModel;
using Tp_Investigacion_NLP_Logica.Interfaces;

namespace Tp_Investigacion_NLP_Logica.Logica;

/// <summary>
/// Adapta Ollama, GitHub Models y OpenAI a <see cref="IChatClient"/>.
/// </summary>
public sealed class ChatClientFactory : IChatClientFactory
{
    private readonly OllamaSettings _ollama;
    private readonly OpenAISettings _openAi;
    private readonly GithubModelsSettings _github;
    private readonly ILoggerFactory _loggerFactory;

    public ChatClientFactory(
        IOptions<OllamaSettings> ollama,
        IOptions<OpenAISettings> openAi,
        IOptions<GithubModelsSettings> github,
        ILoggerFactory loggerFactory)
    {
        _ollama = ollama.Value;
        _openAi = openAi.Value;
        _github = github.Value;
        _loggerFactory = loggerFactory;
    }

    /// <inheritdoc />
    public IChatClient Crear(LLMProvider provider)
    {
        IChatClient clienteBase = provider switch
        {
            LLMProvider.OpenAI => CrearOpenAi(),
            LLMProvider.Github => CrearGithubModels(),
            _ => new OllamaApiClient(new Uri(_ollama.BaseUrl), _ollama.Model)
        };

        return clienteBase
            .AsBuilder()
            .UseFunctionInvocation()
            .UseLogging(_loggerFactory)
            .Build();
    }

    private IChatClient CrearOpenAi()
    {
        ValidarSecreto(_openAi.ApiKey, "OPENAI_API_KEY");
        return new OpenAI.Chat.ChatClient(_openAi.Model, _openAi.ApiKey).AsIChatClient();
    }

    private IChatClient CrearGithubModels()
    {
        ValidarSecreto(_github.Token, "GITHUB_MODELS_TOKEN");

        var opciones = new OpenAIClientOptions
        {
            Endpoint = new Uri(_github.BaseUrl)
        };
        var cliente = new OpenAIClient(new ApiKeyCredential(_github.Token), opciones);

        return cliente.GetChatClient(_github.Model).AsIChatClient();
    }

    private static void ValidarSecreto(string valor, string variable)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new InvalidOperationException(
                $"Falta configurar el secreto {variable}. Usá User Secrets o una variable de entorno.");
        }
    }
}
