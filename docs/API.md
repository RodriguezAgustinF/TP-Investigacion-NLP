# API y flujo HTTP

## Autenticación

La aplicación usa una sesión con la clave `UsuarioId`. No emplea JWT. Las solicitudes al endpoint del chat deben provenir de un usuario con sesión activa.

## Enviar un mensaje

`POST /api/chat/enviar`

### Cuerpo

```json
{
  "conversacionId": 12,
  "mensaje": "¿Qué aporta RAG al proyecto?",
  "provider": 1
}
```

Valores de `provider`:

| Valor | Proveedor |
|---:|---|
| 1 | Ollama |
| 2 | GitHub Models |
| 3 | OpenAI |

### Respuesta correcta

```json
{
  "respuesta": "RAG permite recuperar información interna...",
  "titulo": "¿Qué aporta RAG al proyecto?"
}
```

### Validaciones

- Debe existir una sesión autenticada.
- `conversacionId` debe ser positivo.
- El mensaje no puede estar vacío ni superar 4000 caracteres.
- El proveedor debe existir en `LLMProvider`.
- La conversación debe pertenecer al usuario autenticado.

### Errores

| Estado | Uso |
|---:|---|
| 400 | Datos inválidos o mensaje demasiado largo. |
| 401 | No existe una sesión autenticada. |
| 404 | Conversación inexistente o ajena. |
| 500 | Error interno, del proveedor o de infraestructura. |

Para los errores internos se devuelve un mensaje genérico; el detalle técnico sólo se registra mediante `ILogger`.

## Acciones MVC

| Método | Ruta convencional | Función |
|---|---|---|
| GET | `/Chat/Index/{id?}` | Lista conversaciones y muestra una conversación. |
| POST | `/Chat/NuevaConversacion` | Crea una conversación para el usuario actual. |
| POST | `/Chat/EliminarConversacion/{id}` | Elimina una conversación propia y sus mensajes. |
| GET/POST | `/Usuario/Login` | Muestra y procesa el inicio de sesión. |
| GET/POST | `/Usuario/Registro` | Muestra y procesa el registro. |
| POST | `/Usuario/Logout` | Limpia la sesión. |

## Cliente web

`wwwroot/js/Chat/RecibirMensaje.js` intercepta el formulario, muestra el mensaje de forma optimista, llama al endpoint con `fetch`, reemplaza “Pensando...” por la respuesta y actualiza el título de la conversación.
