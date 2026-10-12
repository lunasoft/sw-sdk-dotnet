using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SW.Helpers
{
    /// <summary>
    /// Lee el cuerpo de una respuesta HTTP conservando lo recibido si se interrumpe.
    /// </summary>
    internal static class HttpBodyReader
    {
        /// <summary>
        /// Tiempo maximo sin recibir datos al leer el cuerpo.
        /// </summary>
        internal static TimeSpan IdleTimeout = TimeSpan.FromSeconds(100);

        /// <summary>
        /// Lee los bytes de la respuesta HttpClient; si falla o llega incompleta, <paramref name="error"/> indica la causa.
        /// </summary>
        internal static byte[] ReadAll(HttpResponseMessage response, out Exception error)
        {
            MemoryStream bytes = new MemoryStream();
            error = null;
            try
            {
                var stream = response.Content.ReadAsStreamAsync().Result;
                byte[] buffer = new byte[4096];
                while (true)
                {
                    Task<int> task = stream.ReadAsync(buffer, 0, buffer.Length);
                    if (!task.Wait(IdleTimeout))
                    {
                        // La cancelacion no interrumpe la lectura pendiente: se libera la respuesta.
                        response.Dispose();
                        task.ContinueWith(t => { var ignorada = t.Exception; }, TaskContinuationOptions.OnlyOnFaulted);
                        error = new TimeoutException("Se agoto el tiempo de espera (sin actividad) al leer el cuerpo de la respuesta.");
                        break;
                    }
                    int read = task.Result;
                    if (read <= 0)
                        break;
                    bytes.Write(buffer, 0, read);
                }
                if (error == null)
                {
                    var expected = response.Content.Headers.ContentLength;
                    if (expected.HasValue && bytes.Length < expected.Value)
                        error = new IOException(string.Format("Respuesta incompleta: se anunciaron {0} bytes y se recibieron {1}.", expected.Value, bytes.Length));
                }
            }
            catch (Exception ex)
            {
                error = Unwrap(ex);
            }
            return Decompress(bytes.ToArray(), string.Join(",", System.Linq.Enumerable.ToArray(response.Content.Headers.ContentEncoding)));
        }
        /// <summary>
        /// Lee los bytes de la respuesta HttpWebRequest; si falla o llega incompleta, <paramref name="error"/> indica la causa.
        /// </summary>
        internal static byte[] ReadAll(HttpWebResponse response, out Exception error)
        {
            MemoryStream bytes = new MemoryStream();
            error = null;
            try
            {
                using (var responseStream = response.GetResponseStream())
                {
                    byte[] buffer = new byte[4096];
                    int read;
                    while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        bytes.Write(buffer, 0, read);
                }
                // Se compara con la longitud anunciada para detectar cortes.
                if (response.ContentLength >= 0 && bytes.Length < response.ContentLength)
                    error = new IOException(string.Format("Respuesta incompleta: se anunciaron {0} bytes y se recibieron {1}.", response.ContentLength, bytes.Length));
            }
            catch (Exception ex)
            {
                error = ex;
            }
            return Decompress(bytes.ToArray(), response.ContentEncoding);
        }
        /// <summary>
        /// Descomprime gzip/deflate si llega comprimido; si falla, conserva los bytes recibidos.
        /// </summary>
        internal static byte[] Decompress(byte[] data, string contentEncoding)
        {
            if (data == null || data.Length == 0 || string.IsNullOrEmpty(contentEncoding))
                return data;
            string encoding = contentEncoding.Trim().ToLowerInvariant();
            try
            {
                using (var input = new MemoryStream(data))
                using (System.IO.Stream zip = encoding == "gzip" || encoding == "x-gzip"
                    ? (System.IO.Stream)new System.IO.Compression.GZipStream(input, System.IO.Compression.CompressionMode.Decompress)
                    : encoding == "deflate" ? new System.IO.Compression.DeflateStream(input, System.IO.Compression.CompressionMode.Decompress) : null)
                {
                    if (zip == null)
                        return data;
                    var output = new MemoryStream();
                    byte[] buffer = new byte[4096];
                    int read;
                    while ((read = zip.Read(buffer, 0, buffer.Length)) > 0)
                        output.Write(buffer, 0, read);
                    return output.ToArray();
                }
            }
            catch (Exception)
            {
                return data;
            }
        }
        /// <summary>
        /// Charset declarado explicitamente en el Content-Type.
        /// </summary>
        internal static string ExplicitCharset(string contentType)
        {
            if (string.IsNullOrEmpty(contentType))
                return null;
            int index = contentType.IndexOf("charset=", StringComparison.OrdinalIgnoreCase);
            if (index < 0)
                return null;
            string charset = contentType.Substring(index + "charset=".Length).Split(';')[0].Trim().Trim('"');
            return charset.Length == 0 ? null : charset;
        }
        internal static string Decode(byte[] data, string charset)
        {
            Encoding encoding = Encoding.UTF8;
            if (!string.IsNullOrEmpty(charset))
            {
                try
                {
                    encoding = Encoding.GetEncoding(charset.Trim('"'));
                }
                catch (ArgumentException)
                {
                }
            }
            using (StreamReader reader = new StreamReader(new MemoryStream(data), encoding, true))
                return reader.ReadToEnd();
        }
        internal static Exception Unwrap(Exception ex)
        {
            var agg = ex as AggregateException;
            if (agg == null)
                return ex;
            var flat = agg.Flatten();
            return flat.InnerExceptions.Count > 0 ? flat.InnerExceptions[0] : ex;
        }
    }
}
