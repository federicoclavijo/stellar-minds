using Newtonsoft.Json;
using System.Text;
using WebApp.Enums;

namespace WebApp.Auxiliar
{
    public class ClienteHttpAuxiliar
    {
        public static HttpResponseMessage EnviarSolicitud(
           string url, VerbosHttp verbo, object obj = null, string token = null)
        {
            HttpClient cliente = new HttpClient();
            Task<HttpResponseMessage> tarea = null;

            if (!string.IsNullOrWhiteSpace(token))
            {
                cliente.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }

            StringContent body = null;
            if (obj != null)
            {
                string stringJson = JsonConvert.SerializeObject(obj);
                body = new StringContent(stringJson, Encoding.UTF8, "application/json");
            }

            switch (verbo)
            {
                case VerbosHttp.GET:
                    tarea = cliente.GetAsync(url);
                    break;
                case VerbosHttp.POST:
                    tarea = cliente.PostAsJsonAsync(url, obj);
                    break;
                case VerbosHttp.PUT:
                    tarea = cliente.PutAsJsonAsync(url, obj);
                    break;
                case VerbosHttp.DELETE:
                    tarea = cliente.DeleteAsync(url);
                    break;
                default:
                    throw new Exception("VERBO NO VALIDO");
                    break;
            }
            tarea.Wait();
            return tarea.Result;
        }

        public static string ObtenerBody(HttpResponseMessage respuesta)
        {
            HttpContent body = respuesta.Content;
            Task<string> tarea = body.ReadAsStringAsync();
            tarea.Wait();
            return tarea.Result;
        }
    }
}
