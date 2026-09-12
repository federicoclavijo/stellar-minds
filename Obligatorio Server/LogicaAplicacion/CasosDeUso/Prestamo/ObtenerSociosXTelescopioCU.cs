using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Prestamo;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Prestamo
{
    public class ObtenerSociosXTelescopioCU : IObtenerSociosXTelescopio
    {
        private IRepositorioPrestamo _repositorio;
        public ObtenerSociosXTelescopioCU(IRepositorioPrestamo repo)
        {
            _repositorio = repo;
        }
        public IEnumerable<UsuarioDTO> Ejecutar(int telescopioId)
        {
            IEnumerable<LogicaNegocio.Entidades.Usuario> usuarios = _repositorio.ObtenerSociosXTelescopio(telescopioId);

            return usuarios.Select(u => UsuarioMapper.ToDTO(u));
        }
    }
}
