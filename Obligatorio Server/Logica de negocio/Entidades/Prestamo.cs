using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogicaNegocio.Entidades
{
    public class Prestamo : IValidable
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoPrestamo Estado { get; set; }
        public Usuario? Usuario { get; set; }
        [ForeignKey(nameof(Usuario))] public int? UsuarioId { get; set; }
        public Telescopio? Telescopio { get; set; }
        [ForeignKey(nameof(Telescopio))] public int? TelescopioId { get; set; }

        public Montura? Montura { get; set; }
        [ForeignKey(nameof(Montura))] public int? MonturaId { get; set; }

        public Camara? Camara { get; set; }
        [ForeignKey(nameof(Camara))] public int? CamaraId { get; set; }
        public Ocular? Ocular { get; set; }
        [ForeignKey(nameof(Ocular))] public int? OcularId { get; set; }


        public Prestamo()
        {
            Id = 0;
        }
        public void Validar()
        {
            if (CamaraId == null && OcularId == null) throw new PrestamoException("Debe seleccionar al menos una cámara o un ocular.");
            if (Telescopio == null) throw new PrestamoException("El telescopio no puede ser nulo.");
            if (Montura == null) throw new PrestamoException("La montura no puede ser nula.");
            if (Montura.CargaMax < Telescopio.Peso) throw new PrestamoException("La montura no soporta el peso del telescopio.");
            if (Montura.CargaMax < Telescopio.Peso) throw new PrestamoException("La montura no soporta el peso del telescopio.");
            if (CamaraId.HasValue && Montura.TipoMontura == TipoMontura.Altazimutal) throw new PrestamoException("Las cámaras requieren monturas Ecuatoriales o Híbridas.");
            if (FechaInicio >= FechaFin) throw new PrestamoException("La fecha de fin no puede ser anterior o igual al dia de hoy");
        }
        

        public void Prestar()
        {
            Telescopio.DecrementarStock();
            Montura.DecrementarStock();
            if (Camara != null) Camara.DecrementarStock();
            if (Ocular != null) Ocular.DecrementarStock();
        }
        public void Devolver()
        {
            Estado = EstadoPrestamo.DEVUELTO;
            Telescopio.IncrementarStock();
            Montura.IncrementarStock();
            if (Camara != null) Camara.IncrementarStock();
            if (Ocular != null) Ocular.IncrementarStock();

        }
    }
}
