using DTOs.DTOs;
using DTOs.Mappers;
using LogicaAplicacion.InterfacesCasosDeUso.Equipo;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Equipo
{
    public class DeleteEquipoCU : IDeleteEquipo
    {
        private IRepositorioEquipo _repoE;
        private readonly IRepositorioPrestamo _repoP;
        public DeleteEquipoCU (IRepositorioEquipo repoE, IRepositorioPrestamo repoP)
        {
            _repoE = repoE;
            _repoP = repoP;
        }
        public void Ejecutar(int id)
        {
            LogicaNegocio.Entidades.Equipo? equipo = _repoE.FindById(id);
            if (equipo == null)
            {
                throw new EquipoException("El equipo no se encontró.");
            }

            bool tieneActivos = _repoP.ObtenerPrestamosPorEquipo(id).Any(p => p.Estado != EstadoPrestamo.DEVUELTO);

            if (tieneActivos)
            {
                throw new EquipoException("El equipo está en prestamo.");
            }

            _repoE.Delete(id);
        }
    }
}
