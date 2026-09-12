using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Excepciones
{
    public class ObservacionException : Exception
    {
        public ObservacionException(string message) : base(message) { }
        public ObservacionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
