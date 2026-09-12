using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTOs
{
    public class ConsultaObservacionDTO
    {
        public int PrestamoId { get; set; }
        public int ObjetoCelesteId { get; set; }
        public DateTime FechaObservacion { get; set; }
    }
}
