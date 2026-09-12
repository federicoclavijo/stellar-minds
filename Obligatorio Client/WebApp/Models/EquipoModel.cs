using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class EquipoModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "La marca es requerida.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El modelo es requerido.")]
        public string Modelo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El stock debe ser mayor a 0.")]
        [Required(ErrorMessage = "La cantidad disponible es requerida.")]
        public int Stock { get; set; }
        public string TipoEquipo { get; set; }



        [Range(1, int.MaxValue, ErrorMessage = "La apertura debe ser mayor a 0.")]
        [Required(ErrorMessage = "La apertura es requerida.")]
        public int? Apertura { get; set; }

        [Required(ErrorMessage = "La relación focal es requerida.")]
        public string? RelacionFocal { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La distancia focal debe ser mayor a 0.")]
        [Required(ErrorMessage = "La distancia focal es requerida.")]
        public int? DistanciaFocal { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El peso debe ser mayor a 0.")]
        [Required(ErrorMessage = "El peso es requerido.")]
        public double? Peso { get; set; }

        [Required(ErrorMessage = "El tipo de montura es requerido.")]
        public string? TipoMontura { get; set; } 

        [Range(0.1, double.MaxValue, ErrorMessage = "La carga máxima debe ser mayor a 0.")]
        [Required(ErrorMessage = "La carga máxima es requerida.")]
        public double? CargaMax { get; set; }

        [Required(ErrorMessage = "La especificación de GoTo es requerida.")]
        public bool? GoTo { get; set; }

        [Required(ErrorMessage = "El tipo de sensor es requerido.")]
        public string? Sensor { get; set; }

        [Required(ErrorMessage = "La resolución es requerida.")]
        public string? Resolucion { get; set; } 

        [Range(0.01, double.MaxValue, ErrorMessage = "El tamaño del píxel debe ser mayor a 0.")]
        [Required(ErrorMessage = "El tamaño es requerido.")]
        public double? Tamano { get; set; } 


        [Range(1, int.MaxValue, ErrorMessage = "El diámetro debe ser mayor a 0.")]
        [Required(ErrorMessage = "El diámetro es requerido.")]
        public int? Diametro { get; set; }
        [Range(1, 360, ErrorMessage = "El ángulo de visión debe estar entre 1 y 360 grados.")]
        [Required(ErrorMessage = "El ángulo es requerido.")]
        public int? Angulo { get; set; }
    }
}
