using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Excepciones
{
    public class ConsultaException : Exception
    {
        public ConsultaException() { }
        public ConsultaException(string message) : base(message) { }
        public ConsultaException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
