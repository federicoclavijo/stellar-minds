using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosDeUso.Observacion
{
    public interface IConsultaIA
    {
        public string Ejecutar(ConsultaDTO consulta);
    }
}
