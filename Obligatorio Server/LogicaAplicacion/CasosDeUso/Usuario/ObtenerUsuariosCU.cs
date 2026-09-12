using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Usuario
{
    public class ObtenerUsuariosCU : IObtenerUsuarios
    {
        private IRepositorioUsuario _repositorio;
        public ObtenerUsuariosCU(IRepositorioUsuario repo)
        {
            _repositorio = repo;
        }

        public List<UsuarioDTO> Ejecutar()
        {
            IEnumerable<LogicaNegocio.Entidades.Usuario> usuarios = _repositorio.FindAll();

            return usuarios.Select(u => UsuarioMapper.ToDTO(u)).ToList();
        }
    }
}
