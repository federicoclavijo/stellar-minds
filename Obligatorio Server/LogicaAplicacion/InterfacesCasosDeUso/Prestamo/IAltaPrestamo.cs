using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.Prestamo
{
    public interface IAltaPrestamo
    {
        public void Ejecutar(PrestamoDTO dto, int coordinadorId);
    }
}
