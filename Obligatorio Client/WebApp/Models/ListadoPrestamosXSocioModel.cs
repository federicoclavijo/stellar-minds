namespace WebApp.Models
{
    public class ListadoPrestamosXSocioModel
    {
        public int? UsuarioId { get; set; }

        public List<UsuarioModel>? Socios { get; set; } = new List<UsuarioModel>();

        public List<PrestamoModel>? Prestamos { get; set; } = new List<PrestamoModel>();
    }
}
