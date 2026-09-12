using DTOs.DTOs;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Mappers
{
    public class AuditoriaMapper
    {
        public static AuditoriaDTO ToDTO(Auditoria auditoria)
        {
            if (auditoria == null) return null!;
            return new AuditoriaDTO
            {
                Id = auditoria.Id,
                PrestamoId = auditoria.PrestamoId,
                Prestamo = PrestamoMapper.ToDto(auditoria.Prestamo),
                UsuarioId = auditoria.UsuarioId,
                Usuario = UsuarioMapper.ToDTO(auditoria.Usuario),
                Accion = auditoria.Accion.ToString(),
                Fecha = auditoria.Fecha
            };
        }

        public static Auditoria FromDto(AuditoriaDTO dto)
        {
            if (dto == null) return null!;
            return new Auditoria
            {
                Id = dto.Id,
                PrestamoId = dto.PrestamoId,
                Prestamo = PrestamoMapper.FromDto(dto.Prestamo),
                UsuarioId = dto.UsuarioId,
                Usuario = UsuarioMapper.FromDto(dto.Usuario),
                Accion = Enum.Parse<TipoAccion>(dto.Accion),
                Fecha = dto.Fecha
            };
        }
    }
}
