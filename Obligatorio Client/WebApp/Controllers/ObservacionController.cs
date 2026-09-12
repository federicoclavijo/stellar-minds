using ExpensesApp.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Buffers.Text;
using WebApp.Auxiliar;
using WebApp.Enums;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ObservacionController : Controller
    {
        private string baseUrl = "http://ObligatorioAPI.somee.com/api/Observacion";

        [LoginFilter] //RF-07 : Alta de observación
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("rol") != "Socio")
            {
                return RedirectToAction("Index", "Usuario");
            }

            ObservacionModel modelo = CargarCombos();
            return View(modelo);
        }

        [HttpPost]
        [LoginFilter] //RF-07 : Evaluación previo al Alta de observación
        public IActionResult Evaluar(ObservacionModel modelo)
        {
            if (HttpContext.Session.GetString("rol") != "Socio")
            {
                return RedirectToAction("Index", "Usuario");
            }

            modelo.Evaluado = false;
            modelo.Prestamos = CargarCombos().Prestamos;
            modelo.ObjetosCelestes = CargarCombos().ObjetosCelestes;

            string token = HttpContext.Session.GetString("token");
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                "http://ObligatorioAPI.somee.com/api/Consulta/", VerbosHttp.POST, modelo, token);

            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            if (respuesta.IsSuccessStatusCode)
            {
                EvaluacionModel evaluacion = JsonConvert.
                    DeserializeObject<EvaluacionModel>(body);
                modelo.Indicador = evaluacion.Indicador;
                modelo.Detalle = evaluacion.Detalle;
                modelo.Evaluado = true;
            }
            else
            {
                ViewBag.Error = body;
            }
            return View("Create", modelo);

        }

        [HttpPost]
        [LoginFilter] //RF-07 : Alta de observación después de ser evaluado
        public IActionResult Create(ObservacionModel modelo)
        {
            if (HttpContext.Session.GetString("rol") != "Socio")
            {
                return RedirectToAction("Index", "Usuario");
            }

            modelo.Prestamos = CargarCombos().Prestamos;
            modelo.ObjetosCelestes = CargarCombos().ObjetosCelestes;
            if (modelo.Evaluado)
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                    baseUrl, VerbosHttp.POST, modelo, token);
                if (respuesta.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Observación creada con éxito.";
                    modelo.Evaluado = false;
                }
                else
                {
                    string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
                    ViewBag.Error = body;
                }
            }
            else
            {
                ViewBag.Error = "La observación no fue evaluada";
            }
            
            return View(modelo);
        }

        [LoginFilter] //RF-10 : Ranking de objetos celestes
        public IActionResult Ranking()
        {
            string token = HttpContext.Session.GetString("token");
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl + "/ranking", VerbosHttp.GET, null, token);
            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            if (respuesta.IsSuccessStatusCode)
            {
                IEnumerable<RankingObjetosCelestesModel> ranking = JsonConvert.
                    DeserializeObject<IEnumerable<RankingObjetosCelestesModel>>
                    (body);
                return View(ranking);
            }
            else
            {
                ViewBag.Error = body;
            }
            return View(new List<RankingObjetosCelestesModel>());
        }

        //Método para cargar de datos las vistas
        private ObservacionModel CargarCombos()
        {
            ObservacionModel modelo = new ObservacionModel();
            string token = HttpContext.Session.GetString("token");
            int? socioId = HttpContext.Session.GetInt32("usuarioId");

            HttpResponseMessage respuestaPrestamos = ClienteHttpAuxiliar.EnviarSolicitud(
                "http://ObligatorioAPI.somee.com/api/Prestamo/prestamosVigentes/" + socioId, VerbosHttp.GET, null, token);
            if (respuestaPrestamos.IsSuccessStatusCode)
            {
                string objetoComoTexto = ClienteHttpAuxiliar.ObtenerBody(respuestaPrestamos);
                IEnumerable<PrestamoModel> prestamosVigentes = JsonConvert.
                    DeserializeObject<IEnumerable<PrestamoModel>>
                    (objetoComoTexto);
                modelo.Prestamos = prestamosVigentes.ToList();
            }

            HttpResponseMessage respuestaObjetos = ClienteHttpAuxiliar.EnviarSolicitud(
               "http://ObligatorioAPI.somee.com/api/ObjetoCeleste", VerbosHttp.GET, null, token);
            if (respuestaObjetos.IsSuccessStatusCode)
            {
                string objetoComoTexto = ClienteHttpAuxiliar.ObtenerBody(respuestaObjetos);
                IEnumerable<ObjetoCelesteModel> objetosCelestes = JsonConvert.
                    DeserializeObject<IEnumerable<ObjetoCelesteModel>>
                    (objetoComoTexto);
                modelo.ObjetosCelestes = objetosCelestes.ToList();
            }
            return modelo;
        }
    }
}
