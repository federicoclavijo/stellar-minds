using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Entidades
{
    public class Telescopio : Equipo
    {
        public double Apertura { get; set; }
        public string RelacionFocal { get; set; }
        public double DistanciaFocal { get; set; }
        public double Peso { get; set; }

        public Telescopio() 
        {
            Id = 0;
        }

        public override void Validar() 
        {
            base.Validar();

            if (Apertura <= 0)
            {
                throw new EquipoException("La apertura debe ser mayor a 0.");
            }
            if (string.IsNullOrEmpty(RelacionFocal))
            {
                throw new EquipoException("La relación no puede ser nula.");
            }
            if (DistanciaFocal <= 0)
            {
                throw new EquipoException("La distancia focal debe ser mayor a 0.");
            }
            if (Peso <= 0)
            {
                throw new EquipoException("El peso debe ser mayor a 0.");
            }
        }
    }
}
