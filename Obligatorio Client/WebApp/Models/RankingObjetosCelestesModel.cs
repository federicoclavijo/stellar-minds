using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class RankingObjetosCelestesModel
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        [Display(Name = "Cantidad de veces que fue observado")]
        public int CantidadObservaciones { get; set; }
    }
}
