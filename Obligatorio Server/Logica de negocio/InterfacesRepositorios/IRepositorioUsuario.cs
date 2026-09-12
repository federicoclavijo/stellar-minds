using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        Usuario? FindByUsername(string username);
        public void Add(Usuario usuario);
        public Usuario Login(string username, string password);
        public IEnumerable<Usuario> ObtenerSocios();
        public IEnumerable<Usuario> ObtenerCoordinadores();

    }
}
