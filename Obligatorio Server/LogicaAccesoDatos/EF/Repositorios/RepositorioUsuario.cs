using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF.Repositorios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private ObligatorioContext _context;
        public RepositorioUsuario (ObligatorioContext context)
        {
            _context = context;
        }
            

            public void Add(Usuario aAgregar)
            {
                try
                {
                    _context.Usuarios.Add(aAgregar);
                    _context.SaveChanges(); 
            }
                catch (UsuarioException ex)
                {
                    throw new UsuarioException(ex.Message);
                }
            }

            public void Delete(int id)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Usuario> FilterByTitle(string titulo)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Usuario> FindAll()
            {
                return _context.Usuarios.AsQueryable().ToList(); ;
            }

            public Usuario FindById(int id)
            {
                Usuario u = _context.Usuarios
                                    .Where(u => u.Id == id)
                                    .FirstOrDefault();
                return u;
            }


            public void Update(Usuario aActualizar)
            {
                throw new NotImplementedException();
            }

        public Usuario Login(string username, string password)
        {
            return _context.Usuarios
                .Where(u => u.User == username && u.Password.Password == password)
                .FirstOrDefault();
        }

        public IEnumerable<Usuario> ObtenerSocios()
        {
            return _context.Usuarios
                .Where(u => u.Rol == Rol.Socio)
                .ToList();
        }
        public IEnumerable<Usuario> ObtenerCoordinadores()
        {
            return _context.Usuarios
                .Where(u => u.Rol == Rol.Coordinador)
                .ToList();
        }

        public Usuario? FindByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}

