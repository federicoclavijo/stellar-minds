using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs
{
    public class PrestamoDTO
    {
        public int Id { get; set; }

        // Datos del socio
        public int? UsuarioId { get; set; }
        public UsuarioDTO? Usuario { get; set; }

        // Fechas
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoPrestamo Estado { get; set; }

        // Equipos
        public int? TelescopioId { get; set; }
        public EquipoPlanoDTO? Telescopio { get; set; }

        public int? MonturaId { get; set; }
        public EquipoPlanoDTO? Montura { get; set; }

        public int? CamaraId { get; set; }
        public EquipoPlanoDTO? Camara { get; set; }

        public int? OcularId { get; set; }
        public EquipoPlanoDTO? Ocular { get; set; }
    }
}
