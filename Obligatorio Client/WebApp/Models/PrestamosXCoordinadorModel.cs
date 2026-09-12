namespace WebApp.Models
{
    public class PrestamosXCoordinadorModel
    {
        public int? UsuarioId { get; set; }

        public List<UsuarioModel>? Coordinadores { get; set; } = new List<UsuarioModel>();

        public List<PrestamoModel>? Prestamos { get; set; } = new List<PrestamoModel>();
    }
}
