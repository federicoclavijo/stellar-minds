using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    // Landing pública del sistema. No requiere sesión iniciada:
    // es la primera pantalla que ve cualquier visitante.
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
