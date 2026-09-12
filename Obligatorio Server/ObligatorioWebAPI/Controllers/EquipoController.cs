using DTOs.DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Equipo;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObligatorioWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private IAltaEquipo _altaCU;
        private IDeleteEquipo _deleteCU;
        private IObtenerEquipoXId _findByIdCU;
        private IObtenerEquipos _findAllCU;
        private IObtenerTelescopios _findTelescopiosCU;
        private IUpdate _updateCU;

        public EquipoController(IAltaEquipo altaCU, IDeleteEquipo deleteCU, IObtenerEquipoXId findByIdCU, IObtenerEquipos findAllCU, IObtenerTelescopios findTelescopiosCU, IUpdate updateCU)
        {
            this._altaCU = altaCU;
            this._deleteCU = deleteCU;
            this._findByIdCU = findByIdCU;
            this._findAllCU = findAllCU;
            this._findTelescopiosCU = findTelescopiosCU;
            this._updateCU = updateCU;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Coordinador")]
        public ActionResult<IEnumerable<EquipoPlanoDTO>> Get()
        {
            try
            {
                IEnumerable<EquipoPlanoDTO> lista = _findAllCU.Ejecutar();
                return Ok(lista);
            }
            catch (System.Data.Common.DbException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (EquipoException ex)
            {
                return BadRequest(ex.Message); ;
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

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<EquipoDTO> Get(int id)
        {
            try
            {
                EquipoDTO equipo = _findByIdCU.Ejecutar(id);
                return Ok(equipo);
            }
            catch (EquipoException ex)
            {
                return BadRequest(ex.Message);
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

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult<EquipoPlanoDTO> Post([FromBody] EquipoPlanoDTO dto)
        {
            try
            {
                _altaCU.Add(dto);
                return Created();
            }
            catch (EquipoException ex)
            {
                return BadRequest(ex.Message);
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

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<EquipoPlanoDTO> Put(int id, [FromBody] EquipoPlanoDTO equipo)
        {
            try
            {
                if (id != equipo.Id)
                {
                    return BadRequest("El identificador del equipo no coincide.");
                }

                _updateCU.Ejecutar(equipo);
                return Ok("Equipo modificado correctamente");
            }
            catch (EquipoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception ex)
            {
                string message = ex.Message ?? string.Empty;
                if (message.IndexOf("cannot open database", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    message.IndexOf("database.* does not exist", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    message.IndexOf("login failed", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return StatusCode(500, message);
                }

                return BadRequest(message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<EquipoDTO> Delete(int id)
        {
            try
            {
                _deleteCU.Ejecutar(id);
                return Ok("Equipo eliminado correctamente");
            }
            catch (EquipoException ex)
            {
                return BadRequest(ex.Message);
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

        [HttpGet("disponibilidad/{id}")]
        public ActionResult<EquipoDTO> ObtenerDisponibilidad(int id)
        {
            try
            {
                EquipoDTO equipo = _findByIdCU.Ejecutar(id);

                return Ok(new
                {
                    disponible = equipo.Stock > 0,
                    stock = equipo.Stock
                });
            }
            catch (EquipoException ex)
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

        [HttpGet("telescopios")]
        [Authorize(Roles = "Coordinador, Administrador")]
        public ActionResult<IEnumerable<EquipoPlanoDTO>> GetTelescopios()
        {
            try
            {
                IEnumerable<EquipoPlanoDTO> lista = _findTelescopiosCU.Ejecutar();
                return Ok(lista);
            }
            catch (System.Data.Common.DbException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (EquipoException ex)
            {
                return BadRequest(ex.Message); ;
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
