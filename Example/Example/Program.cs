using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Services.Authentication;
using SW.Services.Stamp;
using System.IO;
namespace Example
{
    class Programa
    {
        private static string _url = "http://services.test.sw.com.mx";
        private static string _user = "demo";
        private static string _password = "123456789";
        private static string _pathXmlFile= Directory.GetCurrentDirectory()+ "\\..\\..\\..\\Files\\" + "XmlExample.xml";
        static void Main(string[] args)
        {
            Console.WriteLine("Ejemplo de timbrado y autenticación de los servicios REST de CFDI 3.3 en C#");
            Console.WriteLine("La ruta del archivo para timbrar es: "+_pathXmlFile);
            string xmlInfo = GetXmlFile(_pathXmlFile);
            string option=string.Empty;
            while (option != "s")
            {
                Console.WriteLine("Selecciona una opcipon.\n1- Timbrar con versión V1\n2- Timbrar con versión V2\n3- Timbrar con versión V3\n4- Timbrar con versión V4\n5- Autenticación\n6- Cambiar de Formato de timbrado (XML/B64)\ns- Salir");
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        TimbrarV1(xmlInfo);
                        break;
                    case "2":
                        TimbrarV2(xmlInfo);
                        break;
                    case "3":
                        TimbrarV3(xmlInfo);
                        break;
                    case "4":
                        TimbrarV4(xmlInfo);
                        break;
                    case "5":
                        Autenticacion();
                        break;
                    case "6":
                        xmlInfo=GetXmlFile(_pathXmlFile);
                        break;
                    case "s":
                        Console.WriteLine("Aplicación finalizada.....");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                Console.WriteLine("Preciona una tecla para continuar.");
                Console.ReadKey();
                Console.Clear();
            }

        }
        private static void Autenticacion()
        {
            Console.WriteLine("Autenticación");
            Authentication swAutentication = new Authentication(_url, _user, _password);
            AuthResponse authResponse = swAutentication.GetToken();
            if (authResponse.status == "success")
            {
                Console.WriteLine("Se ha autenticado de manera correcta");
                Console.WriteLine("Token:" + authResponse.Data.token + "\n");
            }
            else
            {
                Console.WriteLine("Error en la autenticación");
                Console.WriteLine(authResponse.message+" : "+authResponse.messageDetail + "\n");
            }

            
        }
        private static void TimbrarV1(string xmlInfo)
        {
            Console.WriteLine("Timbrado V1");
            Stamp stamp = new Stamp(_url, _user, _password);
            StampResponseV1 stampResult;
            try
            {
                Convert.FromBase64String(xmlInfo);
                stampResult = stamp.TimbrarV1(xmlInfo,true);
            }
            catch
            {
                stampResult = stamp.TimbrarV1(xmlInfo);
            }
            if(stampResult.status== "success")
            {
                Console.WriteLine("Respuesta del Timbrado\n\n");
                Console.WriteLine("CFDI+TFD:" + stampResult.data.tfd + "\n");
            }
            else
            {
                Console.WriteLine("Error al timbrar\n\n");
                Console.WriteLine(stampResult.message+" : "+stampResult.messageDetail + "\n");
            }
        }
        private static void TimbrarV2(string xmlInfo)
        {
            Console.WriteLine("Timbrado V2");
            Stamp stamp = new Stamp(_url, _user, _password);
            StampResponseV2 stampResult;
            try
            {
                Convert.FromBase64String(xmlInfo);
                stampResult = stamp.TimbrarV2(xmlInfo, true);
            }
            catch
            {
                stampResult = stamp.TimbrarV2(xmlInfo);
            }
            if (stampResult.status == "success")
            {
                Console.WriteLine("Respuesta del Timbrado\n\n");
                Console.WriteLine("TFD:"+stampResult.data.tfd + "\n");
                Console.WriteLine("CFDI:" + stampResult.data.cfdi + "\n");
            }
            else
            {
                Console.WriteLine("Error al timbrar\n\n");
                Console.WriteLine(stampResult.message + " : " + stampResult.messageDetail + "\n");
            }
        }
        private static void TimbrarV3(string xmlInfo)
        {
            Console.WriteLine("Timbrado V3");
            Stamp stamp = new Stamp(_url, _user, _password);
            StampResponseV3 stampResult;
            try
            {
                Convert.FromBase64String(xmlInfo);
                stampResult = stamp.TimbrarV3(xmlInfo, true);
            }
            catch
            {
                stampResult = stamp.TimbrarV3(xmlInfo);
            }
            if (stampResult.status == "success")
            {
                Console.WriteLine("Respuesta del Timbrado\n\n");
                Console.WriteLine("CFDI:" + stampResult.data.cfdi + "\n");
            }
            else
            {
                Console.WriteLine("Error al timbrar\n\n");
                Console.WriteLine(stampResult.message + " : " + stampResult.messageDetail + "\n");
            }
        }
        private static void TimbrarV4(string xmlInfo)
        {
            Console.WriteLine("Timbrado V4");
            Stamp stamp = new Stamp(_url, _user, _password);
            StampResponseV4 stampResult;
            try
            {
                Convert.FromBase64String(xmlInfo);
                stampResult = stamp.TimbrarV4(xmlInfo, true);
            }
            catch
            {
                stampResult = stamp.TimbrarV4(xmlInfo);
            }
            if (stampResult.status == "success")
            {
                Console.WriteLine("Respuesta del Timbrado\n\n");
                Console.WriteLine("CFDI:" + stampResult.data.cfdi+"\n");
                Console.WriteLine("Cadena Original SAT:" + stampResult.data.cadenaOriginalSAT + "\n");
                Console.WriteLine("Fecha de Timbrado:" + stampResult.data.fechaTimbrado + "\n");
                Console.WriteLine("Número de Certificado CFDI:" + stampResult.data.noCertificadoCFDI + "\n");
                Console.WriteLine("Número de Certificado SAT:" + stampResult.data.noCertificadoSAT + "\n");
                Console.WriteLine("qrCode:" + stampResult.data.qrCode + "\n");
                Console.WriteLine("Sello CFDI:" + stampResult.data.selloCFDI + "\n");
                Console.WriteLine("Sello SAT:" + stampResult.data.selloSAT + "\n");
                Console.WriteLine("UUID:" + stampResult.data.uuid + "\n");
            }
            else
            {
                Console.WriteLine("Error al timbrar\n\n");
                Console.WriteLine(stampResult.message + " : " + stampResult.messageDetail + "\n");
            }
        }
        private static string GetXmlFile(string path)
        {
            Console.WriteLine("Seleccione el formato del timbrado.\n1- Formato XML\n2- Formato B64");
            string format = Console.ReadLine();
            bool b64 = false;
            switch (format)
            {
                case "1":
                    b64 = false;
                    break;
                case "2":
                    b64 = true;
                    break;
                default:
                    Console.WriteLine("Por defaut se usará el formato XML");
                    break;

            }
            string xmlText = string.Empty;
            try
            {
                xmlText = File.ReadAllText(path, Encoding.UTF8);
                return b64 ? System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(xmlText)) : xmlText;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la información del xml: "+ex.Message);
                throw;
            }
            
        }
    }
}
