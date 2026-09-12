using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Equipo;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Equipo
{
    public class ObtenerTelescopiosCU : IObtenerTelescopios
    {
        private IRepositorioEquipo _repo;

        public ObtenerTelescopiosCU(IRepositorioEquipo repo)
        {
            _repo = repo;
        }

        public IEnumerable<EquipoPlanoDTO> Ejecutar()
        {
            IEnumerable<LogicaNegocio.Entidades.Equipo> lista = _repo.FindAll();
            return lista.Select(e => EquipoMapper.ToDtoPlano(e))
                        .Where(e => e.TipoEquipo == "Telescopio");
        }
    }
}
