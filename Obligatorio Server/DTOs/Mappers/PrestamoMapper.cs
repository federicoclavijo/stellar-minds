using DTOs.DTOs;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DTOs.Mappers
{
    public static class PrestamoMapper
    {
        public static PrestamoDTO ToDto(Prestamo prestamo)
        {
            if (prestamo == null) return null;

            return new PrestamoDTO
            {
                Id = prestamo.Id,
                UsuarioId = prestamo.UsuarioId,
                Usuario = UsuarioMapper.ToDTO(prestamo.Usuario),

                FechaInicio = prestamo.FechaInicio,
                FechaFin = prestamo.FechaFin,
                Estado = prestamo.Estado,

                TelescopioId = prestamo.TelescopioId,
                Telescopio = EquipoMapper.ToDtoPlano(prestamo.Telescopio),

                MonturaId = prestamo.MonturaId,
                Montura = EquipoMapper.ToDtoPlano(prestamo.Montura),

                CamaraId = prestamo.CamaraId,
                Camara = EquipoMapper.ToDtoPlano(prestamo.Camara),

                OcularId = prestamo.OcularId,
                Ocular = EquipoMapper.ToDtoPlano(prestamo.Ocular),
            };
        }

        public static Prestamo FromDto(PrestamoDTO dto)
        {
            if (dto == null) return null;

            return new Prestamo
            {
                UsuarioId = dto.UsuarioId,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                TelescopioId = dto.TelescopioId,
                MonturaId = dto.MonturaId,
                CamaraId = dto.CamaraId,
                OcularId = dto.OcularId
            };
        }
    }
}
