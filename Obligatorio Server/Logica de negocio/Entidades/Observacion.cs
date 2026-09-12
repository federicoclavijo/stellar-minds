using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogicaNegocio.Entidades
{
    public class Observacion : IValidable
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Prestamo Prestamo { get; set; }
        [ForeignKey(nameof(Prestamo))] public int PrestamoId { get; set; }
        public ObjetoCeleste ObjetoCeleste { get; set; }
        [ForeignKey(nameof(ObjetoCeleste))] public int ObjetoCelesteId { get; set; }
        public EvaluacionIA Evaluacion { get; set; }

        public Observacion() 
        {
            Id = 0;
        }
        public void Validar() 
        {
            if (Prestamo == null)
                throw new ObservacionException("Debe indicar un préstamo.");

            if (ObjetoCeleste == null)
                throw new ObservacionException("Debe indicar un objeto celeste.");

            if(Evaluacion == null)
                throw new ObservacionException("Debe indicar una evaluación.");

            if(Fecha < DateTime.Now)
                throw new ObservacionException("La fecha no puede ser anterior a la fecha actual.");
            if (Fecha > Prestamo.FechaFin || Fecha < Prestamo.FechaInicio)
                throw new ObservacionException("El préstamo no está vigente en la fecha ingresada.");
        }
    }
}
