using DTOs.DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.ObjetoCeleste;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObligatorioWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetoCelesteController : ControllerBase
    {
        private IObtenerObjetosCelestes _findAllCU;

        public ObjetoCelesteController(IObtenerObjetosCelestes findAllCU)
        {
            _findAllCU = findAllCU;
        }

        [HttpGet]
        [Authorize(Roles = "Socio")]
        public ActionResult<ObjetoCelesteDTO> Get()
        {
            try
            {
                IEnumerable<ObjetoCelesteDTO> lista = _findAllCU.Ejecutar();
                return Ok(lista);
            }
            catch (System.Data.Common.DbException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (UsuarioException uex)
            {
                return BadRequest(uex.Message); ;
            }
            catch (System.Exception ex)
            {
                string message = ex.Message ?? string.Empty;
                if (message.IndexOf("cannot open database", System.StringComparison.OrdinalIgnoreCase) >= 0 ||
                    message.IndexOf("database .* does not exist", System.StringComparison.OrdinalIgnoreCase) >= 0 ||
                    message.IndexOf("login failed", System.StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    IEnumerable<UsuarioDTO> empty = new List<UsuarioDTO>();
                    return StatusCode(500, message);
                }

                return BadRequest(message); ;
            }
        }
    }
}
