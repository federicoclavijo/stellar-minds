using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.ObjetoCeleste;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.ObjetoCeleste
{
    public class ObtenerObjetosCelestesCU : IObtenerObjetosCelestes
    {
        private IRepositorioObjetoCeleste _repositorio;
        public ObtenerObjetosCelestesCU(IRepositorioObjetoCeleste repo)
        {
            _repositorio = repo;
        }
        public IEnumerable<ObjetoCelesteDTO> Ejecutar()
        {
            IEnumerable<LogicaNegocio.Entidades.ObjetoCeleste> objetos = _repositorio.FindAll();

            return objetos.Select(o => ObjetoCelesteMapper.ToDto(o));
        }
    }
}
