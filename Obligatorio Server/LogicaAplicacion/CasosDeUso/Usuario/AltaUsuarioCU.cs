using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;

namespace LogicaAplicacion.CasosDeUso.Usuario
{
    public class AltaUsuarioCU : IAltaUsuario
    {
        private IRepositorioUsuario _repositorio;

        public AltaUsuarioCU(IRepositorioUsuario repo)
        {
            _repositorio = repo;
        }

        public void Ejecutar(UsuarioDTO dto)
        {
            LogicaNegocio.Entidades.Usuario usuario = UsuarioMapper.FromDto(dto);
            usuario.Validar();
            _repositorio.Add(usuario);

        }
        
    }
}