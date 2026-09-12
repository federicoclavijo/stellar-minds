using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.Prestamo
{
    public interface IPrestamosVigentesDeSocio
    {
        IEnumerable<PrestamoDTO> Ejecutar(int id);
    }
}
