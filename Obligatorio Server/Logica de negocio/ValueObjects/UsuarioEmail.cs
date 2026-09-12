using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public class UsuarioEmail : IValidable
    {
        public string Email { get; set; }

        public UsuarioEmail(string email)
        {
            Email = email;
        }

        public UsuarioEmail() { }

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Email))
                throw new UsuarioException("El email no puede estar vacío.");
        }
    }
}
