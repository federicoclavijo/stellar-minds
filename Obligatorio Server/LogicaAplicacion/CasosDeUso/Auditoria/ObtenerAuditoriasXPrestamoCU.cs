using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Auditoria;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Auditoria
{
    public class ObtenerAuditoriasXPrestamoCU : IObtenerAuditoriasXPrestamo
    {
        private IRepositorioAuditoria _repoA;
        private IRepositorioPrestamo _repoP;

        public ObtenerAuditoriasXPrestamoCU(IRepositorioAuditoria repoA, IRepositorioPrestamo repoP)
        {
            _repoA = repoA;
            _repoP = repoP;
        }

        public IEnumerable<AuditoriaDTO> Ejecutar(int id)
        {
            LogicaNegocio.Entidades.Prestamo? prestamo = _repoP.FindById(id);
            if(prestamo == null)
            {
                throw new PrestamoException("No existe préstamo con esa ID");
            }
            return _repoA.ObtenerAuditoriasXPrestamo(id)
                                    .Select(a => AuditoriaMapper.ToDTO(a));
        }
    }
}
