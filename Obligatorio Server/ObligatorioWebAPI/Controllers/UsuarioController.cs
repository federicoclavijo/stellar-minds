using DTOs.DTOs;
using LogicaAplicacion.CasosDeUso.Usuario;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ObligatorioWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IAltaUsuario _altaCU;
        private IObtenerUsuarios _findAllCU;
        private IObtenerSocios _findSociosCU;
        private IObtenerCoordinadores _findCoordinadoresCU;
        private IObtenerUsuarioXId _findByIdCU;
        private ILogin _loginCU;

        public UsuarioController(IAltaUsuario altaCU, IObtenerUsuarios findAllCu, ILogin loginCU, IObtenerUsuarioXId findByIdCU, IObtenerSocios findSociosCU, IObtenerCoordinadores findCoordinadoresCU)
        {
            this._altaCU = altaCU;
            this._findAllCU = findAllCu;
            this._loginCU = loginCU;
            this._findByIdCU = findByIdCU;
            this._findSociosCU = findSociosCU;
            this._findCoordinadoresCU = findCoordinadoresCU;
        }

        [HttpGet]
        //Desactivado para testing
        //[Authorize(Roles = "Administrador, Coordinador")]
        public ActionResult<UsuarioDTO> Get()
        {
            try
            {
                IEnumerable<UsuarioDTO> lista = _findAllCU.Ejecutar();
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
        public ActionResult<UsuarioDTO> Get(int id)
        {
            try
            {
                UsuarioDTO usuario = _findByIdCU.Ejecutar(id);
                if (usuario == null) return BadRequest("No existe usuario con ese Id");
                return Ok(usuario);
            }
            catch (UsuarioException ex)
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
        public ActionResult<UsuarioDTO> Post([FromBody] UsuarioDTO dto)
        {
            try
            {
                _altaCU.Ejecutar(dto);
                return Created("api/Usuario", dto);
            }
            catch (UsuarioException ex)
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

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<UsuarioDTO> Login([FromBody] UsuarioDTO usuario)
        {
            try
                {
                UsuarioDTO usr = _loginCU.Login(usuario.User, usuario.Password);
                var token = JWTHandler.JWTHandler.GenerarToken(usr);
                return Ok(new
                {
                    Token = token,
                    Usuario = usr,
                    Nombre = usr.Nombre,
                    Rol = usr.Rol,
                    Id = usr.Id
                });
            }
            catch (UsuarioException ex)
            {
                return Unauthorized(ex.Message);
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

        [HttpGet("socios")]
        [Authorize(Roles = "Administrador, Coordinador")]
        public ActionResult<UsuarioDTO> GetSocios()
        {
            try
            {
                IEnumerable<UsuarioDTO> lista = _findSociosCU.Ejecutar();
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
        [HttpGet("coordinadores")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<UsuarioDTO> GetCoordinadores()
        {
            try
            {
                IEnumerable<UsuarioDTO> lista = _findCoordinadoresCU.Ejecutar();
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
