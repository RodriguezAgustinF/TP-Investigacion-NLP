using Microsoft.AspNetCore.Mvc;
using Tp_Investigacion_NLP_Entidades.Entidades;
using Tp_Investigacion_NLP_Logica.Interfaces;
using Tp_Investigacion_NLP_Web.Models;

namespace Tp_Investigacion_NLP_Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioLogica _usuarioLogica;

        public UsuarioController(IUsuarioLogica usuarioLogica)
        {
            _usuarioLogica = usuarioLogica;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            Usuario usuario;

            try
            {
                usuario = _usuarioLogica.LoginUsuario(loginViewModel.Email, loginViewModel.Password);

            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(loginViewModel);
            }

            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("UsuarioEmail", usuario.Email);

            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Chat");
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(RegistroViewModel registrarViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registrarViewModel);
            }

            try
            {
                _usuarioLogica.RegistrarUsuario(RegistroViewModel.ToEntity(registrarViewModel));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(registrarViewModel.Email), ex.Message);
                return View(registrarViewModel);
            }


            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
