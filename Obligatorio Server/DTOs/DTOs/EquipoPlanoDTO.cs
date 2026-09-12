using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs
{
    public class EquipoPlanoDTO
    {
        public int Id { get; set; } // Identificador autonumérico [cite: 191]

        [Required(ErrorMessage = "La marca es requerida.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El modelo es requerido.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "La cantidad disponible es requerida.")]
        public int Stock { get; set; }
        public string TipoEquipo { get; set; } // Puede tomar valores: "Telescopio", "Montura", "Camara", "Ocular" 



        //  [Range(1, int.MaxValue, ErrorMessage = "La apertura debe ser mayor a 0.")]
        public double? Apertura { get; set; }

        public string? RelacionFocal { get; set; } // Ej: f/10, f/5 [cite: 31]

        // [Range(1, int.MaxValue, ErrorMessage = "La distancia focal debe ser mayor a 0.")]
        public double? DistanciaFocal { get; set; }

        //   [Range(0.01, double.MaxValue, ErrorMessage = "El peso debe ser mayor a 0.")]
        public double? Peso { get; set; }

        public string? TipoMontura { get; set; } // Ecuatorial, Alt-Azimutal o Híbrida [cite: 42]

        //   [Range(0.1, double.MaxValue, ErrorMessage = "La carga útil debe ser mayor a 0.")]
        public double? CargaMax { get; set; }

        public bool? GoTo { get; set; }


        public string? Sensor { get; set; } // CMOS o CCD [cite: 44]

        public string? Resolucion { get; set; } // Ej: 3840x2160 [cite: 44, 89]
        //[Range(0.01, double.MaxValue, ErrorMessage = "El tamaño del píxel debe ser mayor a 0.")]
        public double? Tamano { get; set; } // En micras [cite: 44]


        //[Range(1, int.MaxValue, ErrorMessage = "El diámetro debe ser mayor a 0.")]
        public double? Diametro { get; set; }
        // [Range(1, 360, ErrorMessage = "El ángulo de visión debe estar entre 1 y 360 grados.")]
        public double? Angulo { get; set; }
    }
}
