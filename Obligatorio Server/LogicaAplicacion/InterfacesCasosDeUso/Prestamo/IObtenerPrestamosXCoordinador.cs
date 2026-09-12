using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.Prestamo
{
    public interface IObtenerPrestamosXCoordinador
    {
        public IEnumerable<PrestamoDTO> Ejecutar(int coordinadorId);
    }
}
