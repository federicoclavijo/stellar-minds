using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.Auditoria
{
    public interface IObtenerAuditoriasXPrestamo
    {
        public IEnumerable<AuditoriaDTO> Ejecutar(int id);
    }
}
