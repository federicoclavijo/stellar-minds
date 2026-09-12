using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Equipo;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Equipo
{
    public class ObtenerEquiposCU : IObtenerEquipos
    {
        private IRepositorioEquipo _repo;

        public ObtenerEquiposCU(IRepositorioEquipo repo)
        {
            _repo = repo;
        }

        public IEnumerable<EquipoPlanoDTO> Ejecutar()
        {
            IEnumerable<LogicaNegocio.Entidades.Equipo> lista = _repo.FindAll();
            return lista.Select(ent => EquipoMapper.ToDtoPlano(ent));
        }
    }
}
