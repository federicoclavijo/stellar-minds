using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs
{
    public class OcularDTO : EquipoDTO
    {
        public double Diametro { get; set; }
        public double Angulo { get; set; }
        public OcularDTO()
        {
            TipoEquipo = "Ocular";
        }
    }
}
