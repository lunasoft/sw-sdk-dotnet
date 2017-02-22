using System.Net;

namespace SW.Helpers
{
    public class Validation
    {
        private string _url;
        private string _user;
        private string _password;
        private string _token;
        public Validation()
        {
        }
        public Validation(string url, string user, string password, string token)
        {
            _url = url;
            _user = user;
            _password = password;
            _token = token;
            ValidateHeaderParameters();
        }
        public void ValidateHeaderParameters()
        {
            if (string.IsNullOrEmpty(_url))
            {
                throw new ServicesException("Falta Capturar URL");
            }else
            {
                if (_url == "demoApi")
                {
                    throw new TestEnviromentException();
                }
            } 
            if (string.IsNullOrEmpty(_token))
            {
                if(string.IsNullOrEmpty(_user) && string.IsNullOrEmpty(_password))
                {
                    throw new ServicesException("Falta Capturar Token");
                }
                if (string.IsNullOrEmpty(_user))
                {
                    throw new ServicesException("Falta Capturar Usuario");
                }
                if (string.IsNullOrEmpty(_password))
                {
                    throw new ServicesException("Falta Capturar Contraseña");
                }
            }
            else
            {
                ValidateToken(_token);
            }
        }
        private void ValidateToken(string token)
        {
            string[] validToken = token.Split('.');
            if (validToken.Length != 3)
            {
                throw new ServicesException("Token Mal Formado");
            }
        }
        public void ValidateResponseStatus(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.InternalServerError:
                    break;
                case HttpStatusCode.NotFound:
                    throw new ServicesException("Url Invalida");
                case HttpStatusCode.Forbidden:
                    throw new ServicesException("Token Expirado");
                default:
                    throw new ServicesException("Hubo un Error al procesar tu solicitud - " + statusCode.ToString());
            }
        }
    }
}
