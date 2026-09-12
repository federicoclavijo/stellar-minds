using DTOs.DTOs;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using LogicaNegocio.ValueObjects;
using System;

namespace DTOs.Mappers
{
    public static class UsuarioMapper
    {
        public static UsuarioDTO ToDTO(Usuario usuario)
        {
            if (usuario == null) return null!;
            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Telefono = usuario.Telefono,
                Email = usuario.Email.Email,
                Direccion = usuario.Direccion,
                Rol = usuario.Rol.ToString(),
                User = usuario.User,
                Password = usuario.Password.Password
            };
        }

        public static Usuario FromDto(UsuarioDTO dto)
        {
            if (dto == null) return null!;
            return new Usuario
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono,
                Email = new UsuarioEmail(dto.Email),
                Direccion = dto.Direccion,
                Rol = Enum.Parse<Rol>(dto.Rol),
                User = dto.User,
                Password = new UsuarioPassword(dto.Password)
            };
        }
    }
}
