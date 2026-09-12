using DTOs.DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Auditoria;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObligatorioWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriaController : ControllerBase
    {
        private IObtenerAuditoriasXPrestamo _findAuditoriasXPrestamoCU;

        public AuditoriaController(IObtenerAuditoriasXPrestamo findAuditoriasXPrestamo)
        {
            _findAuditoriasXPrestamoCU = findAuditoriasXPrestamo;
        }
        

        [HttpGet("auditoriaCompleta")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<AuditoriaDTO> AuditoriaCompleta([FromQuery] int prestamoId)
        {
            try
            {
                IEnumerable<AuditoriaDTO> lista = _findAuditoriasXPrestamoCU.Ejecutar(prestamoId);
                return Ok(lista);
            }
            catch (System.Data.Common.DbException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (PrestamoException ex)
            {
                return NotFound(ex.Message); ;
            }
            catch (Exception ex)
            {
                string message = ex.Message ?? string.Empty;
                
                return BadRequest(message); ;
            }
        }
    }
}
