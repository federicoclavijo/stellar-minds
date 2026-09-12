using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mappers
{
    public static class ObjetoCelesteMapper
    {
        public static ObjetoCelesteDTO ToDto (ObjetoCeleste entidad)
        {
            return new ObjetoCelesteDTO
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre.ToString(),
                Tipo = entidad.Tipo.ToString(),
                Magnitud = entidad.Magnitud
            };
        }

        public static ObjetoCeleste FromDto (ObjetoCelesteDTO dto)
        {
            return new ObjetoCeleste
            {
                Id = dto.Id,
                Nombre = Enum.Parse<NombreObjetoCeleste>(dto.Nombre),
                Tipo = Enum.Parse<TipoObjetoCeleste>(dto.Tipo),
                Magnitud = dto.Magnitud
            };
        }
    }
}
