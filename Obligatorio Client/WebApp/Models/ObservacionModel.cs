using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ObservacionModel
    {
        [Required(ErrorMessage = "Debe seleccionar un préstamo.")]
        [Display(Name = "Préstamo")]
        public int? PrestamoId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un objeto celeste.")]
        [Display(Name = "Objeto celeste")]
        public int? ObjetoCelesteId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una fecha.")]
        public DateTime FechaObservacion { get; set; } = DateTime.Now;

        public List<PrestamoModel> Prestamos { get; set; } = new List<PrestamoModel>();

        public List<ObjetoCelesteModel> ObjetosCelestes { get; set; } = new List<ObjetoCelesteModel>();

        public string? Indicador { get; set; }

        public string? Detalle { get; set; }

        public bool Evaluado { get; set; }
    }
}
