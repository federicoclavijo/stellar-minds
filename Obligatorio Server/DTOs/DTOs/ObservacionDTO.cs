using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs
{
    public class ObservacionDTO
    {
        public int Id { get; set; }
        public DateTime FechaObservacion { get; set; }
        public int PrestamoId { get; set; }
        public PrestamoDTO? Prestamo { get; set; }
        public int ObjetoCelesteId { get; set; }
        public ObjetoCelesteDTO? ObjetoCeleste { get; set; }
        public string Indicador { get; set; }
        public string Detalle { get; set; }
    }

}