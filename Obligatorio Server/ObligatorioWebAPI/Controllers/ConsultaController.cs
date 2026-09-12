using DTOs.DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Observacion;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObligatorioWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private IEvaluarObservacion _consultaCU;

        public ConsultaController(IEvaluarObservacion consultaCU)
        {
            _consultaCU = consultaCU;
        }

        [HttpPost]
        [Authorize (Roles = "Socio")]
        public ActionResult<ConsultaObservacionDTO> Post([FromBody] ConsultaObservacionDTO consulta)
        {
            try
            {
                EvaluacionDTO respuesta = _consultaCU.Ejecutar(consulta);
                return Ok(respuesta);
            }
            catch (ConsultaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (PrestamoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ObjetoCelesteException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (MuchasPeticionesException ex)
            {
                return StatusCode(429, ex.Message);
            }
            catch (ServicioNoDisponibleException ex)
            {
                return StatusCode(503, ex.Message);
            }
            catch (System.Exception ex)
            {
                string message = ex.Message ?? string.Empty;
                if (message.IndexOf("cannot open database", System.StringComparison.OrdinalIgnoreCase) >= 0 ||
                    message.IndexOf("database .* does not exist", System.StringComparison.OrdinalIgnoreCase) >= 0 ||
                    message.IndexOf("login failed", System.StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return StatusCode(500, message);
                }

                return BadRequest(message); ;
            }
        }
    }
}
