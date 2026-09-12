using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.Prestamo
{
    public interface IObtenerPrestamosEntreFechas
    {
        public IEnumerable<PrestamoDTO> Ejecutar(int mes, int anio, int usuarioId);
    }
}
