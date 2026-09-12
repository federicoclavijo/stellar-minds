using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.CasosDeUso.Usuario;
using LogicaAplicacion.InterfacesCasosDeUso.Prestamo;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Prestamo
{
    public class ObtenerPrestamosEntreFechasCU : IObtenerPrestamosEntreFechas
    {
        private IRepositorioPrestamo _repoP;
        private IRepositorioUsuario _repoU;
        public ObtenerPrestamosEntreFechasCU(IRepositorioPrestamo repoP, IRepositorioUsuario repoU)
        {
            _repoP = repoP;
            _repoU = repoU;
        }

        public IEnumerable<PrestamoDTO> Ejecutar(int mes, int anio, int usuarioId)
        {
            if (mes < 0 || anio < 0) throw new PrestamoException("La fecha no puede tener valores menor a 0.");

            LogicaNegocio.Entidades.Usuario? usuario = _repoU.FindById(usuarioId);
            if (usuario == null) throw new UsuarioException("No existe un usuario con ese ID.");
            


            IEnumerable<LogicaNegocio.Entidades.Prestamo> prestamos = _repoP.ObtenerEntreFechas(mes, anio, usuarioId);

            return prestamos.Select(p => PrestamoMapper.ToDto(p));
        }
    }
}
