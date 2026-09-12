using DTOs.DTOs;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mappers
{
    public static class ObservacionMapper
    {
        public static ObservacionDTO ToDto (Observacion entidad)
        {
            return new ObservacionDTO
            {
                Id = entidad.Id,
                FechaObservacion = entidad.Fecha,
                ObjetoCelesteId = entidad.ObjetoCelesteId,
                ObjetoCeleste = ObjetoCelesteMapper.ToDto(entidad.ObjetoCeleste),
                PrestamoId = entidad.PrestamoId,
                Prestamo = PrestamoMapper.ToDto(entidad.Prestamo),
                Indicador = entidad.Evaluacion.Indicador.ToString(),
                Detalle = entidad.Evaluacion.Detalle
            };
        }

        public static Observacion FromDto (ObservacionDTO dto)
        {
            return new Observacion
            {
                Id = dto.Id,
                Fecha = dto.FechaObservacion,
                ObjetoCelesteId = dto.ObjetoCelesteId,
                PrestamoId = dto.PrestamoId,
                Evaluacion = new EvaluacionIA(Enum.Parse<Indicador>(dto.Indicador), dto.Detalle)
            };
        }
    }
}
