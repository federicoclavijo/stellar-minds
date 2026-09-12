using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Entidades
{
    public abstract class Equipo : IValidable
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Stock { get; set; }

        public Equipo()
        {
            Id = 0;
        }

        public virtual void Validar()
        {
            if(string.IsNullOrEmpty(Marca))
            {
                throw new EquipoException("La marca no puede ser nula");
            }
            if (string.IsNullOrEmpty(Modelo))
            {
                throw new EquipoException("El modelo no puede ser nulo");
            }
            if (Stock < 0)
            {
                throw new EquipoException("El stock debe ser mayor a 0");
            }
        }

        public void IncrementarStock()
        {
            Stock++;
        }
        
        public void DecrementarStock()
        {
            if (Stock > 0) 
            {
                Stock--; 
            }
            else
            {
                throw new EquipoException("No hay stock disponible.");
            }
                
        }
    }
}
