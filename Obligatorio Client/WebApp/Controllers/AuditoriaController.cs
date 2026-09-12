using ExpensesApp.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Buffers.Text;
using WebApp.Auxiliar;
using WebApp.Enums;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AuditoriaController : Controller
    {
        private string baseUrl = "http://ObligatorioAPI.somee.com/api/Auditoria";

        [LoginFilter] //RF-11 : Ver auditoría de un préstamo
        public IActionResult VerAuditoria(int id)
        {
            string token = HttpContext.Session.GetString("token");
            if (HttpContext.Session.GetString("rol") != "Administrador") return RedirectToAction("Index", "Usuario");
            HttpResponseMessage respuesta =
            ClienteHttpAuxiliar.EnviarSolicitud(
            baseUrl + "/auditoriaCompleta?prestamoId=" + id, VerbosHttp.GET, null, token);

            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            if (respuesta.IsSuccessStatusCode)
            {
                IEnumerable<AuditoriaModel> auditorias = JsonConvert.
                    DeserializeObject<IEnumerable<AuditoriaModel>>(body);
                return View (auditorias);
            }
            else
            {
                ViewBag.Error = body;

            }
            return View(new List<AuditoriaModel>());
        }

    }
}