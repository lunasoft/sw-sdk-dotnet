using System;
using System.Threading.Tasks;

namespace SW.Helpers
{
    public static class RetryHelper
    {
        public static T Retry<T>(Func<T> action, int maxRetries, int retryIntervalSeconds = 1) where T : class, new()
        {
            //Objeto generico para encapsular respuesta de GetResponse
            T lastResponse = null;

            for (int numRetries = 0; numRetries < maxRetries; numRetries++)
            {
                try
                {
                    lastResponse = action();
                    var responseStatus = lastResponse.GetType().GetProperty("status")?.GetValue(lastResponse, null)?.ToString();

                    if (responseStatus == "500")
                    {
                        //Si es un error 500 timeout, hacer el reintento
                        numRetries = numRetries + 1 / maxRetries;
                    }
                    else
                    {
                        return lastResponse;
                    }
                }
                catch (Exception ex)
                {
                   //Devolver cualquier otra excepción no manejada
                   throw new Exception($"Error: {ex.Message}", ex);
                }

                Task.Delay(TimeSpan.FromSeconds(retryIntervalSeconds)).Wait();
            }
            //Si se alcanzan todos los reintentos, devolver la última respuesta
            return lastResponse;
        }


    }
}
