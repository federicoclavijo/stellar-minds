using Azure;
using DTOs.DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Observacion;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace LogicaAplicacion.CasosDeUso.Observacion
{
    public class ConsultaIA : IConsultaIA
    {
        public string Ejecutar(ConsultaDTO consulta)
        {
            HttpClient cliente = new HttpClient();
            Task<HttpResponseMessage> tarea = null;

            //Modificar este string por la apikey que usted haya generado
            string apiKey = Environment.GetEnvironmentVariable("GOOGLE_AI_API_KEY");

            string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

            tarea = cliente.PostAsJsonAsync(url, consulta);
            tarea.Wait();

            if (tarea.Result.StatusCode == HttpStatusCode.TooManyRequests)
            {
                throw new MuchasPeticionesException(
                    "Se alcanzó el límite de consultas de IA.");
            }

            if (tarea.Result.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ServicioNoDisponibleException(
                    "El servicio de IA se encuentra temporalmente saturado. Intente nuevamente en unos segundos.");
            }

            HttpResponseMessage respuesta = tarea.Result;
            HttpContent body = respuesta.Content;

            Task<string> respuestaString = body.ReadAsStringAsync();
            respuestaString.Wait();

            return respuestaString.Result;
        }
    }
}
