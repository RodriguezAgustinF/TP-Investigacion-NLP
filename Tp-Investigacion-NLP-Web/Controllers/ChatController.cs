using Microsoft.AspNetCore.Mvc;
using Tp_Investigacion_NLP_Entidades.Entidades;
using Tp_Investigacion_NLP_Logica.Interfaces;
using Tp_Investigacion_NLP_Web.Models;

namespace Tp_Investigacion_NLP_Web.Controllers;

public class ChatController : Controller
{
    private readonly IConversacionLogica _conversacionLogica;

    public ChatController(IConversacionLogica conversacionLogica)
    {
        _conversacionLogica = conversacionLogica;
    }

    [HttpGet]
    public IActionResult Index(int? id)
    {
        int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

        var conversaciones = _conversacionLogica.ObtenerConversaciones(usuarioId.Value);

        Conversacion? conversacionActual = null;

        if (id.HasValue)
        {
            conversacionActual = _conversacionLogica.ObtenerConversacion(
                id.Value,
                usuarioId.Value);
        }
        else if (conversaciones.Any())
        {
            conversacionActual = _conversacionLogica.ObtenerConversacion(
                conversaciones.First().Id,
                usuarioId.Value);
        }

        var viewModel = new ChatViewModel
        {
            Conversaciones = conversaciones,
            ConversacionActual = conversacionActual
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult NuevaConversacion()
    {
        int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

        var conversacion = _conversacionLogica.CrearConversacion(usuarioId.Value);

        return RedirectToAction(nameof(Index), new
        {
            id = conversacion.Id
        });
    }

    [HttpPost]
    public IActionResult EliminarConversacion(int id)
    {
        int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

        if (usuarioId == null)
        {
            return RedirectToAction("Login", "Usuario");
        }

        _conversacionLogica.EliminarConversacion(id, usuarioId.Value);

        return RedirectToAction(nameof(Index));
    }
}