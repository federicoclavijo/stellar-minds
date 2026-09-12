using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesCasosDeUso.Usuario
{
    public interface IObtenerSocios
    {
        IEnumerable<UsuarioDTO> Ejecutar();
    }
}