namespace WebApp.Models
{
    public class ListadoSociosXTelescopioModel
    {
        public int? TelescopioId { get; set; }

        public List<EquipoModel> Telescopios { get; set; } = new List<EquipoModel>();

        public List<UsuarioModel> Socios { get; set; } = new List<UsuarioModel>();
    }
}
