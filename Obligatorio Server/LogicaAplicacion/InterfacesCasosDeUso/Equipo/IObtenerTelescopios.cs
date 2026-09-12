using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.Equipo
{
    public interface IObtenerTelescopios
    {
        public IEnumerable<EquipoPlanoDTO> Ejecutar();
    }
}
