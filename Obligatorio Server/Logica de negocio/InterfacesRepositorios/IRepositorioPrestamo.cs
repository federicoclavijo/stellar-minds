using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioPrestamo : IRepositorio<Prestamo>
    {

        public void Add(Prestamo aAgregar);
        IEnumerable<Prestamo> ObtenerEntreFechas(int mes, int anio, int usuarioId);
        public IEnumerable<Usuario> ObtenerSociosXTelescopio(int telescopioId);
        public IEnumerable<Prestamo> ObtenerPrestamosPorEquipo(int equipoId);
        public IEnumerable<Prestamo> ObtenerPrestamosActivosXSocio(int id);
        public IEnumerable<Prestamo> ObtenerPrestamosXCoordinador(int coordinadorId);
        public IEnumerable<Prestamo> ObtenerPrestamosVigentesXSocio(int id);

    }
}
