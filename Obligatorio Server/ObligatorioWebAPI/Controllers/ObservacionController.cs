using DTOs.DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.ObjetoCeleste;
using LogicaAplicacion.InterfacesCasosDeUso.Observacion;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObligatorioWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservacionController : ControllerBase
    {
        private IAltaObservacion _altaCU;
        private IListadoRankingObjetos _rankingCU;

        public ObservacionController (IAltaObservacion altaCU, IListadoRankingObjetos rankingCU)
        {
            _altaCU = altaCU;
            _rankingCU = rankingCU;
        }

        [HttpPost]
        [Authorize(Roles = "Socio")]
        public ActionResult<ObservacionDTO> Post([FromBody] ObservacionDTO dto)
        {
            try
            {
                _altaCU.Ejecutar(dto);
                return Created("api/Observacion", dto);
            }
            catch (ObservacionException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (PrestamoException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ObjetoCelesteException ex)
            {
                return NotFound(ex.Message);
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

        [HttpGet("ranking")]
        [Authorize(Roles = "Administrador, Coordinador, Socio")]
        public ActionResult<RankingObjetosCelestesDTO> Ranking()
        {
            try
            {
                IEnumerable<RankingObjetosCelestesDTO> lista = _rankingCU.Ejecutar();
                return Ok(lista);
            }
            catch (System.Data.Common.DbException)
            {
                return StatusCode(500, "Internal Server Error");
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
