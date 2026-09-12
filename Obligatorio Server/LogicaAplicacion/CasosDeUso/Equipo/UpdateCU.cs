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
    public class UpdateCU : IUpdate
    {
        private IRepositorioEquipo _repo;
        public UpdateCU(IRepositorioEquipo repo)
        {
            _repo = repo;
        }

        public void Ejecutar(EquipoPlanoDTO dto)
        {
            if (dto == null) throw new EquipoException("Los datos del equipo no pueden ser nulos.");

            LogicaNegocio.Entidades.Equipo? equipoExistente = _repo.FindById(dto.Id);
            LogicaNegocio.Entidades.Equipo equipoDTO = EquipoMapper.FromDtoPlano(dto);
            if (equipoExistente == null) throw new EquipoException("El equipo a modificar no existe.");
            equipoDTO.Validar();

            equipoExistente.Marca = equipoDTO.Marca;
            equipoExistente.Modelo = equipoDTO.Modelo;
            equipoExistente.Stock = equipoDTO.Stock;

            if (equipoExistente is Telescopio telescopio && equipoDTO is Telescopio telescopioDTO)
            {
                telescopio.Apertura = telescopioDTO.Apertura;
                telescopio.RelacionFocal = telescopioDTO.RelacionFocal;
                telescopio.DistanciaFocal = telescopioDTO.DistanciaFocal;
                telescopio.Peso = telescopioDTO.Peso;
            }
            else if (equipoExistente is Montura montura && equipoDTO is Montura monturaDTO)
            {
                montura.TipoMontura = monturaDTO.TipoMontura;
                montura.CargaMax = monturaDTO.CargaMax;
                montura.GoTo = monturaDTO.GoTo;
            }
            else if (equipoExistente is Camara camara && equipoDTO is Camara camaraDTO)
            {
                camara.Sensor = camaraDTO.Sensor;
                camara.Resolucion = camaraDTO.Resolucion;
                camara.Tamano = camaraDTO.Tamano;
            }
            else if (equipoExistente is Ocular ocular && equipoDTO is Ocular ocularDTO)
            {
                ocular.Diametro = ocularDTO.Diametro;
                ocular.Angulo = ocularDTO.Angulo;
            }
            else
            {
                throw new EquipoException("El tipo de equipo enviado no coincide.");
            }

            _repo.Update(equipoExistente);
        }
    }
}
