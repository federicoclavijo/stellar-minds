using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioObservacion : IRepositorio<Observacion>
    {
        public IEnumerable<object> ListarRanking();
    }
}
