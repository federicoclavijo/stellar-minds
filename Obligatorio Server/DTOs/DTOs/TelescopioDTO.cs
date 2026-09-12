using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs
{
    public class TelescopioDTO : EquipoDTO
    {
        public double Apertura { get; set; }
        public double DistanciaFocal { get; set; }
        public string RelacionFocal { get; set; }
        public double Peso { get; set; }

        public TelescopioDTO()
        {
            TipoEquipo = "Telescopio";
        }
    }
}
