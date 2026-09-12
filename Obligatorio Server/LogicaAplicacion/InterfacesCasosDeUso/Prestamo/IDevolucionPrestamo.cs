using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.Prestamo
{
    public interface IDevolucionPrestamo
    {
        public void Ejecutar(int id, int coordinadorId);
    }
}
