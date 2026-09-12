using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public class UsuarioPassword : IValidable
    {
        public string Password { get; set; }

        public UsuarioPassword(string password)
        {
            Password = password;
        }

        public UsuarioPassword() { }

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Password))
                throw new UsuarioException("La contraseña no puede estar vacía.");
            if (Password.Length < 8)
                throw new UsuarioException("La contraseña debe tener al menos 8 caracteres.");
            if (!Password.Any(char.IsUpper))
                throw new UsuarioException("La contraseña debe contener al menos una letra mayúscula.");
            if (!Password.Any(char.IsLower))
                throw new UsuarioException("La contraseña debe contener al menos una letra minúscula.");
            if (!Password.Any(char.IsDigit))
                throw new UsuarioException("La contraseña debe contener al menos un número.");
            if (!Password.Any(c => !char.IsLetterOrDigit(c)))
                throw new UsuarioException("La contraseña debe contener al menos un carácter especial.");
        }
    }
}
