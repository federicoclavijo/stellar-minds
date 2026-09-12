using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Excepciones
{
    public class MuchasPeticionesException : Exception
    {
        public MuchasPeticionesException() { }
        public MuchasPeticionesException(string message) : base(message) { }
        public MuchasPeticionesException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
