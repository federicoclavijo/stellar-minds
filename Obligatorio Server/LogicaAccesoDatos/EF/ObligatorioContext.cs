using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class ObligatorioContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Camara> Camaras { get; set; }
        public DbSet<Montura> Monturas { get; set; }
        public DbSet<ObjetoCeleste> ObjetosCelestes { get; set; }
        public DbSet<Observacion> Observaciones { get; set; }
        public DbSet<Ocular> Oculares { get; set; }
        public DbSet<Telescopio> Telescopios { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }

        public ObligatorioContext(DbContextOptions optionsBuilder) : base(optionsBuilder)
        {
        }

        public ObligatorioContext()
        {
        }

    }
}
