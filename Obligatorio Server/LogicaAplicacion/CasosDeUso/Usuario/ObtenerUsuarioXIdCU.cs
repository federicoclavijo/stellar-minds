using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Usuario
{
    public class ObtenerUsuarioXIdCU : IObtenerUsuarioXId
    {
        private readonly IRepositorioUsuario _repositorio;

        public ObtenerUsuarioXIdCU(IRepositorioUsuario repositorio)
        {
            _repositorio = repositorio;
        }

        public UsuarioDTO Ejecutar(int id)
        {
            LogicaNegocio.Entidades.Usuario? entidad = _repositorio.FindById(id);
            if (entidad == null) throw new UsuarioException("No se encontró el usuario.");
            return UsuarioMapper.ToDTO(entidad);
        }
    }
}
