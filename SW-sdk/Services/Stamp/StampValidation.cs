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
                    ValidateStructure();
                }else
                {
                    throw new ServicesException("Tu XML esta vacio");
                }
            }
            catch (XmlException ex)
            {
                throw new ServicesException("No es un XML Valido");
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
        private void ValidateStructure()
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(_xmlString)))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            while (reader.MoveToNextAttribute())
                            {
                                switch (reader.NodeType)
                                {
                                    case XmlNodeType.Attribute:
                                        ValidateSpecialCharacters(reader.Value.ToString());
                                        break;
                                }
                            }
                            break;
                    }
                }
            }
        }
        private void ValidateSpecialCharacters(string value)
        {
            Regex regex = new Regex("[&'<\">]");
            Match match = regex.Match(value);
            if (match.Success)
            {
                throw new ServicesException("Tu XML tiene caracteres que no estan codifcados correctamente en UTF-8 "+ value);
            }
        }
    }
}