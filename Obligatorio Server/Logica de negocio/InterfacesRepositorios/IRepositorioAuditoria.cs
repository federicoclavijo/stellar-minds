using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioAuditoria : IRepositorio<Auditoria>
    {
        public IEnumerable<Auditoria> ObtenerAuditoriasXPrestamo(int id);
    }
}
