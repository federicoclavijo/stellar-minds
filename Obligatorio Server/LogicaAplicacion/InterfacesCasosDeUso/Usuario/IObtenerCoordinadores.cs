using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesCasosDeUso.Usuario
{
    public interface IObtenerCoordinadores
    {
      IEnumerable<UsuarioDTO> Ejecutar();
    }
}