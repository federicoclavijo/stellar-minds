using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs
{
    public class MonturaDTO : EquipoDTO
    {
        public string TipoMontura { get; set; } = string.Empty;
        public double CargaMax { get; set; }
        public bool GoTo { get; set; }
        public MonturaDTO()
        {
            TipoEquipo = "Montura";
        }
    }
}
