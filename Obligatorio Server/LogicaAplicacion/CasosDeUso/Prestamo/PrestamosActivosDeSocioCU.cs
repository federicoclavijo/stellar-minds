using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Prestamo;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Prestamo
{
    public class PrestamosActivosDeSocioCU : IPrestamosActivosDeSocio
    {
        private IRepositorioPrestamo _repositorio;
        public PrestamosActivosDeSocioCU(IRepositorioPrestamo repo)
        {
            _repositorio = repo;
        }
        public IEnumerable<PrestamoDTO> Ejecutar(int id)
        {
            IEnumerable<LogicaNegocio.Entidades.Prestamo> prestamos = _repositorio.ObtenerPrestamosActivosXSocio(id);

            return prestamos.Select(p => PrestamoMapper.ToDto(p));
        }
    }
}
