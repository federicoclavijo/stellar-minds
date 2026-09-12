using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.Prestamo
{
    public interface IObtenerPrestamoXId
    {
        public PrestamoDTO Ejecutar(int id);
    }
}
