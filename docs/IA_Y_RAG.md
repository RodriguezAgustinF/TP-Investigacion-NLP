# Integración de IA, Semantic Kernel y RAG

## Abstracción del proveedor

`ChatClientFactory` devuelve un `Microsoft.Extensions.AI.IChatClient`. Esto mantiene a `ChatbotService` independiente de los SDK concretos utilizados para conectar Ollama y GitHub Models.

La canalización aplica:

- `UseFunctionInvocation()`: procesa automáticamente solicitudes de herramientas.
- `UseLogging()`: registra actividad a través de `ILogger`.

## Proveedores

### Ollama

Usa `OllamaApiClient`, por defecto con `qwen3:8b`. Es local y no requiere credenciales externas.

### GitHub Models

GitHub Models expone una API compatible con el protocolo de OpenAI. Por eso la implementación reutiliza ese cliente técnico apuntándolo a `GithubModels:BaseUrl`, aunque OpenAI no se ofrece como proveedor seleccionable. Necesita `GithubModels:Token`.

## Construcción del contexto

`ChatbotService` agrega:

1. Un mensaje de sistema con idioma, alcance y reglas de seguridad.
2. Los últimos 20 mensajes de la conversación.
3. Las herramientas producidas desde el plugin de Semantic Kernel.
4. Temperatura `0.2` y un máximo de 800 tokens de salida.

El límite de historial reduce latencia y consumo, aunque no reemplaza una estrategia avanzada de resumen.

## Semantic Kernel

`ConocimientoPlugin.BuscarConocimiento` está marcado con `[KernelFunction]`. `ChatbotService` crea un `KernelPlugin`, lo transforma en funciones `AIFunction` y las entrega al modelo mediante `ChatOptions.Tools`.

El modelo decide si necesita invocar la función. `UseFunctionInvocation()` ejecuta la llamada y devuelve el resultado al modelo para que redacte la respuesta final.

## Recuperación tipo RAG

La base interna contiene fragmentos breves sobre NLP, arquitectura, proveedores, RAG, seguridad y buenas prácticas. La búsqueda:

1. Normaliza mayúsculas y acentos.
2. Tokeniza la consulta.
3. Puntúa cada entrada por términos coincidentes.
4. Devuelve como máximo cuatro fragmentos.

Es una demostración educativa de recuperación, no un RAG vectorial productivo. No utiliza embeddings ni una base vectorial.

## Cómo ampliar el conocimiento

Para una ampliación pequeña se agregan entradas a `ConocimientoPlugin`. Para un volumen grande conviene reemplazar el arreglo interno por:

- documentos segmentados;
- `IEmbeddingGenerator`;
- una base vectorial;
- metadatos y citas de la fuente;
- filtros por usuario o dominio;
- evaluación de relevancia y respuestas fundamentadas.

## Limitaciones

- La calidad del function calling depende del modelo elegido.
- No hay streaming de tokens.
- No se registran métricas específicas de tokens o costo.
- El RAG actual no incluye citas navegables.
- Un error del proveedor se informa al usuario de forma genérica y queda en logs.
