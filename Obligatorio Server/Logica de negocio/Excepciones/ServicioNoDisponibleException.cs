using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Excepciones
{
    public class ServicioNoDisponibleException : Exception
    {
        public ServicioNoDisponibleException() { }
        public ServicioNoDisponibleException(string message) : base(message) { }
        public ServicioNoDisponibleException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
