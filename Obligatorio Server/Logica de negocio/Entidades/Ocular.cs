using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Entidades
{
    public class Ocular : Equipo
    {
        public double Diametro { get; set; }
        public double Angulo { get; set; }

        public Ocular() 
        {
            Id = 0;
        }

        public override void Validar() 
        {
            base.Validar();


            if (Diametro <= 0)
            {
                throw new EquipoException("El diámetro debe ser mayor a 0.");
            }
            if (Angulo <= 0)
            {
                throw new EquipoException("El ángulo debe ser mayor a 0.");
            }
        }
    }
}
