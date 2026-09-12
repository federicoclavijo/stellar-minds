using ExpensesApp.Filter;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using WebApp.Auxiliar;
using WebApp.Enums;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EquipoController : Controller
    {
        private string baseUrl = "http://ObligatorioAPI.somee.com/api/Equipo";

        [LoginFilter]
        public IActionResult Index() //RF-03 : Listado de equipos con enlace a Alta, Modificación y Baja
        {
            if (HttpContext.Session.GetString("rol") != "Administrador")
            {
                return RedirectToAction("Index", "Usuario");
            }
            IEnumerable<EquipoModel> equipos = CargarIndex();
            return View(equipos);
        }

        [LoginFilter] //RF-03 : Alta de equipo
        public IActionResult Create(string tipoEquipo)
        {
            if (HttpContext.Session.GetString("rol") != "Administrador")
            {
                return RedirectToAction("Index", "Usuario");
            }
            EquipoModel modelo = new EquipoModel();
            modelo.TipoEquipo = tipoEquipo;
            return View(modelo);
        }

        [HttpPost]
        [LoginFilter] //RF-03 : Alta de equipo
        public IActionResult Create(EquipoModel equipo)
        {
            if (HttpContext.Session.GetString("rol") != "Administrador")
            {
                return RedirectToAction("Index", "Usuario");
            }

            string token = HttpContext.Session.GetString("token");
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl, VerbosHttp.POST, equipo, token);

            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            IEnumerable<EquipoModel> equipos = CargarIndex();
            if (respuesta.IsSuccessStatusCode)
            {
                ViewBag.Mensaje = "Equipo creado correctamente.";
            }
            else
            {
                ViewBag.Error = body;
            }
            return View("Index", equipos);
        }

        [LoginFilter] //RF-03 : Modificación de equipo
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("rol") != "Administrador")
            {
                return RedirectToAction("Index", "Usuario");
            }
            string token = HttpContext.Session.GetString("token");

            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl + "/" + id, VerbosHttp.GET, null, token);

            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            IEnumerable<EquipoModel> equipos = CargarIndex();
            if (respuesta.IsSuccessStatusCode)
            {
                EquipoModel equipo = JsonConvert.DeserializeObject<EquipoModel>(body);
                return View(equipo);
            }
            else
            {
                ViewBag.Error = body;
                return View("Index", equipos);
            }
        }

        [HttpPost]
        [LoginFilter] //RF-03 : Modificación de equipo
        public IActionResult Update(int id, EquipoModel equipo)
        {
            if (HttpContext.Session.GetString("rol") != "Administrador")
            {
                return RedirectToAction("Index", "Usuario");
            }
            string token = HttpContext.Session.GetString("token");
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl + "/" + id, VerbosHttp.PUT, equipo, token);
            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            IEnumerable<EquipoModel> equipos = CargarIndex();
            if (respuesta.IsSuccessStatusCode)
            {
                ViewBag.Mensaje = body;
                return View("Index", equipos);
            }
            else
            {
                ViewBag.Error = body;
                return View(equipo);
            }
        }

        public IActionResult Delete(int id) //RF-03 : Baja de equipo
        {
            if (HttpContext.Session.GetString("rol") != "Administrador")
            {
                return RedirectToAction("Index", "Usuario");
            }
            EquipoModel equipo = new EquipoModel();
            equipo.Id = id;
            return View(equipo);
        }
        [HttpPost]
        public IActionResult Delete(EquipoModel equipo) //RF-03 : Baja de equipo
        {
            if (HttpContext.Session.GetString("rol") != "Administrador")
            {
                return RedirectToAction("Index", "Usuario");
            }
            string token = HttpContext.Session.GetString("token");
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl + "/" + equipo.Id, VerbosHttp.DELETE, null, token);
            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            IEnumerable<EquipoModel> equipos = CargarIndex();
            if (respuesta.IsSuccessStatusCode)
            {
                ViewBag.Mensaje = body;
            }
            else
            {
                ViewBag.Error = body;
            }
                return View("Index", equipos);

        }

        //Método para cargar de datos las vistas
        private IEnumerable<EquipoModel> CargarIndex()
        {
            string token = HttpContext.Session.GetString("token");
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl, VerbosHttp.GET, null, token);
            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);

            if (respuesta.IsSuccessStatusCode)
            {
                IEnumerable<EquipoModel> equipos = JsonConvert.
                    DeserializeObject<IEnumerable<EquipoModel>>
                    (body);
                return equipos;
            }
            else
            {
                ViewBag.Error = body;
                return new List<EquipoModel>();
            }
        }
    }
}