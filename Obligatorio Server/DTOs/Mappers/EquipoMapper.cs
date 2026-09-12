using DTOs.DTOs;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mappers
{
    public static class EquipoMapper
    {
        public static EquipoDTO ToDto(Equipo equipo)
        {
            if (equipo == null) return null!;
            return equipo switch
            {
                Telescopio t => new TelescopioDTO
                {
                    Id = t.Id,
                    Marca = t.Marca,
                    Modelo = t.Modelo,
                    Stock = t.Stock,
                    Apertura = t.Apertura,
                    DistanciaFocal = t.DistanciaFocal,
                    RelacionFocal = t.RelacionFocal,
                    Peso = t.Peso
                },

                Montura m => new MonturaDTO
                {
                    Id = m.Id,
                    Marca = m.Marca,
                    Modelo = m.Modelo,
                    Stock = m.Stock,
                    TipoMontura = m.TipoMontura.ToString(),
                    CargaMax = m.CargaMax,
                    GoTo = m.GoTo
                },

                Camara c => new CamaraDTO
                {
                    Id = c.Id,
                    Marca = c.Marca,
                    Modelo = c.Modelo,
                    Stock = c.Stock,
                    Sensor = c.Sensor.ToString(),
                    Resolucion = c.Resolucion,
                    Tamano = c.Tamano
                },

                Ocular o => new OcularDTO
                {
                    Id = o.Id,
                    Marca = o.Marca,
                    Modelo = o.Modelo,
                    Stock = o.Stock,
                    Diametro = o.Diametro,
                    Angulo = o.Angulo
                },

                _ => throw new ArgumentException($"Tipo de entidad desconocido: {equipo.GetType().Name}")
            };
        }

        public static EquipoPlanoDTO ToDtoPlano(Equipo equipo)
        {
            if (equipo == null) return null!;

            return equipo switch
            {
                Telescopio t => new EquipoPlanoDTO
                {
                    Id = t.Id,
                    Marca = t.Marca,
                    Modelo = t.Modelo,
                    Stock = t.Stock,
                    TipoEquipo = "Telescopio",

                    Apertura = t.Apertura,
                    DistanciaFocal = t.DistanciaFocal,
                    RelacionFocal = t.RelacionFocal?.ToString(),
                    Peso = t.Peso,
                },

                Montura m => new EquipoPlanoDTO
                {
                    Id = m.Id,
                    Marca = m.Marca,
                    Modelo = m.Modelo,
                    Stock = m.Stock,
                    TipoEquipo = "Montura",

                    TipoMontura = m.TipoMontura.ToString(),
                    CargaMax = m.CargaMax,
                    GoTo = m.GoTo
                },

                Camara c => new EquipoPlanoDTO
                {
                    Id = c.Id,
                    Marca = c.Marca,
                    Modelo = c.Modelo,
                    Stock = c.Stock,
                    TipoEquipo = "Camara",

                    Sensor = c.Sensor.ToString(),
                    Resolucion = c.Resolucion,
                    Tamano = c.Tamano
                },

                Ocular o => new EquipoPlanoDTO
                {
                    Id = o.Id,
                    Marca = o.Marca,
                    Modelo = o.Modelo,
                    Stock = o.Stock,
                    TipoEquipo = "Ocular",

                    Diametro = o.Diametro,
                    Angulo = o.Angulo
                },

                _ => throw new ArgumentException($"Tipo de entidad desconocido: {equipo.GetType().Name}")
            };
        }

        public static Equipo FromDtoPlano(EquipoPlanoDTO dto)
        {
            return dto.TipoEquipo switch
            {
                "Telescopio" => new Telescopio
                {
                    Id = dto.Id,
                    Marca = dto.Marca,
                    Modelo = dto.Modelo,
                    Stock = dto.Stock,
                    Apertura = (double)dto.Apertura,
                    DistanciaFocal = (double)dto.DistanciaFocal,
                    RelacionFocal = dto.RelacionFocal,
                    Peso = (double)dto.Peso
                },

                "Montura" => new Montura
                {
                    Id = dto.Id,
                    Marca = dto.Marca,
                    Modelo = dto.Modelo,
                    Stock = dto.Stock,
                    TipoMontura = Enum.Parse<TipoMontura>(dto.TipoMontura),
                    CargaMax = (double)dto.CargaMax,
                    GoTo = (bool)dto.GoTo
                },

                "Camara" => new Camara
                {
                    Id = dto.Id,
                    Marca = dto.Marca,
                    Modelo = dto.Modelo,
                    Stock = dto.Stock,
                    Sensor = Enum.Parse<Sensor>(dto.Sensor),
                    Resolucion = dto.Resolucion,
                    Tamano = (double)dto.Tamano
                },

                "Ocular" => new Ocular
                {
                    Id = dto.Id,
                    Marca = dto.Marca,
                    Modelo = dto.Modelo,
                    Stock = dto.Stock,
                    Diametro = (double)dto.Diametro,
                    Angulo = (double)dto.Angulo
                },

                _ => throw new ArgumentException("Tipo inválido")
            };
        }

    }
}
