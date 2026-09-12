using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesDominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public class EvaluacionIA
    {
        public Indicador Indicador { get; set; }
        public string Detalle { get; set; }

        public EvaluacionIA (Indicador indicador, string detalle)
        {
            Indicador = indicador;
            Detalle = detalle;
        }
    }
}
