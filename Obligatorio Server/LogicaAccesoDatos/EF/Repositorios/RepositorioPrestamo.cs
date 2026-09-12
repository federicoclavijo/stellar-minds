using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
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
    public class RepositorioPrestamo : IRepositorioPrestamo
    {
        private ObligatorioContext _context;
        public RepositorioPrestamo(ObligatorioContext context)
        {
            _context = context;
        }
        public void Add(Prestamo aAgregar)
        {
            try
            {
                _context.Prestamos.Add(aAgregar);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new PrestamoException(ex.Message);
            }
        }

        public void Delete(int equipoId)
        {

        }

        public IEnumerable<Prestamo> FindAll()
        {
            return _context.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Telescopio)
                .Include(p => p.Montura)
                .Include(p => p.Camara)
                .Include(p => p.Ocular)
                .ToList();
        }

        public Prestamo FindById(int id)
        {
            Prestamo prestamo = _context.Prestamos
                .Include(p => p.Telescopio)
                .Include(p => p.Montura)
                .Include(p => p.Camara)
                .Include(p => p.Ocular)
                .Include(p => p.Usuario)
                .Where(p => p.Id == id)
                .FirstOrDefault();

            return prestamo;
        }

        public IEnumerable<Prestamo> ObtenerEntreFechas(int mes, int anio, int usuarioId)
        {
            return _context.Prestamos
                .Where(p => p.FechaInicio.Month == mes && p.FechaInicio.Year == anio && p.Usuario.Id == usuarioId)
                .Include(p => p.Usuario)
                .Include(p => p.Telescopio)
                .Include(p => p.Montura)
                .Include(p => p.Camara)
                .Include(p => p.Ocular)
                .ToList();
        }

        public void Update(Prestamo aActualizar)
        {

            try
            {
                _context.Update(aActualizar);
                _context.SaveChanges();
            }
            catch (PrestamoException ex)
            {
                string errorReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new EquipoException("Error en BD: " + errorReal);
            }
        }

        public IEnumerable<Usuario> ObtenerSociosXTelescopio(int telescopioId)
        {
            return _context.Prestamos
        .Where(p => p.Telescopio.Id == telescopioId)
        .Select(p => p.Usuario)
        .Distinct()
        .OrderByDescending(u => u.Nombre)
        .ToList();

        }

        public IEnumerable<Prestamo> ObtenerPrestamosPorEquipo(int equipoId)
        {
            return _context.Prestamos
                .Where(p => p.TelescopioId == equipoId || p.CamaraId == equipoId || p.MonturaId == equipoId || p.OcularId == equipoId)
                .ToList();
        }

        public IEnumerable<Prestamo> ObtenerPrestamosActivosXSocio(int id)
        {
            return _context.Prestamos
                .Where(p => p.Estado == EstadoPrestamo.EN_PRESTAMO && p.UsuarioId == id)
                .ToList();
        }

        public IEnumerable<Prestamo> ObtenerPrestamosXCoordinador(int coordinadorId)
        {
            return _context.Auditorias
        .Include(a => a.Prestamo)
            .ThenInclude(p => p.Usuario)
        .Where(a => a.UsuarioId == coordinadorId
                 && a.Accion == TipoAccion.Alta)
        .Select(a => a.Prestamo)
        .Distinct()
        .ToList();
        }

        public IEnumerable<Prestamo> ObtenerPrestamosVigentesXSocio(int id)
        {
            return _context.Prestamos
                .Where(p => p.Estado == EstadoPrestamo.EN_PRESTAMO && p.FechaFin >= DateTime.Now && p.UsuarioId == id)
                .ToList();
        }
    }
}
