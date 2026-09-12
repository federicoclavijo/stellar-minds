using LogicaAplicacion.InterfacesCasosDeUso.Prestamo;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Prestamo
{
    public class DevolucionPrestamoCU : IDevolucionPrestamo
    {
        private IRepositorioPrestamo _repoP;
        private IRepositorioUsuario _repoU;
        private IRepositorioAuditoria _repoA;
        public DevolucionPrestamoCU(IRepositorioPrestamo repoP, IRepositorioUsuario repoU, IRepositorioAuditoria repoA)
        {
            _repoP = repoP;
            _repoU = repoU;
            _repoA = repoA;
        }
        public void Ejecutar(int id, int coordinadorId)
        {
            LogicaNegocio.Entidades.Prestamo prestamo = _repoP.FindById(id);
            if (prestamo == null)
                throw new PrestamoException("El préstamo no existe.");

            if (prestamo.Estado == EstadoPrestamo.DEVUELTO)
                throw new PrestamoException("El préstamo ya fue devuelto.");

            prestamo.Devolver();
            _repoP.Update(prestamo);



            LogicaNegocio.Entidades.Usuario coordinador = _repoU.FindById(coordinadorId);
            LogicaNegocio.Entidades.Auditoria auditoria = new LogicaNegocio.Entidades.Auditoria();
            auditoria.Usuario = coordinador;
            auditoria.Prestamo = prestamo;
            auditoria.Accion = TipoAccion.Devolucion;
            auditoria.Fecha = DateTime.Now;
            auditoria.Validar();
            _repoA.Add(auditoria);


        }
    }
}
