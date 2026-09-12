using DTOs.DTOs;
using LogicaAplicacion.CasosDeUso.Equipo;
using LogicaAplicacion.InterfacesCasosDeUso.Equipo;
using LogicaAplicacion.InterfacesCasosDeUso.Prestamo;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObligatorioWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private IAltaPrestamo _altaCU;
        private IObtenerPrestamos _findAllCU;
        private IObtenerPrestamoXId _findByIdCU;
        private IPrestamosActivosDeSocio _findPrestamosActivosSocioCU;
        private IPrestamosVigentesDeSocio _findPrestamosVigentesSocioCU;
        private IObtenerPrestamosEntreFechas _findPrestamosEntreFechasCU;
        private IObtenerSociosXTelescopio _findSociosXTelescopioCU;
        private IObtenerPrestamosXCoordinador _findPrestamosXCoordinadorCU;
        private IDevolucionPrestamo _updateCU;

        public PrestamoController(IAltaPrestamo altaCU, IObtenerPrestamos findAllCU, IObtenerPrestamoXId findByIdCU, IPrestamosActivosDeSocio findPrestamosActivosSocioCU, IPrestamosVigentesDeSocio findPrestamosVigentesSocioCU, IObtenerPrestamosEntreFechas findPrestamosEntreFechasCU, IObtenerSociosXTelescopio findSociosXTelescopioCU, IDevolucionPrestamo updateCU, IObtenerPrestamosXCoordinador findPrestamosXCoordinadorCU)
        {
            this._altaCU = altaCU;
            this._findAllCU = findAllCU;
            this._findByIdCU = findByIdCU;
            this._findPrestamosActivosSocioCU = findPrestamosActivosSocioCU;
            this._findPrestamosVigentesSocioCU = findPrestamosVigentesSocioCU;
            this._findPrestamosEntreFechasCU = findPrestamosEntreFechasCU;
            this._findSociosXTelescopioCU = findSociosXTelescopioCU;
            this._findSociosXTelescopioCU = findSociosXTelescopioCU;
            this._updateCU = updateCU;
            this._findPrestamosXCoordinadorCU = findPrestamosXCoordinadorCU;
        }

        [HttpGet]
        [Authorize(Roles = "Coordinador")]
        public ActionResult<PrestamoDTO> Get()
        {
            try
            {
                IEnumerable<PrestamoDTO> lista = _findAllCU.Ejecutar();
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

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<PrestamoDTO> Get(int id)
        {
            try
            {
                PrestamoDTO prestamo = _findByIdCU.Ejecutar(id);
                if (prestamo == null) return BadRequest("No existe prestamo con ese Id");
                return Ok(prestamo);
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

        [HttpPost("{id}")]
        [Authorize(Roles = "Coordinador")]
        public ActionResult<PrestamoDTO> Post([FromBody] PrestamoDTO dto, int id)
        {
            try
            {
                _altaCU.Ejecutar(dto, id);
                return Created("api/Prestamo", dto);
            }
            catch (PrestamoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UsuarioException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AuditoriaException ex)
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

        [HttpPut]
        [Authorize(Roles = "Coordinador")]
        public ActionResult<PrestamoDTO> Put([FromQuery] int coordinadorId, [FromQuery] int id)
        {
            try
            {
                _updateCU.Ejecutar(id, coordinadorId);
                return Ok("Préstamo devuelto correctamente.");
            }
            catch (EquipoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (PrestamoException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AuditoriaException ex)
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


        [HttpGet("filtrado")]
        [Authorize(Roles = "Socio")]
        public ActionResult<PrestamoDTO> GetEnFecha([FromQuery] int mes, [FromQuery] int anio, [FromQuery] int id)
        {
            try
            {
                IEnumerable<PrestamoDTO> lista = _findPrestamosEntreFechasCU.Ejecutar(mes, anio, id);
                return Ok(lista);
            }
            catch (System.Data.Common.DbException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (UsuarioException ex)
            {
                return NotFound(ex.Message); ;
            }
            catch (PrestamoException ex)
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

        [HttpGet("sociosXTelescopio")]
        [Authorize(Roles = "Administrador, Coordinador")]
        public ActionResult<UsuarioDTO> GetSociosXTelescopio([FromQuery] int telescopioId)
        {
            try
            {
                IEnumerable<UsuarioDTO> lista = _findSociosXTelescopioCU.Ejecutar(telescopioId);
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

        [HttpGet("prestamosActivos/{id}")]
        [Authorize(Roles = "Coordinador")]
        public ActionResult<PrestamoDTO> GetActivos(int id)
        {
            try
            {
                IEnumerable<PrestamoDTO> lista = _findPrestamosActivosSocioCU.Ejecutar(id);
                return Ok(lista);
            }
            catch (System.Data.Common.DbException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (PrestamoException uex)
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
                    IEnumerable<PrestamoDTO> empty = new List<PrestamoDTO>();
                    return StatusCode(500, message);
                }
                return BadRequest(message); ;
            }
        }

        [HttpGet("prestamosVigentes/{id}")]
        public ActionResult<PrestamoDTO> GetPrestamosVigentesXSocio(int id)
        {
            try
            {
                IEnumerable<PrestamoDTO> lista = _findPrestamosVigentesSocioCU.Ejecutar(id);
                return Ok(lista);
            }
            catch (System.Data.Common.DbException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (PrestamoException uex)
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

        [HttpGet("prestamosXCoordinador")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<PrestamoDTO> GetPrestamosXCoordinador([FromQuery] int coordinadorId)
        {
            try
            {
                IEnumerable<PrestamoDTO> lista = _findPrestamosXCoordinadorCU.Ejecutar(coordinadorId);
                return Ok(lista);
            }
            catch (System.Data.Common.DbException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (UsuarioException ex)
            {
                return BadRequest(ex.Message); ;
            }
            catch (PrestamoException ex)
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
