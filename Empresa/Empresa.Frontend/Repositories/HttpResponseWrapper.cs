using System.Net;

namespace Empresa.Frontend.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? Response, bool error, HttpResponseMessage httpresponsem)
        {
            this.Response = Response;
            Error = error;
            Httpresponsem = httpresponsem;
        }

        public T? Response { get; }
        public bool Error { get; }
        public HttpResponseMessage Httpresponsem { get; }

        public async Task<string?> GetErrorMessageAsync()
        {
            if (!Error)
            {
                return null;
            }

            var statusCode = Httpresponsem.StatusCode;
            if (statusCode == HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado.";
            }
            if (statusCode == HttpStatusCode.BadRequest)
            {
                return await Httpresponsem.Content.ReadAsStringAsync();
            }
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                return "Tienes que estar logueado para ejecutar esta operación.";
            }
            if (statusCode == HttpStatusCode.Forbidden)
            {
                return "No tienes permisos para hacer esta operación.";
            }

            return "Ha ocurrido un error inesperado.";
        }
    }
}
