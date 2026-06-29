using Microsoft.AspNetCore.Mvc;
using Tp_Investigacion_NLP_Logica;
using Tp_Investigacion_NLP_Logica.Interfaces;
using Tp_Investigacion_NLP_Web.Models;

namespace Tp_Investigacion_NLP_Web.Controllers;

[Route("api/chat")]
[ApiController]
public class ChatApiController : ControllerBase
{
    private readonly IChatLogica _chatLogica;

    public ChatApiController(IChatLogica chatLogica)
    {
        _chatLogica = chatLogica;
    }

    [HttpPost("enviar")]
    public async Task<IActionResult> Enviar(
        [FromBody] EnviarMensajeRequest request,
        CancellationToken cancellationToken)
    {
        int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

        if (usuarioId == null)
        {
            return Unauthorized(new { error = "Usuario no autenticado." });
        }

        if (request.ConversacionId <= 0)
        {
            return BadRequest(new { error = "La conversación no es válida." });
        }

        if (string.IsNullOrWhiteSpace(request.Mensaje))
        {
            return BadRequest(new { error = "El mensaje no puede estar vacío." });
        }

        if (!Enum.IsDefined(typeof(LLMProvider), request.Provider))
        {
            return BadRequest(new { error = "El proveedor seleccionado no es válido." });
        }

        var resultado = await _chatLogica.EnviarMensajeAsync(
            request.ConversacionId,
            usuarioId.Value,
            request.Mensaje.Trim(),
            (LLMProvider)request.Provider,
            cancellationToken);

        return Ok(new
        {
            respuesta = resultado.Respuesta,
            titulo = resultado.Titulo
        });
    }
}
