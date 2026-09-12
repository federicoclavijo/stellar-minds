using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; } = string.Empty;
        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string Direccion { get; set; } = string.Empty;
        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        public string Telefono { get; set; } = string.Empty;
        [Required(ErrorMessage = "El email es obligatorio.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string User { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener un mínimo de 8 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).*$", ErrorMessage = "Debe contener mayúscula, minúscula, número y símbolo.")]
        public string Password { get; set; } = string.Empty;
    }
}
