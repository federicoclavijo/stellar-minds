using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.Equipo
{
    public interface IUpdate
    {
        public void Ejecutar(EquipoPlanoDTO dto);
    }
}
