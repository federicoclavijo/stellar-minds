using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Usuario
{
    public class ObtenerSociosCU : IObtenerSocios
    {
        private IRepositorioUsuario _repo;
        public ObtenerSociosCU(IRepositorioUsuario repo)
        {
            _repo = repo;
        }
        public IEnumerable<UsuarioDTO> Ejecutar()
        {
            return _repo.ObtenerSocios()
                        .Select(u => UsuarioMapper.ToDTO(u));
        }
    }
}
