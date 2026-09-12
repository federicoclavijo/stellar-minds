using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAccesoDatos.EF.Repositorios
{
    public class RepositorioAuditoria : IRepositorioAuditoria
    {
        private ObligatorioContext _context;
        public RepositorioAuditoria(ObligatorioContext context)
        {
            _context = context;
        }

        public void Add(Auditoria aAgregar)
        {
            try
            {
                aAgregar.Id = 0;
                _context.Auditorias.Add(aAgregar);
                _context.SaveChanges();
            }
            catch (AuditoriaException ex)
            {
                throw new AuditoriaException(ex.Message);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Auditoria> FindAll()
        {
            return _context.Auditorias
                .Include(a => a.Usuario)
                .Include(a => a.Prestamo)
                .AsQueryable().ToList(); ;
        }

        public Auditoria FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Auditoria aActualizar)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Auditoria> ObtenerAuditoriasXPrestamo(int Id)
        {
            return _context.Auditorias
                .Include(a => a.Usuario)
                .Include(a => a.Prestamo)
                    .ThenInclude(p => p.Usuario)
                .Where(p => p.PrestamoId == Id)
                .AsQueryable()
                .ToList();
        }
    }
}
