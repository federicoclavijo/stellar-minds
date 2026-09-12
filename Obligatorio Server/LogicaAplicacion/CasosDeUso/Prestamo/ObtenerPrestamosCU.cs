using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Prestamo;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Prestamo
{
    public class ObtenerPrestamosCU : IObtenerPrestamos
    {
        private IRepositorioPrestamo _repositorio;
        public ObtenerPrestamosCU(IRepositorioPrestamo repo)
        {
            _repositorio = repo;
        }
        public IEnumerable<PrestamoDTO> Ejecutar()
        {
            IEnumerable<LogicaNegocio.Entidades.Prestamo> prestamos = _repositorio.FindAll();

            return prestamos.Select(p => PrestamoMapper.ToDto(p));
        }
    }
}
