# Guía personal de tecnologías del TP

Esta guía está escrita para entender el proyecto y explicarlo oralmente. Distingue entre tecnologías implementadas y alternativas mencionadas en la investigación.

## El proyecto en una oración

Es una aplicación web en .NET que permite conversar con modelos de lenguaje, guardar el historial y ofrecerle al modelo información interna mediante herramientas controladas.

```text
Navegador
   ↓
ASP.NET Core MVC + API REST
   ↓
ChatLogica → ChatbotService
   ├── IChatClient → Ollama / GitHub Models
   ├── Semantic Kernel → ConocimientoPlugin
   └── EF Core → SQL Server
```

## NLP, chatbot y LLM

### NLP

NLP significa *Natural Language Processing*. Es el área de la IA que procesa lenguaje humano: clasificación, extracción de datos, resumen, traducción y generación de respuestas.

### Chatbot

El chatbot es el sistema completo: interfaz, autenticación, historial, modelo, herramientas, validaciones y persistencia. El modelo es sólo una pieza.

### LLM

Un LLM es un modelo entrenado con grandes cantidades de texto para interpretar y generar lenguaje. Puede responder de forma convincente aunque esté equivocado; esto se llama alucinación.

> Respuesta oral: “Usamos modelos generativos para interpretar mensajes y mantener conversaciones, pero el control, la memoria y la seguridad dependen de nuestra aplicación”.

## Prompt y contexto

El prompt no es sólo la pregunta visible. Incluye:

- instrucciones del sistema;
- historial de mensajes;
- pregunta actual;
- resultados de herramientas;
- restricciones de respuesta.

`ChatbotService` agrega instrucciones para responder en español, reconocer límites y usar información interna cuando corresponda. También envía los últimos 20 mensajes para conservar contexto sin crecer indefinidamente.

## ASP.NET Core, MVC y Razor

### ASP.NET Core

Es el framework web de Microsoft. Recibe solicitudes, ejecuta controladores, maneja sesiones, configuración, dependencias y middleware.

### MVC

- **Model:** datos utilizados por la pantalla.
- **View:** interfaz `.cshtml`.
- **Controller:** recibe solicitudes y coordina acciones.

### Razor

Razor combina HTML con C#. Genera la pantalla inicial y muestra conversaciones almacenadas.

### Minimal API

La PPT muestra una Minimal API como ejemplo. El proyecto usa `ChatApiController`. Ambas son formas válidas de crear endpoints ASP.NET Core; elegimos controladores para mantener una estructura más clara.

## API REST, JSON y JavaScript

El navegador envía un JSON mediante:

```http
POST /api/chat/enviar
```

La API responde otro JSON con la respuesta y el título. `RecibirMensaje.js` utiliza `fetch` para hacerlo sin recargar toda la página.

REST es el mecanismo de comunicación; no es una tecnología de IA.

## Bootstrap y CSS

Bootstrap aporta grillas, formularios, navegación y utilidades responsive. `site.css` agrega la identidad visual propia. Bootstrap es la base; el CSS personalizado define el diseño final.

## Inyección de dependencias

ASP.NET Core crea y entrega los servicios mediante sus constructores. Así las clases no construyen manualmente todas sus dependencias.

Ventajas:

- menor acoplamiento;
- cambio sencillo de implementaciones;
- mejor capacidad de prueba;
- configuración centralizada.

Ejemplo: `ChatLogica` depende de `IChatbotService`, no crea directamente un cliente de Ollama.

## Microsoft.Extensions.AI e IChatClient

`Microsoft.Extensions.AI` ofrece abstracciones comunes para IA en .NET. El proyecto utiliza `IChatClient`.

```text
ChatbotService → IChatClient → proveedor concreto
```

Esto permite que la lógica conversacional funcione igual con Ollama o GitHub Models. También permite agregar logging y ejecución automática de herramientas mediante una canalización.

> Respuesta oral: “IChatClient desacopla nuestra lógica del proveedor. Cambiar de modelo no obliga a reescribir el chatbot”.

## Semantic Kernel

Semantic Kernel conecta modelos con funciones de una aplicación. Una función C# marcada con `[KernelFunction]` puede convertirse en una herramienta que el modelo conoce.

En el proyecto, `ConocimientoPlugin` ofrece `buscar_conocimiento`.

Semantic Kernel no reemplaza al modelo. Le permite utilizar capacidades controladas de nuestro programa.

## Function calling

Flujo de una herramienta:

1. La aplicación informa qué funciones están disponibles.
2. El modelo decide si necesita una.
3. Solicita la función con argumentos estructurados.
4. La aplicación ejecuta el código C# permitido.
5. El resultado vuelve al modelo.
6. El modelo redacta la respuesta final.

El modelo no ejecuta código libremente. Sólo puede solicitar herramientas registradas. `UseFunctionInvocation()` automatiza este ciclo.

## RAG

RAG significa *Retrieval-Augmented Generation*: buscar información interna antes de generar una respuesta.

El proyecto implementa una recuperación léxica tipo RAG:

1. normaliza acentos y mayúsculas;
2. separa la consulta en palabras;
3. puntúa fragmentos internos;
4. devuelve los más relevantes;
5. el modelo los usa como contexto.

No utiliza embeddings ni base vectorial. Por eso conviene presentarlo así:

> “Implementamos una recuperación interna tipo RAG mediante Semantic Kernel. Es una demostración léxica, no un RAG vectorial productivo”.

### Embeddings y base vectorial

Un embedding convierte texto en un vector numérico que representa su significado. Una base vectorial permite encontrar vectores similares aunque las palabras no coincidan exactamente.

Son una evolución posible, pero no están implementados actualmente.

## Ollama

Ollama permite ejecutar modelos localmente. El proyecto usa `qwen3:8b`.

Ventajas:

- no cobra por token;
- los mensajes permanecen en la computadora;
- sirve para desarrollo y demostraciones.

Limitaciones:

- consume recursos locales;
- depende del hardware;
- algunos modelos manejan peor las herramientas.

## GitHub Models

GitHub Models permite consumir modelos remotos mediante un token. Su API es compatible con el protocolo de OpenAI; por eso existen paquetes técnicos con “OpenAI” en el nombre aunque OpenAI no sea una opción visible.

No debe confundirse con GitHub Copilot: Models es una API; Copilot es un asistente para programadores.

## OpenAI, Azure OpenAI, Entra ID y CLU

Estas tecnologías forman parte de la investigación, pero no están implementadas como proveedores del proyecto.

### OpenAI

Proveedor original de modelos GPT mediante una API paga.

### Azure OpenAI

Ofrece modelos de OpenAI dentro de Azure con capacidades empresariales: regiones, redes privadas, gobernanza e integración con servicios Microsoft.

### Microsoft Entra ID

Servicio de identidad de Microsoft. Puede autenticar aplicaciones en Azure sin guardar una API key tradicional.

### CLU

*Conversational Language Understanding* clasifica intenciones y extrae entidades en dominios entrenados.

Ejemplo:

```text
“Reservame un turno mañana”
Intención: reservar_turno
Entidad fecha: mañana
```

Es un enfoque más estructurado que un chatbot generativo.

## Entity Framework Core y SQL Server

EF Core es un ORM: permite trabajar con tablas mediante clases C# y consultas LINQ.

```text
Usuario      ↔ Usuarios
Conversacion ↔ Conversaciones
Mensaje      ↔ Mensajes
```

SQL Server guarda usuarios, chats y mensajes. El modelo no conserva memoria por sí mismo; la aplicación recupera el historial y lo envía nuevamente.

### Migraciones

Las migraciones describen cambios en la estructura de la base: crear tablas, agregar columnas o relaciones. El proyecto aplica las migraciones pendientes al iniciar.

## Sesiones, autenticación y autorización

La sesión guarda `UsuarioId` para identificar al usuario conectado.

- **Autenticación:** determina quién es el usuario.
- **Autorización:** determina qué puede hacer.

Antes de escribir en una conversación se verifica que pertenezca al usuario. Las contraseñas se guardan mediante hash, no como texto.

## Middleware

Un middleware es una etapa del pipeline HTTP.

- `AuthMiddleware`: protege rutas privadas.
- `ApiExceptionMiddleware`: registra errores y evita revelar detalles internos.

## Logging y telemetría

Logging registra eventos técnicos. La telemetría completa incluiría además métricas y trazas: latencia, errores, tokens, costos y herramientas utilizadas.

El proyecto tiene logging básico. La telemetría avanzada queda como mejora futura.

## DTO

Un DTO transporta datos entre capas o por HTTP sin exponer entidades completas.

- `EnviarMensajeRequest`: mensaje recibido por la API.
- `ChatResponse`: resultado devuelto por la lógica.

## Pruebas automatizadas

El proyecto utiliza xUnit. Las pruebas actuales comprueban que la recuperación:

- encuentra información de RAG;
- ignora diferencias de acentos;
- no inventa resultados desconocidos.

## Guardrails y seguridad

Guardrails son límites alrededor del modelo:

- validación de entradas;
- límite de longitud;
- herramientas permitidas;
- secretos fuera del código;
- autorización por conversación;
- instrucciones del sistema;
- manejo seguro de errores.

Reducen riesgos, pero no garantizan respuestas correctas.

## Preguntas probables de la exposición

### ¿Por qué no llaman a Ollama desde el controlador?

Porque separar la lógica facilita pruebas, mantenimiento y cambio de proveedor.

### ¿Para qué sirve IChatClient?

Para utilizar una interfaz común independientemente de si el modelo viene de Ollama o GitHub Models.

### ¿Para qué sirve Semantic Kernel?

Para exponer funciones C# como herramientas controladas que el modelo puede solicitar.

### ¿Implementaron RAG?

Implementamos recuperación interna tipo RAG mediante coincidencia léxica. No es RAG vectorial.

### ¿El modelo ejecuta código libremente?

No. Sólo solicita funciones previamente registradas por la aplicación.

### ¿Cuál es la diferencia entre Ollama y GitHub Models?

Ollama ejecuta el modelo localmente. GitHub Models llama a un servicio remoto y necesita un token.

### ¿Qué mejorarían para producción?

Identity, rate limiting, telemetría completa, RAG vectorial con fuentes, streaming, sesiones distribuidas y más pruebas de integración y seguridad.

## Resumen para memorizar

> Construimos un chatbot con ASP.NET Core y arquitectura por capas. Microsoft.Extensions.AI permite usar proveedores mediante IChatClient. Semantic Kernel expone funciones C# como herramientas y usamos recuperación léxica tipo RAG para aportar conocimiento interno. Ollama permite ejecución local y GitHub Models ofrece una alternativa remota. Entity Framework Core y SQL Server guardan usuarios, conversaciones y mensajes. La solución agrega autenticación, autorización, logging, validaciones y pruebas básicas.
