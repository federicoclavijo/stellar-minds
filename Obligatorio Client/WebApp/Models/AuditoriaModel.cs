namespace WebApp.Models
{
    public class AuditoriaModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioModel? Usuario { get; set; }
        public int PrestamoId { get; set; }
        public PrestamoModel? Prestamo { get; set; }
        public string Accion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }
}