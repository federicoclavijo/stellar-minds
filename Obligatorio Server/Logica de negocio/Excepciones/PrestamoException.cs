using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Excepciones
{
    public class PrestamoException : Exception
    {
        public PrestamoException(string message) : base(message) { }
        public PrestamoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
