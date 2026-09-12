using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Entidades
{
    public class ObjetoCeleste : IValidable
    {
        public int Id { get; set; }
        public NombreObjetoCeleste Nombre { get; set; }
        public TipoObjetoCeleste Tipo { get; set; }
        public double Magnitud { get; set; }

        public ObjetoCeleste()
        {
            Id = 0;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre.ToString()))
            {
                throw new ObjetoCelesteException("El nombre del objeto celeste no puede ser nulo o vacío.");
            }
            if (string.IsNullOrEmpty(Tipo.ToString()))
            {
                throw new ObjetoCelesteException("El tipo del objeto celeste no puede ser nulo o vacío.");
            }
            if (Magnitud < 0)
            {
                throw new ObjetoCelesteException("La magnitud del objeto celeste debe ser mayor o igual a 0.");
            }
        }
    }
}