using System.IO;
using System.Text;
using System.Xml;
using System;
using System.Text.RegularExpressions;
using SW.Helpers;

namespace SW.Services.Stamp
{
    internal class StampValidation : Validation
    {
        private string _xmlString;
        public StampValidation(string url, string user, string password, string token) : base(url, user, password, token)
        {
        }

        public void ValidaXML(string XMLString)
        {
            this._xmlString = XMLString;
            Validations();
        }
        internal void ValidaXML(byte[] xmlString)
        {
            this._xmlString = Encoding.UTF8.GetString(xmlString);
            Validations();
        }
        private void Validations()
        {
            try
            {
                if (!string.IsNullOrEmpty(_xmlString))
                {
                    ValidateEncoding();
                }else
                {
                    throw new ServicesException("Tu XML esta vacio");
                }
            }
            catch (XmlException ex)
            {
                throw new ServicesException("No es un XML Valido "+ ex.Message);
            }
        }
        private void ValidateEncoding()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(new MemoryStream(Encoding.UTF8.GetBytes(_xmlString)));
            }
            catch (Exception ex)
            {
                throw new ServicesException("Tu XML no tiene codificacion UTF-8");
            }
        }       
    }
}