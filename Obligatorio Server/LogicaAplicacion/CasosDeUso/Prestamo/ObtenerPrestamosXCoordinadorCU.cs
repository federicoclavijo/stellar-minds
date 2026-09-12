using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Prestamo;
using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Prestamo
{
    public class ObtenerPrestamosXCoordinadorCU : IObtenerPrestamosXCoordinador
    {
        private IRepositorioPrestamo _repoP;
        private IRepositorioUsuario _repoU;

        public ObtenerPrestamosXCoordinadorCU (IRepositorioPrestamo repoP, IRepositorioUsuario repoU) 
        {
            _repoP = repoP;
            _repoU = repoU;
        }

        public IEnumerable<PrestamoDTO> Ejecutar(int coordinadorId)
        {
            LogicaNegocio.Entidades.Usuario coord = _repoU.FindById(coordinadorId);
            if (coord.Rol != Rol.Coordinador) throw new PrestamoException("El usuario proporcionado no es un coordinador");

            IEnumerable<LogicaNegocio.Entidades.Prestamo> prestamos = _repoP.ObtenerPrestamosXCoordinador(coordinadorId);
            return prestamos.Select(p => PrestamoMapper.ToDto(p));
        }
    }
}
