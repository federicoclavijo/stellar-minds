using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Prestamo;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Prestamo
{
    public class ObtenerPrestamoXIdCU : IObtenerPrestamoXId
    {
        private IRepositorioPrestamo _repositorio;
        public ObtenerPrestamoXIdCU(IRepositorioPrestamo repo)
        {
            _repositorio = repo;
        }
        public PrestamoDTO Ejecutar(int id)
        {
            LogicaNegocio.Entidades.Prestamo? prestamo = _repositorio.FindById(id);
            if (prestamo == null) throw new PrestamoException("No se encontró el préstamo.");

            return PrestamoMapper.ToDto(prestamo);
        }
    }
}
