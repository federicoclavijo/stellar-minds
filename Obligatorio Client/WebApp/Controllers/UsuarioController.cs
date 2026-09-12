using ExpensesApp.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.Auxiliar;
using WebApp.Enums;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private string baseUrl = "http://ObligatorioAPI.somee.com/api/Usuario";

        [LoginFilter] //Pantalla principal
        public IActionResult Index()
        {
            ViewBag.Nombre = HttpContext.Session.GetString("nombre");

            return View();
        }

        [LoginFilter] //RF-02 : Alta de socios
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("rol") != "Administrador")
            {
                return RedirectToAction("Index");
            }
            return View(new UsuarioModel());
        }

        [HttpPost]
        [LoginFilter] //RF-02 : Alta de socios
        public IActionResult Create(UsuarioModel usuario)
        {
            if (HttpContext.Session.GetString("rol") != "Administrador")
            {
                return RedirectToAction("Index");
            }
            string token = HttpContext.Session.GetString("token");
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl, VerbosHttp.POST, usuario, token);
            if (!respuesta.IsSuccessStatusCode)
            {
                string error = ClienteHttpAuxiliar.ObtenerBody(respuesta);
                ViewBag.Error = error;
            }
            else
            {
                ViewBag.Mensaje = "Usuario creado con éxito";
            }
            return View(usuario);
        }

        [HttpGet] //RF-01 : Login
        public IActionResult Login()
        {
            string token = HttpContext.Session.GetString("token");
            if (!string.IsNullOrEmpty(token))
            {
                return View("Index");
            }

            return View(new LoginModel());
        }

        [HttpPost] //RF-01 : Login
        public IActionResult Login(LoginModel usuario)
        {
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl + "/login", VerbosHttp.POST, usuario);
            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            if (respuesta.IsSuccessStatusCode)
            {

                LoginModel login = JsonConvert.DeserializeObject<LoginModel>(body);
                HttpContext.Session.SetString("token", login.Token);
                HttpContext.Session.SetString("rol", login.Rol);
                HttpContext.Session.SetString("nombre", login.Nombre);
                HttpContext.Session.SetInt32("usuarioId", login.Id);
                return RedirectToAction("Index", new { mensaje = "Bienvenido " + login.Nombre });
            }
            else
            {
                ViewBag.Error = body;
            }
            return View(usuario);
        }

        //RF-01 : Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("rol");
            HttpContext.Session.Remove("nombre");
            HttpContext.Session.Remove("usuarioId");
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
