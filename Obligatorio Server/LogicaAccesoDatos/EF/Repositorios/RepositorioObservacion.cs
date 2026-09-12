using DTOs.DTOs;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF.Repositorios
{
    public class RepositorioObservacion : IRepositorioObservacion
    {
        private ObligatorioContext _context;
        public RepositorioObservacion(ObligatorioContext context)
        {
            _context = context;
        }
        public void Add(Observacion aAgregar)
        {
            try
            {
                _context.Observaciones.Add(aAgregar);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ObservacionException(ex.Message);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Observacion> FindAll()
        {
            return _context.Observaciones
                .Include(p => p.ObjetoCeleste)
                .Include(p => p.Evaluacion)
                .Include(p => p.Prestamo)
                .AsQueryable()
                .ToList(); ;
        }

        public Observacion FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Observacion aActualizar)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<object> ListarRanking()
        {
            return _context.Observaciones
        .GroupBy(o => new
        {
            o.ObjetoCeleste.Nombre,
            o.ObjetoCeleste.Tipo
        })  
        .Select(ranking => new RankingObjetosCelestesDTO
        {
            Nombre = ranking.Key.Nombre.ToString(),
            Tipo = ranking.Key.Tipo.ToString(),
            CantidadObservaciones = ranking.Count()
        })
        .OrderByDescending(ranking => ranking.CantidadObservaciones)
        .ToList();
        }
    }
}
