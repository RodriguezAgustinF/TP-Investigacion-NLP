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
public async Task<IActionResult> Enviar([FromBody] EnviarMensajeRequest request)
{
    try
    {
        int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

        if (usuarioId == null)
        {
            return Unauthorized(new
            {
                error = "Usuario no autenticado."
            });
        }

        if (request.ConversacionId <= 0)
        {
            return BadRequest(new
            {
                error = "La conversación no es válida."
            });
        }

        if (string.IsNullOrWhiteSpace(request.Mensaje))
        {
            return BadRequest(new
            {
                error = "El mensaje no puede estar vacío."
            });
        }

        var provider = (LLMProvider)request.Provider;

            var resultado = await _chatLogica.EnviarMensajeAsync(
        request.ConversacionId,
        request.Mensaje,
        provider);

            return Ok(new
            {
                respuesta = resultado.Respuesta,
                titulo = resultado.Titulo
            });
        }
    catch (Exception ex)
    {
        return StatusCode(500, new
        {
            error = ex.Message,
            detalle = ex.InnerException?.Message
        });
    }
}
}