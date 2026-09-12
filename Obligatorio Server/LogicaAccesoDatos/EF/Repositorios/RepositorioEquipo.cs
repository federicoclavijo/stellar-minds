using Azure;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF.Repositorios
{
    public class RepositorioEquipo : IRepositorioEquipo
    {
        private ObligatorioContext _context;
        public RepositorioEquipo(ObligatorioContext context)
        {
            _context = context;
        }

        public void Add(Equipo equipo)
        {
            try
            {
                _context.Equipos.Add(equipo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new EquipoException(ex.Message);
            }
        }

        public void Delete(int id)
        {
            _context.Equipos.Remove(FindById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Equipo> FindAll()
        {
            return _context.Equipos.AsQueryable().ToList();
        }

        public Equipo FindById(int id)
        {
            Equipo e = _context.Equipos.
                Where(equipo => equipo.Id == id).FirstOrDefault();
            return e;
        }

        public void Update(Equipo equipo)
        {
            try
            {
                equipo.Validar();
                _context.Update(equipo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new EquipoException("Error en BD: " + errorReal);
            }
        }

        public bool EnPrestamo(int id)
        {
            return _context.Prestamos
            .Any(p =>
            p.Estado == EstadoPrestamo.EN_PRESTAMO &&
            (
                p.TelescopioId == id ||
                p.MonturaId == id ||
                p.CamaraId == id ||
                p.OcularId == id
            ));

        }

        public IEnumerable<Telescopio> ObtenerTelescopios()
        {
            return _context.Equipos
                            .OfType<Telescopio>()
                            .ToList();
        }
    }
}
