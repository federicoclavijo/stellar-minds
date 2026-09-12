using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Entidades
{
    public class Camara : Equipo
    {
        public Sensor Sensor { get; set; }
        public string Resolucion { get; set; }
        public double Tamano { get; set; }

        public Camara()
        {
            Id = 0;

        }

        public override void Validar()
        {
            base.Validar();

            if (string.IsNullOrEmpty(Sensor.ToString()))
            {
                throw new EquipoException("El sensor no puede ser nulo o vacío.");
            }
            if (string.IsNullOrEmpty(Resolucion))
            {
                throw new EquipoException("La resolución no puede ser nula.");
            }
            if (Tamano <= 0)
            {
                throw new EquipoException("El tamaño debe ser mayor a 0.");
            }
        }
    }
}
