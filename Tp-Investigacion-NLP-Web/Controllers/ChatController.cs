using Microsoft.AspNetCore.Mvc;
using Tp_Investigacion_NLP_Entidades.Entidades;
using Tp_Investigacion_NLP_Logica.Interfaces;
using Tp_Investigacion_NLP_Web.Models;

namespace Tp_Investigacion_NLP_Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly IConversacionLogica _conversacionLogica;
        private readonly IChatLogica _chatLogica;

        public ChatController(IConversacionLogica conversacionLogica, IChatLogica chatLogica)
        {
            _conversacionLogica = conversacionLogica;
            _chatLogica = chatLogica;
        }

        [HttpGet]
        public IActionResult Index(int? id)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
                return RedirectToAction("Login", "Usuario");

            var conversaciones = _conversacionLogica.ObtenerConversaciones(usuarioId.Value);

            Conversacion? conversacionActual = null;

            if (id.HasValue)
            {
                conversacionActual = _conversacionLogica.ObtenerConversacion(id.Value, usuarioId.Value);
            }
            else if (conversaciones.Any())
            {
                conversacionActual = conversaciones.First();
            }

            var vm = new ChatViewModel
            {
                Conversaciones = conversaciones,
                ConversacionActual = conversacionActual
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult NuevaConversacion()
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
                return RedirectToAction("Login", "Usuario");

            var conversacion = _conversacionLogica.CrearConversacion(usuarioId.Value);

            return RedirectToAction(nameof(Index), new
            {
                id = conversacion.Id
            });
        }

        [HttpPost]
        public async Task<IActionResult> EnviarMensaje(int conversacionId, string nuevoMensaje)
        {
            if (string.IsNullOrWhiteSpace(nuevoMensaje))
            {
                return RedirectToAction(nameof(Index), new
                {
                    id = conversacionId
                });
            }

            await _chatLogica.EnviarMensajeAsync(conversacionId, nuevoMensaje);

            return RedirectToAction(nameof(Index), new
            {
                id = conversacionId
            });
        }
    }
}