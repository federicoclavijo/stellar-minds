using DTOs.DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.ObjetoCeleste;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.ObjetoCeleste
{
    public class ListadoRankingObjetosCU : IListadoRankingObjetos
    {
        private IRepositorioObservacion _repositorio;
        public ListadoRankingObjetosCU(IRepositorioObservacion repo)
        {
            _repositorio = repo;
        }

        public IEnumerable<RankingObjetosCelestesDTO> Ejecutar()
        {
            IEnumerable<RankingObjetosCelestesDTO> ranking = _repositorio.FindAll()
         .GroupBy(o => o.ObjetoCeleste)
        .Select(ranking => new RankingObjetosCelestesDTO
        {
            Nombre = ranking.Key.Nombre.ToString(),
            Tipo = ranking.Key.Tipo.ToString(),
            CantidadObservaciones = ranking.Count()
        })
        .OrderByDescending(ranking => ranking.CantidadObservaciones);
                return ranking;
        }
    }
}
