using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Equipo;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Equipo
{
    public class AltaEquipoCU : IAltaEquipo
    {
        private IRepositorioEquipo _repo;
        public AltaEquipoCU (IRepositorioEquipo repo)
        {
            _repo = repo;
        }
        public void Add(EquipoPlanoDTO dto)
        {
            LogicaNegocio.Entidades.Equipo entidad = EquipoMapper.FromDtoPlano(dto);
            entidad.Validar();
            _repo.Add(entidad);
        }

    }
}
