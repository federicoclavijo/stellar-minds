namespace WebApp.Models
{
    public class PrestamoFiltroViewModel
    {
        public DateTime? Fecha { get; set; }
        public IEnumerable<PrestamoModel> Prestamos { get; set; } = new List<PrestamoModel>();
    }
}
