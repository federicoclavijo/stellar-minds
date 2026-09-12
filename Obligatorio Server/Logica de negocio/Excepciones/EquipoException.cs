using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Excepciones
{
    public class EquipoException : Exception
    {
        public EquipoException(string message) : base(message) { }
        public EquipoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
