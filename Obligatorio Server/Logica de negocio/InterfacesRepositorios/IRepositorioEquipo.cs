using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioEquipo : IRepositorio<Equipo>
    {
        public void Add(Equipo aAgregar);
        public bool EnPrestamo(int id);
        public IEnumerable<Telescopio> ObtenerTelescopios();
    }
}
