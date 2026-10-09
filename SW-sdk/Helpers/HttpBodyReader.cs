using System;
using System.IO;
using System.Net;
using System.Text;

namespace SW.Helpers
{
    /// <summary>
    /// Lee el cuerpo de una respuesta HTTP conservando lo recibido si se interrumpe.
    /// </summary>
    internal static class HttpBodyReader
    {
        /// <summary>
        /// Lee los bytes de la respuesta; si falla o llega incompleta, <paramref name="error"/> indica la causa.
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
                    encoding = Encoding.GetEncoding(charset);
                }
                catch (ArgumentException)
                {
                }
            }
            using (StreamReader reader = new StreamReader(new MemoryStream(data), encoding, true))
                return reader.ReadToEnd();
        }
    }
}
