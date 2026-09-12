using Azure;
using DTOs.DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Observacion;
using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using static DTOs.DTOs.RespuestaEvaluacionDTO;

namespace LogicaAplicacion.CasosDeUso.Observacion
{
    public class EvaluarObservacionCU : IEvaluarObservacion
    {

        private IRepositorioPrestamo _repoP;
        private IRepositorioObjetoCeleste _repoO;
        private IConsultaIA _consultaIA;

        public EvaluarObservacionCU(IRepositorioPrestamo repoP, IRepositorioObjetoCeleste repoO, IConsultaIA consultaIA)
        {
            _repoP = repoP;
            _repoO = repoO;
            _consultaIA = consultaIA;
        }

        public EvaluacionDTO Ejecutar(ConsultaObservacionDTO dto)
        {
            LogicaNegocio.Entidades.Prestamo? prestamo =
            _repoP.FindById(dto.PrestamoId);

            LogicaNegocio.Entidades.ObjetoCeleste? objeto =
           _repoO.FindById(dto.ObjetoCelesteId);

            if (dto.FechaObservacion < DateTime.Now)
            {
                throw new ConsultaException("La fecha de observación no puede ser anterior a la fecha actual.");
            }

            if (dto.FechaObservacion < prestamo.FechaInicio || dto.FechaObservacion > prestamo.FechaFin)
            {
                throw new ConsultaException("La fecha de observación debe estar dentro del período del préstamo.");
            }

            if (prestamo.Estado == EstadoPrestamo.EN_PRESTAMO && prestamo.FechaFin < DateTime.Now)
            {
                throw new ConsultaException("El préstamo no está vigente");
            }

            if (prestamo == null)
            {
                throw new PrestamoException("No existe el préstamo seleccionado.");
            }

            if (objeto == null)
            {
                throw new ObjetoCelesteException("No existe el objeto celeste seleccionado.");
            }

            string prompt = $"""
                        Eres un experto en astronomía observacional y equipamiento astronómico.

            Debes evaluar si el equipamiento incluido en el préstamo es adecuado para observar el objeto celeste indicado. Si el préstamo incluye un ocular, evalúa si es adecuado para observación visual. Si incluye una cámara, evalúa si es adecuado para astrofotografía.

            Analiza las características del telescopio, montura, cámara y ocular (si existen), así como el tipo de objeto celeste.

            Debes responder ÚNICAMENTE un JSON válido, sin texto adicional, sin markdown y sin explicaciones fuera del JSON.

            El campo "indicador" solamente puede tener uno de estos tres valores exactos:

            * IDEAL
            * ADECUADO
            * NO_RECOMENDABLE

            Criterios:

            * IDEAL: el equipamiento es especialmente apropiado para observar el objeto celeste.
            * ADECUADO: el equipamiento permite observar el objeto razonablemente bien.
            * NO_RECOMENDABLE: el equipamiento no es apropiado o producirá resultados deficientes.

            La respuesta debe tener exactamente este formato:

            "Indicador": "IDEAL | ADECUADO | NO_RECOMENDABLE",
            "Detalle": "Explicación breve de máximo 300 caracteres"

            Datos a evaluar:

            Telescopio:

            * Marca: {prestamo.Telescopio.Marca}
            * Modelo: {prestamo.Telescopio.Modelo}
            * Apertura(mm): {prestamo.Telescopio.Apertura}
            * Distancia focal(mm): {prestamo.Telescopio.DistanciaFocal}
            * Relación focal: {prestamo.Telescopio.RelacionFocal}

            Montura:

            * Tipo: {prestamo.Montura.TipoMontura.ToString()}
            * Capacidad de carga(kg): {prestamo.Montura.CargaMax}

            Cámara:

            * Sensor: {(prestamo.Camara != null ? prestamo.Camara.Sensor.ToString() : "No posee")}
            * Resolución MP: {(prestamo.Camara != null ? prestamo.Camara.Resolucion : "No posee")}

            Ocular:

            * Ángulo: {(prestamo.Ocular != null ? prestamo.Ocular.Angulo : "No posee")}
            * Diámetro: {(prestamo.Ocular != null ? prestamo.Ocular.Diametro : "No posee")}

            Objeto celeste:

            * Nombre: {objeto.Nombre.ToString()}
            * Tipo: {objeto.Tipo.ToString()}
            * Magnitud: {objeto.Magnitud}

            Devuelve solamente el JSON.
            
            """;

            ConsultaDTO consulta = new ConsultaDTO
            {
                contents = [new ContentsDTO { parts = [new PartsDTO { text = prompt }] }]
            };
            string respuestaIA = _consultaIA.Ejecutar(consulta);
            RespuestaEvaluacionDTO respuestaGemini =
            JsonSerializer.Deserialize<RespuestaEvaluacionDTO>(
            respuestaIA);
            string texto = respuestaGemini.candidates[0].content.parts[0].text;

            EvaluacionDTO evaluacion = JsonSerializer.Deserialize<EvaluacionDTO>(
            texto);
            if (evaluacion.Indicador != "IDEAL" && evaluacion.Indicador != "ADECUADO" && evaluacion.Indicador != "NO_RECOMENDABLE")
                throw new ConsultaException("Gemini devolvió un valor inesperado. Reintente.");

            return evaluacion;
        }
    }
}
