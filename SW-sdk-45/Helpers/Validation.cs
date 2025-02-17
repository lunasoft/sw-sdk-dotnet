﻿using System;
using System.Net;
using System.Net.Mail;

namespace SW.Helpers
{
    public class Validation
    {
        private string _url;
        private string _urlApi;
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
        public Validation(string url, string urlApi, string user, string password, string token)
        {
            _url = url;
            _urlApi = urlApi;
            _user = user;
            _password = password;
            _token = token;
            ValidateHeaderParameters();
        }
        public void ValidateHeaderParameters()
        {
            if (string.IsNullOrEmpty(_url) || _url == "/")
                throw new ServicesException("Falta Capturar URL");

            if (_urlApi == "/")
                throw new ServicesException("Falta Capturar URL Api");

            if (string.IsNullOrEmpty(_token))
            {
                if (string.IsNullOrEmpty(_user) && string.IsNullOrEmpty(_password))
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
        internal static bool ValidateEmail(string[] emails)
        {
            try
            {
                foreach (string email in emails)
                {
                    new MailAddress(email);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        internal static void ValidateCustomId(string customId)
        {
            if (customId.Length > 100)
            {
                throw new ServicesException("El CustomId no es válido o es mayor a 100 caracteres.");
            }
            else if (customId.Length <= 0)
            {
                throw new ServicesException("El CustomId viene vacío.");
            }
        }
        internal static void ValidateFormatEmail(string[] emails)
        {
            try
            {
                if (!Validation.ValidateEmail(emails) || emails.Length > 5)
                {
                    throw new Exception("El listado contiene más de 5 correos o el formato es incorrecto.");
                }
                else
                {
                    for (int i = 0; i < emails.Length; i++)
                    {
                        emails[i] = emails[i].Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
