using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Excepciones
{
    public class ObjetoCelesteException : Exception
    {
        public ObjetoCelesteException(string message) : base(message) { }
        public ObjetoCelesteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
