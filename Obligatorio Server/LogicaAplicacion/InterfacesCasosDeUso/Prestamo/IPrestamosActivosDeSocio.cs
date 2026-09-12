using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.Prestamo
{
    public interface IPrestamosActivosDeSocio
    {
        public IEnumerable<PrestamoDTO> Ejecutar(int id);
    }
}
