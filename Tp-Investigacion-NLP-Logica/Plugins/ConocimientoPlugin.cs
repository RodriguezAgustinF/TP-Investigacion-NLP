using System.ComponentModel;
using System.Globalization;
using System.Text;
using Microsoft.SemanticKernel;

namespace Tp_Investigacion_NLP_Logica.Plugins;

/// <summary>
/// Fuente interna del TP expuesta al modelo como herramienta de recuperación léxica.
/// </summary>
public sealed class ConocimientoPlugin
{
    private static readonly EntradaConocimiento[] Entradas =
    [
        new("nlp chatbot", "Un NLP chatbot interpreta mensajes en lenguaje natural, conserva contexto, decide acciones y genera respuestas."),
        new("arquitectura dotnet", "La solución usa ASP.NET Core MVC y una API REST, una capa lógica, Entity Framework Core y proveedores de modelos desacoplados mediante IChatClient."),
        new("proveedores modelos", "El prototipo permite seleccionar Ollama local o GitHub Models sin cambiar la lógica conversacional."),
        new("semantic kernel", "Semantic Kernel registra funciones nativas como plugins. El modelo puede invocarlas automáticamente para recuperar información verificable."),
        new("rag recuperación", "RAG recupera información de una fuente interna antes de generar la respuesta. Reduce alucinaciones y permite fundamentar respuestas del dominio."),
        new("seguridad secretos", "Las claves y tokens deben suministrarse con variables de entorno, User Secrets o un almacén como Azure Key Vault; nunca deben versionarse."),
        new("riesgos", "Los riesgos principales son alucinaciones, exposición de datos sensibles, prompt injection, costos, latencia y dependencia del proveedor."),
        new("buenas prácticas", "Se recomienda usar inyección de dependencias, validación de entradas, autorización por recurso, logging, cancelación, límites de tamaño y pruebas automatizadas."),
        new("microsoft extensions ai", "Microsoft.Extensions.AI aporta IChatClient, herramientas, function calling, telemetría y una canalización común para distintos proveedores."),
        new("net 10", ".NET 10 es la versión objetivo de este proyecto y permite utilizar las abstracciones actuales de Microsoft.Extensions.AI.")
    ];

    /// <summary>Recupera hasta cuatro fragmentos relevantes para una consulta.</summary>
    [KernelFunction("buscar_conocimiento")]
    [Description("Busca información verificable del trabajo práctico sobre NLP chatbots, arquitectura .NET, seguridad, RAG y tecnologías utilizadas.")]
    public string BuscarConocimiento(
        [Description("Pregunta o conceptos que se desean buscar.")] string consulta)
    {
        if (string.IsNullOrWhiteSpace(consulta))
        {
            return "No se indicó una consulta.";
        }

        string[] terminos = Tokenizar(consulta);

        var resultados = Entradas
            .Select(entrada =>
            {
                var palabrasEntrada = Tokenizar($"{entrada.Tema} {entrada.Contenido}")
                    .ToHashSet();

                return new
                {
                    Entrada = entrada,
                    Puntaje = terminos.Count(palabrasEntrada.Contains)
                };
            })
            .Where(x => x.Puntaje > 0)
            .OrderByDescending(x => x.Puntaje)
            .Take(4)
            .Select(x => $"[{x.Entrada.Tema}] {x.Entrada.Contenido}")
            .ToArray();

        return resultados.Length == 0
            ? "La base interna no contiene información específica para esa consulta."
            : string.Join(Environment.NewLine, resultados);
    }

    private static string Normalizar(string valor)
    {
        string descompuesto = valor.ToLowerInvariant().Normalize(NormalizationForm.FormD);
        var resultado = new StringBuilder(descompuesto.Length);

        foreach (char caracter in descompuesto)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(caracter) != UnicodeCategory.NonSpacingMark)
            {
                resultado.Append(caracter);
            }
        }

        return resultado.ToString().Normalize(NormalizationForm.FormC);
    }

    private static string[] Tokenizar(string valor) => Normalizar(valor)
        .Split(
            [' ', '.', ',', ';', ':', '¿', '?', '¡', '!', '(', ')', '/', '-'],
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Where(x => x.Length > 2)
        .Distinct()
        .ToArray();

    private sealed record EntradaConocimiento(string Tema, string Contenido);
}
