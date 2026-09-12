using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string User { get; set; }
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener un mínimo de 8 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).*$", ErrorMessage = "Debe contener mayúscula, minúscula, número y símbolo.")]
        public string Password { get; set; }
        public string Rol { get; set; } = string.Empty;
        public string Token { get; set; }
    }
}
