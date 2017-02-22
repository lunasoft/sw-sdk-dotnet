using System;
using SW.Helpers;

namespace SW.Services.Cancelation
{
    internal class CancelationValidation : Validation
    {
        public CancelationValidation(string url, string user, string password, string token) : base(url, user, password, token)
        {
        }
        public void ValidateRequest(string Certificado, string Key, string Contraseña, string[] uuids)
        {
            if(uuids.Length == 0)
            {
                throw new ServicesException("Faltan especificar los UUIDs a Cancelar");
            }
            if(string.IsNullOrEmpty(Certificado))
            {
                throw new ServicesException("Falta Capturar el Certificado");
            }else
            {
                ValidateIsBase64("Certificado",Certificado);
            }
            if (string.IsNullOrEmpty(Key))
            {
                throw new ServicesException("Falta Capturar Key del Certificado");
            }else
            {
                ValidateIsBase64("Key", Key);
            }
            if (string.IsNullOrEmpty(Contraseña))
            {
                throw new ServicesException("Falta Capturar Contraseña del Certificado");
            }else
            {
                ValidateIsBase64("Contraseña", Contraseña);
            }
        }
        private static void ValidateIsBase64(string key, string value)
        {
            try
            {
                Convert.FromBase64String(value);
            }
            catch (Exception ex)
            {
                throw new ServicesException("Tu " + key + " no es Base64");
            }
        }
    }
}