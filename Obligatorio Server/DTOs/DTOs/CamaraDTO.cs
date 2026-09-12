using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs
{
    public class CamaraDTO : EquipoDTO
    {
        public string Sensor { get; set; } = string.Empty;
        public string Resolucion { get; set; } = string.Empty;
        public double Tamano { get; set; }
        public CamaraDTO()
        {
            TipoEquipo = "Camara";
        }
    }
}
