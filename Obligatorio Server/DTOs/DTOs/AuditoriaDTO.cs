using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTOs
{
    public class AuditoriaDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioDTO? Usuario { get; set; }
        public int PrestamoId { get; set; }
        public PrestamoDTO? Prestamo { get; set; }
        public string Accion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }
}
