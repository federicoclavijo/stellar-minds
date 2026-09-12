using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LogicaNegocio.Entidades
{
    public class Usuario : IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public UsuarioEmail Email { get; set; }
        public string User { get; set; }
        public UsuarioPassword Password { get; set; }
        public Rol Rol { get; set; }

        public Usuario()
        {
            Id = 0;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
                throw new UsuarioException("El nombre no puede ser nulo o vacío.");
            if (string.IsNullOrEmpty(Apellido))
                throw new UsuarioException("El apellido no puede ser nulo o vacío.");
            if (string.IsNullOrEmpty(Direccion))
                throw new UsuarioException("La dirección no puede ser nula o vacía.");
            if (string.IsNullOrEmpty(Telefono))
                throw new UsuarioException("El teléfono no puede ser nulo o vacío.");
            if (Email == null)
                throw new UsuarioException("El email no puede ser nulo.");
            if (string.IsNullOrEmpty(User))
                throw new UsuarioException("El usuario no puede ser nulo o vacío.");
            if (Password == null)
                throw new UsuarioException("La contraseña no puede ser nula.");
        }

    }
}
