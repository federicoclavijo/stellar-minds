using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Entidades
{
    public class Montura : Equipo
    {
        public TipoMontura TipoMontura { get; set; }
        public double CargaMax { get; set; }
        public bool GoTo { get; set; }

        public Montura()
        {
            Id = 0;
        }

        public override void Validar()
        {
            base.Validar();

            if (string.IsNullOrEmpty(TipoMontura.ToString()))
            {
                throw new EquipoException("El tipo de montura no puede ser nulo o vacío.");
            }
            if (CargaMax <= 0)
            {
                throw new EquipoException("La carga máxima debe ser mayor a 0");
            }
        }
    }
}
