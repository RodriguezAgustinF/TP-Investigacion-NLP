using System;
using System.Collections.Generic;
using System.Text;
using Tp_Investigacion_NLP_Logica.Interfaces;

namespace Tp_Investigacion_NLP_Logica.Logica;

public class LLMServiceFactory : ILLMServiceFactory
{
    private readonly OllamaService _ollamaService;
    private readonly GithubModelsService _githubModelsService;
    private readonly OpenAIService _openAIService;

    public LLMServiceFactory(
        OllamaService ollamaService,
        GithubModelsService githubModelsService,
        OpenAIService openAIService)
    {
        _ollamaService = ollamaService;
        _githubModelsService = githubModelsService;
        _openAIService = openAIService;
    }

    public ILLMService GetService(LLMProvider provider)
    {
        return provider switch
        {
            LLMProvider.Ollama => _ollamaService,
            LLMProvider.Github => _githubModelsService,
            LLMProvider.OpenAI => _openAIService,
            _ => _ollamaService
        };
    }
}
