using LogicaNegocio.Enums;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class PrestamoModel
    {

        [Display(Name = "ID")]
        public int Id { get; set; }

        // Datos del socio
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [Display(Name = "Usuario ID")]
        public int? UsuarioId { get; set; }
        public UsuarioModel? Usuario { get; set; }

        [Display (Name = "Fecha de inicio")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [Display(Name = "Fecha de inicio")]
        public DateTime FechaFin { get; set; } = DateTime.Now;
        public EstadoPrestamo Estado { get; set; }

        // Equipos
        [Required(ErrorMessage = "El telescopio es obligatorio.")]

        [Display(Name = "Telescopio ID")]
        public int? TelescopioId { get; set; }
        public EquipoModel? Telescopio { get; set; }

        [Required(ErrorMessage = "La montura es obligatoria.")]
        [Display(Name = "Montura ID")]
        public int? MonturaId { get; set; }
        public EquipoModel? Montura { get; set; }


        [Display(Name = "Cámara ID")]
        public int? CamaraId { get; set; }
        public EquipoModel? Camara { get; set; }


        [Display(Name = "Ocular ID")]
        public int? OcularId { get; set; }
        public EquipoModel? Ocular { get; set; }
    }
}
