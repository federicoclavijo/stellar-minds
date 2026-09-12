using DTOs.DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Equipo;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using DTOs.Mappers;
using LogicaNegocio.Excepciones;

namespace LogicaAplicacion.CasosDeUso.Equipo
{
    public class ObtenerEquipoXIdCU : IObtenerEquipoXId
    {
        private IRepositorioEquipo _repo;

        public ObtenerEquipoXIdCU(IRepositorioEquipo repo)
        {
            _repo = repo;
        }

        public EquipoDTO Ejecutar(int id)
        {
            LogicaNegocio.Entidades.Equipo? entidad = _repo.FindById(id);

            if (entidad == null) throw new EquipoException ("No existe equipo con ese Id");

            return EquipoMapper.ToDto(entidad);
        }
    }
}
