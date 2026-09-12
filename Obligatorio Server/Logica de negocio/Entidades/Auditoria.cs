using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogicaNegocio.Entidades
{
    public class Auditoria
    {
        public int Id { get; set; }
        public Prestamo Prestamo { get; set; } = null!;
        [ForeignKey(nameof(Prestamo))] public int PrestamoId { get; set; }

        public Usuario Usuario { get; set; }
        [ForeignKey(nameof(Usuario))] public int UsuarioId { get; set; }
        public TipoAccion Accion { get; set; }
        public DateTime Fecha { get; set; }

        public Auditoria()  
        {
            Id = 0;
        }

        public void Validar()
        {
            if (Usuario == null) throw new AuditoriaException("El usuario no puede ser nulo.");
            if (Prestamo == null) throw new AuditoriaException("El préstamo no puede ser nulo.");
            if(Accion != TipoAccion.Alta && Accion != TipoAccion.Devolucion) throw new AuditoriaException("La acción debe ser Alta o Devolución.");
            if (Accion == TipoAccion.Alta && Fecha != Prestamo.FechaInicio) throw new AuditoriaException("La fecha de auditoría debe ser la fecha de inicio de préstamo.");
        }
    }
}
