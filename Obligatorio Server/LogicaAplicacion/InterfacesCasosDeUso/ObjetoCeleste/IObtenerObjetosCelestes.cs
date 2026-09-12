using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.ObjetoCeleste
{
    public interface IObtenerObjetosCelestes
    {
        public IEnumerable<ObjetoCelesteDTO> Ejecutar();
    }
}
