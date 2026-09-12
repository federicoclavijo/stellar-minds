using ExpensesApp.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using WebApp.Auxiliar;
using WebApp.Enums;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PrestamoController : Controller
    {
        private string baseUrl = "http://ObligatorioAPI.somee.com/api/Prestamo";

        [LoginFilter]//RF-04 : Listado de préstamos con enlace a Alta
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("rol") != "Coordinador")
            {
                return View("Index", "Usuario");
            }

            return View(CargarIndex());
        }

        [LoginFilter] //RF-11 : Detalle de préstamo a partir de auditoría
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("rol") != "Administrador")
            {
                return View("Index", "Usuario");
            }

            string token = HttpContext.Session.GetString("token");
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl + "/" + id, VerbosHttp.GET, null, token);
            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            if (respuesta.IsSuccessStatusCode)
            {
                PrestamoModel prestamo = JsonConvert.
                    DeserializeObject<PrestamoModel>
                    (body);
                return View(prestamo);
            }
            else
            {
                ViewBag.Error = body;
                return View(new PrestamoModel());
            }
        }

        [LoginFilter] //RF-04 : Alta de préstamo
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("rol") != "Coordinador")
            {
                return View("Index", "Usuario");
            }
            CargarCombos();
            PrestamoModel modelo = new PrestamoModel();
            return View(modelo);
        }

        [HttpPost]
        [LoginFilter] //RF-04 : Alta de préstamo
        public IActionResult Create(PrestamoModel modelo)
        {
            if (HttpContext.Session.GetString("rol") != "Coordinador")
            {
                return View("Index", "Usuario");
            }

            int? coordinadorId = HttpContext.Session.GetInt32("usuarioId");
            string token = HttpContext.Session.GetString("token");
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl + "/" + coordinadorId, VerbosHttp.POST, modelo, token);
            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            if (respuesta.IsSuccessStatusCode)
            {
                ViewBag.Mensaje = "Préstamo creado correctamente";
                return View("Index", CargarIndex());
            }
            else
            {
                ViewBag.Error = body;
                CargarCombos();
                return View(modelo);
            }
        }

        [LoginFilter] //RF-05 : Devolucion de préstamo
        public IActionResult Update(int id) 
        {
            if (HttpContext.Session.GetString("rol") != "Coordinador")
            {
                return View("Index", "Usuario");
            }

            PrestamoModel prestamo = new PrestamoModel();
            prestamo.Id = id;
            return View(prestamo);
        }

        [HttpPost]
        [LoginFilter] //RF-05 : Devolucion de préstamo
        public IActionResult Update(PrestamoModel modelo)
        {
            if (HttpContext.Session.GetString("rol") != "Coordinador")
            {
                return View("Index", "Usuario");
            }

            int? coordinadorId = HttpContext.Session.GetInt32("usuarioId");

            string token = HttpContext.Session.GetString("token");
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl + "?coordinadorId=" + coordinadorId + "&id=" + modelo.Id, VerbosHttp.PUT, null, token);
            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            if (respuesta.IsSuccessStatusCode)
            {
                ViewBag.Mensaje = body;
                return View("Index", CargarIndex());
            }
            else
            {
                ViewBag.Error = body;
                return View(modelo);
            }
        }



        [LoginFilter] //RF-08 : Listado de préstamos en una fecha
        public IActionResult ListadoEnFecha()
        {
            if (HttpContext.Session.GetString("rol") != "Socio")
            {
                return View("Index", "Usuario");
            }

            var modelo = new PrestamoFiltroViewModel();
            return View(modelo);
        }

        [HttpPost]
        [LoginFilter] //RF-08 : Listado de préstamos en una fecha
        public IActionResult ListadoEnFecha(PrestamoFiltroViewModel modelo)
        {
            if (HttpContext.Session.GetString("rol") != "Socio")
            {
                return View("Index", "Usuario");
            }

            if (!modelo.Fecha.HasValue)
            {
                ViewBag.Error = "Debe especificar una fecha.";
                modelo.Prestamos = new List<PrestamoModel>();
                return View(modelo);
            }


            string token = HttpContext.Session.GetString("token");
            int mes = modelo.Fecha.Value.Month;
            int anio = modelo.Fecha.Value.Year;
            int? usuarioId = HttpContext.Session.GetInt32("usuarioId");

            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl + "/filtrado?mes=" + mes + "&anio=" + anio + "&id=" + usuarioId, VerbosHttp.GET, null, token);
            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            if (respuesta.IsSuccessStatusCode)
            {
                IEnumerable<PrestamoModel> prestamos = JsonConvert.
                    DeserializeObject<IEnumerable<PrestamoModel>>(body);
                modelo.Prestamos = prestamos ?? new List<PrestamoModel>();
            }
            else
            {
                ViewBag.Error = body;
                modelo.Prestamos = new List<PrestamoModel>();
            }
            return View(modelo);
        }

        [LoginFilter] //RF-09 : Listado de socios que se les prestó un telescopio
        public IActionResult ListadoSociosXTelescopio()
        {

            if (HttpContext.Session.GetString("rol") != "Administrador" && HttpContext.Session.GetString("rol") != "Coordinador")
            {
                return View("Index", "Usuario");
            }

            ListadoSociosXTelescopioModel modelo = new ListadoSociosXTelescopioModel();

            string token = HttpContext.Session.GetString("token");
            modelo.Telescopios = ObtenerTelescopios(token);

            if (modelo.Telescopios.Count == 0)
            {
                ViewBag.Error = "No se pudieron cargar los telescopios o no hay ninguno registrado.";
            }

            return View(modelo);
        }

        [HttpPost]
        [LoginFilter] //RF-09 : Listado de socios que se les prestó un telescopio
        public IActionResult ListadoSociosXTelescopio(ListadoSociosXTelescopioModel modelo)
        {

            if (HttpContext.Session.GetString("rol") != "Administrador" && HttpContext.Session.GetString("rol") != "Coordinador")
            {
                return View("Index", "Usuario");
            }

            string token = HttpContext.Session.GetString("token");
            if(!modelo.TelescopioId.HasValue)
            {
                ViewBag.Error = "Debe seleccionar un telescopio.";
                modelo.Telescopios = ObtenerTelescopios(token);
                return View(modelo);
            }


            int? id = modelo.TelescopioId;

            ListadoSociosXTelescopioModel modeloNuevo = new ListadoSociosXTelescopioModel();
            modeloNuevo.TelescopioId = id;
            modeloNuevo.Telescopios = ObtenerTelescopios(token);
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl + "/sociosXTelescopio?telescopioId=" + id, VerbosHttp.GET, null, token);

            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            if (respuesta.IsSuccessStatusCode)
            {
                IEnumerable<UsuarioModel> socios = JsonConvert.
                    DeserializeObject<IEnumerable<UsuarioModel>>(body);
                modeloNuevo.Socios = socios.ToList();
            }
            else
            {
                ViewBag.Error = body;

            }
            return View(modeloNuevo);
        }

        [LoginFilter] //RF-05 : Listado para devolucion
        public IActionResult ListadoPrestamosXSocio()
        {

            if (HttpContext.Session.GetString("rol") != "Coordinador")
            {
                return View("Index", "Usuario");
            }

            string token = HttpContext.Session.GetString("token");
            ListadoPrestamosXSocioModel modelo = new ListadoPrestamosXSocioModel();
            modelo.Socios = ObtenerSocios(token);

            if (modelo.Socios.Count == 0)
            {
                ViewBag.Error = "No se pudieron cargar los socios o no hay ninguno registrado.";
            }
            return View(modelo);
        }

        [HttpPost]
        [LoginFilter] //RF-05 : Listado para devolucion
        public ActionResult ListadoPrestamosXSocio(ListadoPrestamosXSocioModel modelo)
        {
            if (HttpContext.Session.GetString("rol") != "Coordinador")
            {
                return View("Index", "Usuario");
            }

            string token = HttpContext.Session.GetString("token");
            if (!modelo.UsuarioId.HasValue)
            {
                ViewBag.Error = "Debe seleccionar un socio.";
                modelo.Socios = ObtenerSocios(token);
                return View(modelo);
            }

            int? id = modelo.UsuarioId;

            ListadoPrestamosXSocioModel modeloNuevo = new ListadoPrestamosXSocioModel();
            modeloNuevo.UsuarioId = id;
            modeloNuevo.Socios = ObtenerSocios(token);
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl + "/prestamosActivos/" + id, VerbosHttp.GET, null, token);

            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            if (respuesta.IsSuccessStatusCode)
            {
                IEnumerable<PrestamoModel> prestamos = JsonConvert.
                    DeserializeObject<IEnumerable<PrestamoModel>>(body);
                modeloNuevo.Prestamos = prestamos.ToList();
            }
            else
            {
                ViewBag.Error = body;

            }
            return View(modeloNuevo);
        }

        [LoginFilter] //RF-11 : Listado de préstamos que realizó un coordinador
        public IActionResult PrestamosPorCoordinador()
        {
            if (HttpContext.Session.GetString("rol") != "Administrador")
            {
                return View("Index", "Usuario");
            }

            string token = HttpContext.Session.GetString("token");
            PrestamosXCoordinadorModel modelo = new PrestamosXCoordinadorModel();
            modelo.Coordinadores = ObtenerCoordinadores(token);

            if (modelo.Coordinadores.Count == 0)
            {
                ViewBag.Error = "No se pudieron cargar los coordinadores o no hay ninguno registrado.";
            }
            return View(modelo);
        }

        [HttpPost]
        [LoginFilter] //RF-11 : Listado de préstamos que realizó un coordinador
        public IActionResult PrestamosPorCoordinador(PrestamosXCoordinadorModel modelo)
        {
            if (HttpContext.Session.GetString("rol") != "Administrador")
            {
                return View("Index", "Usuario");
            }
            string token = HttpContext.Session.GetString("token");
            if (!modelo.UsuarioId.HasValue)
            {
                ViewBag.Error = "Debe seleccionar un coordinador.";
                modelo.Coordinadores = ObtenerCoordinadores(token);
                return View(modelo);
            }

            int? id = modelo.UsuarioId;

            PrestamosXCoordinadorModel modeloNuevo = new PrestamosXCoordinadorModel();
            modeloNuevo.UsuarioId = id;
            modeloNuevo.Coordinadores = ObtenerCoordinadores(token);
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl + "/prestamosXCoordinador?coordinadorId=" + id, VerbosHttp.GET, null, token);
            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            if (respuesta.IsSuccessStatusCode)
            {
                IEnumerable<PrestamoModel> prestamos = JsonConvert.
                    DeserializeObject<IEnumerable<PrestamoModel>>(body);
                modeloNuevo.Prestamos = prestamos.ToList();
            }
            else
            {
                ViewBag.Error = body;

            }
            return View(modeloNuevo);
        }

        //Método para cargar de datos las vistas
        private List<PrestamoModel> CargarIndex()
        {
            string token = HttpContext.Session.GetString("token");
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                baseUrl, VerbosHttp.GET, null, token);
            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);
            if (respuesta.IsSuccessStatusCode)
            {
                IEnumerable<PrestamoModel> prestamos = JsonConvert.
                    DeserializeObject<IEnumerable<PrestamoModel>>
                    (body);
                return prestamos.ToList();
            }
            else
            {
                ViewBag.Error = body;
            }
            return new List<PrestamoModel>();
        }

        //Método para cargar de datos las vistas
        private List<UsuarioModel>? ObtenerCoordinadores(string token)
        {
            HttpResponseMessage respuestaSocios = ClienteHttpAuxiliar.EnviarSolicitud(
            "http://ObligatorioAPI.somee.com/api/Usuario/coordinadores", VerbosHttp.GET, null, token);
            if (respuestaSocios.IsSuccessStatusCode)
            {
                string txt = ClienteHttpAuxiliar.ObtenerBody(respuestaSocios);
                IEnumerable<UsuarioModel> coordinadores = JsonConvert.DeserializeObject<IEnumerable<UsuarioModel>>(txt);
                return coordinadores.ToList();
            }
            return new List<UsuarioModel>();
        }

        //Método para cargar de datos las vistas
        private List<EquipoModel> ObtenerTelescopios(string token)
        {
            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                "http://ObligatorioAPI.somee.com/api/Equipo/telescopios", VerbosHttp.GET, null, token);

            if (respuesta.IsSuccessStatusCode)
            {
                string txt = ClienteHttpAuxiliar.ObtenerBody(respuesta);
                var todos = JsonConvert.DeserializeObject<IEnumerable<EquipoModel>>(txt);
                return todos.ToList();
            }
            return new List<EquipoModel>();
        }

        //Método para cargar de datos las vistas
        private List<UsuarioModel> ObtenerSocios(string token)
        {
            HttpResponseMessage respuestaSocios = ClienteHttpAuxiliar.EnviarSolicitud(
            "http://ObligatorioAPI.somee.com/api/Usuario/socios", VerbosHttp.GET, null, token);
            if (respuestaSocios.IsSuccessStatusCode)
            {
                string txt = ClienteHttpAuxiliar.ObtenerBody(respuestaSocios);
                IEnumerable<UsuarioModel> socios = JsonConvert.DeserializeObject<IEnumerable<UsuarioModel>>(txt);
                return socios.ToList();
            }
            return new List<UsuarioModel>();
        }

        //Método para cargar de datos las vistas
        private void CargarCombos()
        {
            string token = HttpContext.Session.GetString("token");
            HttpResponseMessage respuestaUsuarios = ClienteHttpAuxiliar.EnviarSolicitud(
                "http://ObligatorioAPI.somee.com/api/Usuario", VerbosHttp.GET, null, token);
            if (respuestaUsuarios.IsSuccessStatusCode)
            {
                string objetoComoTexto = ClienteHttpAuxiliar.ObtenerBody(respuestaUsuarios);
                IEnumerable<UsuarioModel> usuarios = JsonConvert.
                    DeserializeObject<IEnumerable<UsuarioModel>>
                    (objetoComoTexto);
                ViewBag.Usuarios = usuarios;
            }

            HttpResponseMessage respuestaEquipos = ClienteHttpAuxiliar.EnviarSolicitud(
               "http://ObligatorioAPI.somee.com/api/Equipo", VerbosHttp.GET, null, token);
            if (respuestaEquipos.IsSuccessStatusCode)
            {
                string objetoComoTexto = ClienteHttpAuxiliar.ObtenerBody(respuestaEquipos);
                IEnumerable<EquipoModel> equipos = JsonConvert.
                    DeserializeObject<IEnumerable<EquipoModel>>
                    (objetoComoTexto);
                ViewBag.Equipos = equipos;
            }
        }
    }
}
