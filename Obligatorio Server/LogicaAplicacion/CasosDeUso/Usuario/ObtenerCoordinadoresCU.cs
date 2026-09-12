using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Usuario
{
    public class ObtenerCoordinadoresCU : IObtenerCoordinadores
    {
        private IRepositorioUsuario _repo;
        public ObtenerCoordinadoresCU(IRepositorioUsuario repo)
        {
            _repo = repo;
        }
        public IEnumerable<UsuarioDTO> Ejecutar()
        {
            return _repo.ObtenerCoordinadores()
                        .Select(u => UsuarioMapper.ToDTO(u));
        }
    }
}
