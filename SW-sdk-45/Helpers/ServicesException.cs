using System;
using System.Runtime.Serialization;

namespace SW.Helpers
{
    [Serializable]
    internal class ServicesException : Exception
    {
        public ServicesException()
        {
        }

        public ServicesException(string message) : base(message)
        {
        }

        public ServicesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServicesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    // Conserva el diagnostico de autenticacion para la operacion dependiente.
    public sealed class AuthenticationResponseException : Exception
    {
        /// <summary>
        /// Respuesta completa de la autenticacion.
        /// </summary>
        public SW.Services.Authentication.AuthResponse Response { get; private set; }

        /// <summary>
        /// Mensaje original de SW.
        /// </summary>
        public override string Message
        {
            get { return Response.message; }
        }

        internal AuthenticationResponseException(SW.Services.Authentication.AuthResponse response)
            : base(response == null ? "No se recibio una respuesta de autenticacion." : response.message)
        {
            Response = response ?? new SW.Services.Authentication.AuthResponse
            {
                status = "error",
                message = "No se recibio una respuesta de autenticacion.",
                messageDetail = ""
            };
        }
    }
}