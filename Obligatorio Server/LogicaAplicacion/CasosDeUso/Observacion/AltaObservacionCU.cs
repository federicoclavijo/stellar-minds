using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Observacion;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Observacion
{
    public class AltaObservacionCU : IAltaObservacion
    {
        private IRepositorioObservacion _repoO;
        private IRepositorioPrestamo _repoP;
        private IRepositorioObjetoCeleste _repoOC;

        public AltaObservacionCU(IRepositorioObservacion repoO, IRepositorioObjetoCeleste repoOC, IRepositorioPrestamo repoP)
        {
            _repoO = repoO;
            _repoOC = repoOC;
            _repoP = repoP;
        }

        public void Ejecutar(ObservacionDTO dto)
        {
            if (dto.Indicador == "NO_RECOMENDABLE") throw new ObservacionException("No puede darse de alta una observación con indicador: NO RECOMENDABLE");

            LogicaNegocio.Entidades.Observacion observacion = ObservacionMapper.FromDto(dto);

            LogicaNegocio.Entidades.Prestamo? prestamo = _repoP.FindById(dto.PrestamoId);
            if (prestamo == null) throw new PrestamoException("No se encontró ningún préstamo con esa ID.");
            observacion.Prestamo = prestamo;

            LogicaNegocio.Entidades.ObjetoCeleste? objetoceleste = _repoOC.FindById(dto.ObjetoCelesteId);
            if (objetoceleste == null) throw new EquipoException("No se encontró ningún equipo con esa ID.");
            observacion.ObjetoCeleste = objetoceleste;

            observacion.Validar();
            _repoO.Add(observacion);
        }
    }
}
