﻿![NET](http://resources.workable.com/wp-content/uploads/2015/08/Microsoft-dotNET-logo.jpg)
![NET](https://smarterwebci.visualstudio.com/_apis/public/build/definitions/402b9165-314f-4f5f-8073-9ae3a2e962ef/23/badge)
##### Servicios de Timbrado para documentos CFDI a traves del Proveedor de Certificación de CFDI  **SW SmarterWeb**

# Compatibilidad #
* CFDI 4.0
* .Net Framework


# Dependencias #
* [RestSharp](http://restsharp.org/)
* [NewtonSoft](https://www.newtonsoft.com/json)

# Documentación #
* [Inicio Rápido](https://developers.sw.com.mx/knowledge-base/conoce-el-proceso-de-integracion-en-solo-7-pasos/)
* [Libreria dot-net](http://developers.sw.com.mx/article-categories/csharp/)
* [Documentacion Oficial Servicios](http://developers.sw.com.mx)
 
----------------
# Instalaci&oacute;n #

Instalar la librería para .Net Framework 4.5 (o superior), a traves de Package Manager Console [nuget.org](https://www.nuget.org/packages/SW-sdk-45)
```cs
Install-Package SW-sdk-45
```

Instalar la librería para .Net Framework 3.5, a traves Package Manager Console [nuget.org](https://www.nuget.org/packages/SW-sdk)
```cs
Install-Package SW-sdk
```

En caso de no utilizar Package Manager Console puedes descargar la librería directamente a traves del siguiente [link](https://github.com/lunasoft/sw-sdk-dotnet/releases) y agregarla como Referencia local a tu proyecto. Asegurate de utilizar la última versión publicada.

# Implementaci&oacute;n #
La librería contara con los servicios principales como lo son Timbrado de CFDI, Cancelación, Consulta estatus CFDI, etc.

## Autenticaci&oacute;n ##
El servicio de Autenticación es utilizado principalmente para obtener el **token** el cual será utilizado para poder timbrar nuestro CFDI (xml) ya emitido (sellado), para poder utilizar este servicio es necesario que cuente con un **usuario** y **contraseña** para posteriormente obtenga el token.


**Ejemplo de consumo de la librería para obtener token**
```cs
using SW.Services.Authentication;
using System;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Authentication 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                Authentication auth = new Authentication("http://services.test.sw.com.mx", "user", "password");
                AuthResponse response = auth.GetToken();
            }
            catch (Exception e)
            {

            }

        }
    }
}
```

## Timbrado ##

<details>
<summary>
Timbrado CFDI V1
</summary>

<br>El método **TimbrarV1** recibe el contenido de un **XML** ya emitido (sellado) en formato **String**  o tambien puede ser en **Base64**, posteriormente si la factura y el token son correctos devuelve el complemento timbre en un string (**TFD**), en caso contrario lanza una excepción.

Este método recibe los siguientes parámetros:
* Archivo en formato **String** o **Base64**
* Usuario y contraseña o Token
* Url Servicios SW

**Timbrar XML en formato string utilizando usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Stamp;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Stamp 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a timbrar el xml
                Stamp stamp = new Stamp("http://services.test.sw.com.mx", "user", "password");
                string xml = Encoding.UTF8.GetString(File.ReadAllBytes("file.xml"));
                StampResponseV1 response = stamp.TimbrarV1(xml);
                Console.WriteLine(response.status);
                Console.WriteLine(response.data.tfd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**Timbrar XML en formato string utilizando token** [¿Como obtener token?](http://developers.sw.com.mx/knowledge-base/generar-un-token-infinito/)
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Stamp;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Stamp 
                //A esta le pasamos la Url y su Token infinito 
                //Este lo puede obtener ingresando al administrador de timbres con su usuario y contraseña
                Stamp stamp = new Stamp("http://services.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                string xml = Encoding.UTF8.GetString(File.ReadAllBytes("file.xml"));
                StampResponseV1 response = stamp.TimbrarV1(xml);
                Console.WriteLine(response.status);
                Console.WriteLine(response.data.tfd);
            }
            catch (Exception e)
            {
            }
        }
    }
}
```
**Ejemplo de consumo de la librería para Timbrado XML en formato b64 utilizando usuario y contraseña**
```cs
using SW.Services.Stamp;
using System;
using System.IO;
using System.Text;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Stamp con parametros Url y credenciales de acceso
                Stamp stamp = new Stamp("http://services.test.sw.com.mx", "user", "password");
                //Colocamos el XML a timbrar en una variable
                var xml = Encoding.UTF8.GetString(File.ReadAllBytes("file.xml"));
                //Convertimos el XML a formato B64
                xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
                //Recibimos la respuesta enviando el XML en B64 al metodo TimbrarV1, acompañado del valor "true" indicando que enviamos un b64 **Nota: el tfd se recibe en formatob64
                var response = (StampResponseV1)stamp.TimbrarV1(xml, true);
                Console.WriteLine(response.status);
                Console.WriteLine(response.data.tfd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
<summary>
Emisión Timbrado V1
</summary>

<br>Emisión Timbrado realiza el sellado y timbrado de un comprobante CFDI 4.0. Recibe el contenido de un XML en formato String ó tambien puede ser en Base64, posteriormente si la factura y el token son correctos devuelve el complemento timbre en un string (TFD), en caso contrario lanza una excepción.

Este método recibe los siguientes parámetros:
* Archivo en formato **String** o **Base64**
* Usuario y contraseña o Token
* Url Servicios SW

**Ejemplo de consumo de la librería para la emisión Timbrado XML en formato string utilizando usuario y contraseña**
```cs
using SW.Services.Issue;
using System;
using System.IO;
using System.Text;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {//Instancia del servicio Issue, pasando como paremetros URL de servicios, usuario y contraseña como metodo de autenticación.
            Issue issue = new Issue("http://services.test.sw.com.mx", "user", "password");
            //El XML armado a sellar y timbrar como cadena
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("file.xml"));
            //Recibimos la respuesta enviando el XML al metodo TimbrarV1
            var response = issue.TimbrarV1(xml);
            Console.WriteLine(response.status);
            Console.WriteLine(response.data.tfd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**Emision timbrado con XML en formato string utilizando token** [¿Como obtener token?](http://developers.sw.com.mx/knowledge-base/generar-un-token-infinito/)
```cs
using System;
using System.IO;
using System.Text;
using SW.Services.Issue;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Issue con parametros Url y su Token infinito 
                Issue issue = new Issue ("http://services.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //El XML armado a sellar y timbrar como cadena
                string xml = Encoding.UTF8.GetString(File.ReadAllBytes("file.xml"));
                //Recibimos la respuesta enviando el XML al metodo TimbrarV1
                var response = issue.TimbrarV1(xml);
                Console.WriteLine(response.status);
                Console.WriteLine(response.data.tfd);
            }
            catch (Exception e)
            {
                 Console.WriteLine(e.Message);
            }
        }
    }
}
```
**Ejemplo de consumo de la librería para Emision Timbrado en formato b64 utilizando usuario y contraseña**
```cs
using SW.Services.Issue;
using System;
using System.IO;
using System.Text;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Issue con parametros Url y credenciales de acceso
                Issue issue = new Issue("http://services.test.sw.com.mx", "user", "password");
                //Colocamos el XML a timbrar en una variable
                var xml = Encoding.UTF8.GetString(File.ReadAllBytes("file.xml"));
                //Convertimos el XML a formato B64
                xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
                //Recibimos la respuesta enviando el XML en B64 al metodo TimbrarV1, acompañado del valor "true" indicando que enviamos un b64 **Nota: el tfd se recibe en formatob64
                var response = issue.TimbrarV1(xml, true);
                Console.WriteLine(response.status);
                Console.WriteLine(response.data.tfd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>
<details>
<summary>
Emisión Timbrado JSON V1
</summary>

<br>Emisión Timbrado JSON realiza el sellado y timbrado de un CFDI 4.0. Recibe el contenido de un JSON en formato String, posteriormente si la factura y el token son correctos devuelve el complemento timbre en un string (TFD), en caso contrario lanza una excepción

Este método recibe los siguientes parámetros:
* Archivo en formato **String**
* Usuario y contraseña o Token
* Url Servicios SW

**Ejemplo de consumo de la librería para la emisión Timbrado JSON en formato string utilizando usuario y contraseña**
```cs
using SW.Services.Issue;
using System;
using System.IO;
using System.Text;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Issue con parametros Url y credenciales de acceso
                IssueJson issue = new IssueJson("http://services.test.sw.com.mx", "user", "password");
                //Colocamos el JSON a timbrar en una variable
                var json = Encoding.UTF8.GetString(File.ReadAllBytes("file.json"));
                //Recibimos la respuesta enviando el JSON al metodo TimbrarJsonV1
                var response = issue.TimbrarJsonV1(json);
                Console.WriteLine(response.status);
                Console.WriteLine(response.data.tfd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**Timbrar XML en formato string utilizando token** [¿Como obtener token?](http://developers.sw.com.mx/knowledge-base/generar-un-token-infinito/)
```cs
using SW.Services.Issue;
using System;
using System.IO;
using System.Text;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Issue con parametros Url y token de acceso
                IssueJson issue = new IssueJson("http://services.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Colocamos el JSON a timbrar en una variable
                var json = Encoding.UTF8.GetString(File.ReadAllBytes("file.json"));
                //Recibimos la respuesta enviando el JSON al metodo TimbrarJsonV1
                var response = issue.TimbrarJsonV1(json);
                Console.WriteLine(response.status);
                Console.WriteLine(response.data.tfd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

:pushpin: ***NOTA:*** Existen varias versiones de respuesta:

| Version |                         Respuesta                             | 
|---------|---------------------------------------------------------------|
|  V1     | Devuelve el timbre fiscal digital                             | 
|  V2     | Devuelve el timbre fiscal digital y el CFDI timbrado          | 
|  V3     | Devuelve el CFDI timbrado                                     | 
|  V4     | Devuelve todos los datos del timbrado                         |

Para mayor referencia de estas versiones de respuesta, favor de visitar el siguiente [link](https://developers.sw.com.mx/knowledge-base/versiones-de-respuesta-timbrado/).
</details>

## Cancelación ##

Este servicio se utiliza para cancelar documentos xml y se puede hacer mediante varios métodos **Cancelación CSD**, **Cancelación PFX**, **Cancelacion por XML** y **Cancelación UUID**.

<details>
<summary>
Cancelación por CSD
</summary>

<br>Como su nombre lo indica, este método realiza la cancelacion mediante los CSD.

Este método recibe los siguientes parámetros:
* Usuario y contraseña
* Url Servicios SW
* Certificado (.cer) en **Base64**
* Key (.key) en **Base64**
* RFC emisor
* Password del archivo key
* UUID
* Motivo
* Folio Sustitución (Si el motivo es 01: "Comprobante emitido con errores con relación")

**Ejemplo de consumo de la librería para cancelar con CSD con motivo de cancelación 02 "Comprobante emitido con errores sin relación", mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Cancelation;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Cancelation 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a Cancelar el xml o cfdi

                Cancelation cancelation = new Cancelation("http://services.test.sw.com.mx", "user", "password");
               
                //Obtenemos Certificado y lo convertimos a Base 64
                string CerB64 = Convert.ToBase64String(File.ReadAllBytes("CSD_Prueba_CFDI_LAN8507268IA.cer"));
                //Obtenemos LLave y lo convertimos a Base 64
                string KeyB64 = Convert.ToBase64String(File.ReadAllBytes("CSD_Prueba_CFDI_LAN8507268IA.key"));

               
                CancelationResponse response = cancelation.CancelarByCSD(CerB64, KeyB64, "LAN8507268IA", "12345678a", "01724196-ac5a-4735-b621-e3b42bcbb459","02");
              
                if (response.status == "success" && response.Data != null)

                {
                    //Acuse de cancelación
                    Console.WriteLine(response.data.acuse);
                    //Estatus por UUID
                    foreach (var folio in response.data.uuid)
                    {
                        Console.WriteLine("UUID: {0} Estatus: {1}", folio.Key, folio.Value);
                    }
                }
                else
                {
                    //Obtenemos el detalle del Error
                    Console.WriteLine("Error al Cancelar\n\n");
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**Ejemplo de consumo de la librería para cancelar con CSD con motivo de cancelación 01 "Comprobante emitido con errores con relación", mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Cancelation;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Cancelation 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a Cancelar el xml o cfdi
                Cancelation cancelation = new Cancelation("http://services.test.sw.com.mx", "user", "password");

                //Obtenemos Certificado y lo convertimos a Base 64
                string CerB64 = Convert.ToBase64String(File.ReadAllBytes("CSD_Prueba_CFDI_LAN8507268IA.cer"));
                //Obtenemos LLave y lo convertimos a Base 64
                string KeyB64 = Convert.ToBase64String(File.ReadAllBytes("CSD_Prueba_CFDI_LAN8507268IA.key"));

                CancelationResponse response = cancelation.CancelarByCSD(CerB64, KeyB64, "LAN8507268IA", "12345678a", "01724196-ac5a-4735-b621-e3b42bcbb459","01","01724196-ac5a-4735-b621-e3b42bcbb459");

                if (response.status == "success" && response.data != null)
                {
                    //Acuse de cancelación
                    Console.WriteLine(response.data.acuse);
                    //Estatus por UUID
                    foreach (var folio in response.data.uuid)
                    {
                        Console.WriteLine("UUID: {0} Estatus: {1}", folio.Key, folio.Value);
                    }
                }
                else
                {
                    //Obtenemos el detalle del Error
                    Console.WriteLine("Error al Cancelar\n\n");
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
<summary>
Cancelación por XML
</summary>

<br>Como su nombre lo indica, este método realiza la cancelación mediante el XML sellado con los UUID a cancelar.

Este método recibe los siguientes parámetros:
* Usuario y contraseña
* Url Servicios SW
* XML sellado con los UUID a cancelar.

**Ejemplo de XML para Cancelar**
```xml
<Cancelacion xmlns="http://cancelacfd.sat.gob.mx"
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    xmlns:xsd="http://www.w3.org/2001/XMLSchema" Fecha="2021-12-26T18:15:28" RfcEmisor="EKU9003173C9">
    <Folios>
        <Folio UUID="fe4e71b0-8959-4fb9-8091-f5ac4fb0fef8" Motivo="02" FolioSustitucion=""/>
    </Folios>
    <Signature xmlns="http://www.w3.org/2000/09/xmldsig#">
        <SignedInfo>
            <CanonicalizationMethod Algorithm="http://www.w3.org/TR/2001/REC-xml-c14n-20010315" />
            <SignatureMethod Algorithm="http://www.w3.org/2000/09/xmldsig#rsa-sha1" />
            <Reference URI="">
                <Transforms>
                    <Transform Algorithm="http://www.w3.org/2000/09/xmldsig#enveloped-signature" />
                </Transforms>
                <DigestMethod Algorithm="http://www.w3.org/2000/09/xmldsig#sha1" />
                <DigestValue>XEdUtCptjdlz9DsYAP7nnU6MytU=</DigestValue>
            </Reference>
        </SignedInfo>
        <SignatureValue>ZnWh91e5tUc4/t1ZWnb3yOgB8zuCXNPioND+rv6aLOEwIw26/8sYYb+GT4wgyqlc09wOs32XTUwWoGQwtWMG8Euqq+4xJyobWvPCsX6CiURvD/Pd33xgkH92A0AGQxEMYGVT7wK+GFS2gDTYEYAXvZqzCe6+rXnlQvHML0TOOmhVu/wc8YrCbGt4z/F5sRxhjpa0eqwFEq4RmB4nkWjcD3Pnudn3XAI5NHIiOd8KVGVcDR+LvYvKj7h+18WxZgujpggYjbFN79i1jEsAEPDfgryUdTvjDw+KC7Mg+/ge6pssH42buEMIwVE4VX9Y3NtWSGTwdIK/8pxXk+Y5wyR6Gg==</SignatureValue>
        <KeyInfo>
            <X509Data>
                <X509IssuerSerial>
                    <X509IssuerName>OID.1.2.840.113549.1.9.2=responsable: ACDMA-SAT, OID.2.5.4.45=2.5.4.45, L=COYOACAN, S=CIUDAD DE MEXICO, C=MX, PostalCode=06370, STREET=3ra cerrada de cadiz, E=oscar.martinez@sat.gob.mx, OU=SAT-IES Authority, O=SERVICIO DE ADMINISTRACION TRIBUTARIA, CN=AC UAT</X509IssuerName>
                    <X509SerialNumber>292233162870206001759766198444326234574038512436</X509SerialNumber>
                </X509IssuerSerial>
                <X509Certificate>MIIFuzCCA6OgAwIBAgIUMzAwMDEwMDAwMDA0MDAwMDI0MzQwDQYJKoZIhvcNAQELBQAwggErMQ8wDQYDVQQDDAZBQyBVQVQxLjAsBgNVBAoMJVNFUlZJQ0lPIERFIEFETUlOSVNUUkFDSU9OIFRSSUJVVEFSSUExGjAYBgNVBAsMEVNBVC1JRVMgQXV0aG9yaXR5MSgwJgYJKoZIhvcNAQkBFhlvc2Nhci5tYXJ0aW5lekBzYXQuZ29iLm14MR0wGwYDVQQJDBQzcmEgY2VycmFkYSBkZSBjYWRpejEOMAwGA1UEEQwFMDYzNzAxCzAJBgNVBAYTAk1YMRkwFwYDVQQIDBBDSVVEQUQgREUgTUVYSUNPMREwDwYDVQQHDAhDT1lPQUNBTjERMA8GA1UELRMIMi41LjQuNDUxJTAjBgkqhkiG9w0BCQITFnJlc3BvbnNhYmxlOiBBQ0RNQS1TQVQwHhcNMTkwNjE3MTk0NDE0WhcNMjMwNjE3MTk0NDE0WjCB4jEnMCUGA1UEAxMeRVNDVUVMQSBLRU1QRVIgVVJHQVRFIFNBIERFIENWMScwJQYDVQQpEx5FU0NVRUxBIEtFTVBFUiBVUkdBVEUgU0EgREUgQ1YxJzAlBgNVBAoTHkVTQ1VFTEEgS0VNUEVSIFVSR0FURSBTQSBERSBDVjElMCMGA1UELRMcRUtVOTAwMzE3M0M5IC8gWElRQjg5MTExNlFFNDEeMBwGA1UEBRMVIC8gWElRQjg5MTExNk1HUk1aUjA1MR4wHAYDVQQLExVFc2N1ZWxhIEtlbXBlciBVcmdhdGUwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCN0peKpgfOL75iYRv1fqq+oVYsLPVUR/GibYmGKc9InHFy5lYF6OTYjnIIvmkOdRobbGlCUxORX/tLsl8Ya9gm6Yo7hHnODRBIDup3GISFzB/96R9K/MzYQOcscMIoBDARaycnLvy7FlMvO7/rlVnsSARxZRO8Kz8Zkksj2zpeYpjZIya/369+oGqQk1cTRkHo59JvJ4Tfbk/3iIyf4H/Ini9nBe9cYWo0MnKob7DDt/vsdi5tA8mMtA953LapNyCZIDCRQQlUGNgDqY9/8F5mUvVgkcczsIgGdvf9vMQPSf3jjCiKj7j6ucxl1+FwJWmbvgNmiaUR/0q4m2rm78lFAgMBAAGjHTAbMAwGA1UdEwEB/wQCMAAwCwYDVR0PBAQDAgbAMA0GCSqGSIb3DQEBCwUAA4ICAQBcpj1TjT4jiinIujIdAlFzE6kRwYJCnDG08zSp4kSnShjxADGEXH2chehKMV0FY7c4njA5eDGdA/G2OCTPvF5rpeCZP5Dw504RZkYDl2suRz+wa1sNBVpbnBJEK0fQcN3IftBwsgNFdFhUtCyw3lus1SSJbPxjLHS6FcZZ51YSeIfcNXOAuTqdimusaXq15GrSrCOkM6n2jfj2sMJYM2HXaXJ6rGTEgYmhYdwxWtil6RfZB+fGQ/H9I9WLnl4KTZUS6C9+NLHh4FPDhSk19fpS2S/56aqgFoGAkXAYt9Fy5ECaPcULIfJ1DEbsXKyRdCv3JY89+0MNkOdaDnsemS2o5Gl08zI4iYtt3L40gAZ60NPh31kVLnYNsmvfNxYyKp+AeJtDHyW9w7ftM0Hoi+BuRmcAQSKFV3pk8j51la+jrRBrAUv8blbRcQ5BiZUwJzHFEKIwTsRGoRyEx96sNnB03n6GTwjIGz92SmLdNl95r9rkvp+2m4S6q1lPuXaFg7DGBrXWC8iyqeWE2iobdwIIuXPTMVqQb12m1dAkJVRO5NdHnP/MpqOvOgLqoZBNHGyBg4Gqm4sCJHCxA1c8Elfa2RQTCk0tAzllL4vOnI1GHkGJn65xokGsaU4B4D36xh7eWrfj4/pgWHmtoDAYa8wzSwo2GVCZOs+mtEgOQB91/g==</X509Certificate>
            </X509Data>
        </KeyInfo>
    </Signature>
</Cancelacion>
```
Para caso de motivo 01 deberá añadir el atributo "FolioSustitucion dentro del Nodo <Folio>

Ejemplo de nodo Folio: 
```xml
<Folios>
	<Folio UUID="b374db50-a0a3-4028-9d01-32b93e2b925a" Motivo="01" FolioSustitucion="b3641a4b-7177-4323-aaa0-29bd34bf1ff8" />
</Folios>
```

**Ejemplo de consumo de la librería para cancelar con XML mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Cancelation;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Cancelation 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a Cancelar el xml o cfdi
                Cancelation cancelation = new Cancelation("http://services.test.sw.com.mx", "user", "password");
                //Obtenemos el XML de cancelacion
                byte[] xml = File.ReadAllBytes("Resources/ejemplo.xml");

                CancelationResponse response = cancelation.CancelarByXML(xml);

                if (response.status == "success" && response.data != null)
                {
                    //Acuse de cancelación
                    Console.WriteLine(response.data.acuse);
                    //Estatus por UUID
                    foreach (var folio in response.data.uuid)
                    {
                        Console.WriteLine("UUID: {0} Estatus: {1}", folio.Key, folio.Value);
                    }
                }
                else
                {
                    //Obtenemos el detalle del Error
                    Console.WriteLine("Error al Cancelar\n\n");
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
<summary>
Cancelación por PFX
</summary>

<br>Como su nombre lo indica, este método realiza la cancelacion mediante el PFX.

Este método recibe los siguientes parámetros:
* Usuario y contraseña
* Url Servicios SW
* Archivo PFX en **Base64**
* RFC emisor
* Password (CSD)
* UUID
* Motivo
* Folio Sustitución

**Ejemplo de consumo de la librería para cancelar con PFX con motivo de cancelación 02 "Comprobante emitido con errores con relación", mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Cancelation;

namespace ExampleSDK
{
    class Program
    {
       static void Main(string[] args)
        {
            try
            {
                //Datos de Cancelación
                string uuid = "01724196-ac5a-4735-b621-e3b42bcbb459";
                string rfc = "LAN8507268IA";
                string passwordKey = "12345678a";
                string motivo = "02";
                //Creamos una instancia de tipo Cancelation 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a Cancelar el xml o cfdi

                Cancelation cancelation = new Cancelation("http://services.test.sw.com.mx", "user", "password");
               
                //Obtenemos el XML de cancelacion
                byte[] pfx = File.ReadAllBytes(Path.Combine(@"Resources\CertificadosDePrueba", "CSD_Prueba_CFDI_LAN8507268IA.pfx"));
                //Convertimos el PFX a base 64
                string pfxB64 = Convert.ToBase64String(pfx);

                //Realizamos la petición de cancelación al servicio.

                CancelationResponse response = cancelation.CancelarByPFX(pfxB64, rfc, passwordKey, uuid, motivo);
                if (response.status == "success" && response.Data != null)

                {
                    //Acuse de cancelación
                    Console.WriteLine(response.data.acuse);
                    //Estatus por UUID
                    foreach (var folio in response.data.uuid)
                    {
                        Console.WriteLine("UUID: {0} Estatus: {1}", folio.Key, folio.Value);
                    }
                }
                else
                {
                    //Obtenemos el detalle del Error
                    Console.WriteLine("Error al Cancelar\n\n");
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**Ejemplo de consumo de la librería para cancelar con PFX con motivo 01 "Comprobante emitido con errores con relación", mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Cancelation;

namespace ExampleSDK
{
    class Program
    {
       static void Main(string[] args)
        {
            try
            {
                //Datos de Cancelación
                string uuid = "01724196-ac5a-4735-b621-e3b42bcbb459";
                string rfc = "LAN8507268IA";
                string passwordKey = "12345678a";
                //Creamos una instancia de tipo Cancelation 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a Cancelar el xml o cfdi
                Cancelation cancelation = new Cancelation("http://services.test.sw.com.mx", "user", "password");

                //Obtenemos el XML de cancelacion
                byte[] pfx = File.ReadAllBytes(Path.Combine(@"Resources\CertificadosDePrueba", "CSD_Prueba_CFDI_LAN8507268IA.pfx"));
                //Convertimos el PFX a base 64
                string pfxB64 = Convert.ToBase64String(pfx);

                //Realizamos la petición de cancelación al servicio.
                CancelationResponse response = cancelation.CancelarByPFX(pfxB64, rfc, passwordKey, uuid,"01", "017241788-ac5a-4735-b621-e3b42bcbb584");
                if (response.status == "success" && response.data != null)
                {
                    //Acuse de cancelación
                    Console.WriteLine(response.data.acuse);
                    //Estatus por UUID
                    foreach (var folio in response.data.uuid)
                    {
                        Console.WriteLine("UUID: {0} Estatus: {1}", folio.Key, folio.Value);
                    }
                }
                else
                {
                    //Obtenemos el detalle del Error
                    Console.WriteLine("Error al Cancelar\n\n");
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
<summary>
Cancelación por UUID
</summary>

<br>Como su nombre lo indica, este método realiza la cancelacion mediante el UUID a cancelar.

Este método recibe los siguientes parámetros:
* Usuario y contraseña
* Url Servicios SW
* RFC emisor
* UUID
* Motivo
* Folio Sustitución

**Ejemplo de consumo de la librería para cancelar con UUID con motivo de cancelación 02 "Comprobante emitido sin errores con relación", mediante usuario y contraseñ**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Cancelation;

namespace ExampleSDK
{
    class Program
    {
      static void Main(string[] args)
        {
            try
            {
                //Datos de Cancelación
                string uuid = "01724196-ac5a-4735-b621-e3b42bcbb459";
                string rfc = "LAN8507268IA";
                //Creamos una instancia de tipo Cancelation 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a Cancelar el xml o cfdi
                Cancelation cancelation = new Cancelation("http://services.test.sw.com.mx", "user", "password");
                //Realizamos la petición de cancelación al servicio.
                CancelationResponse response = cancelation.CancelarByRfcUuid(rfc,uuid,"02");
                if (response.status == "success" && response.data != null)
                {
                    //Acuse de cancelación
                    Console.WriteLine(response.data.acuse);
                    //Estatus por UUID
                    foreach (var folio in response.data.uuid)
                    {
                        Console.WriteLine("UUID: {0} Estatus: {1}", folio.Key, folio.Value);
                    }
                }
                else
                {
                    //Obtenemos el detalle del Error
                    Console.WriteLine("Error al Cancelar\n\n");
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**Ejemplo de consumo de la librería para cancelar con UUID con motivo de cancelación 01 "Comprobante emitido con errores con relación", mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Cancelation;

namespace ExampleSDK
{
    class Program
    {
      static void Main(string[] args)
        {
            try
            {
                //Datos de Cancelación
                string uuid = "01724196-ac5a-4735-b621-e3b42bcbb459";
                string rfc = "LAN8507268IA";
                string motivo = "01";
                string folioSustitucion = "09d849d8-1cbf-424e-84bc-8e6724dcb649";
                //Creamos una instancia de tipo Cancelation 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a Cancelar el xml o cfdi
                Cancelation cancelation = new Cancelation("http://services.test.sw.com.mx", "user", "password");
                //Realizamos la petición de cancelación al servicio.

                CancelationResponse response = cancelation.CancelarByRfcUuid(rfc, uuid, motivo, folioSustitucion);
                if (response.status == "success" && response.Data != null)

                {
                    //Acuse de cancelación
                    Console.WriteLine(response.data.acuse);
                    //Estatus por UUID
                    foreach (var folio in response.data.uuid)
                    {
                        Console.WriteLine("UUID: {0} Estatus: {1}", folio.Key, folio.Value);
                    }
                }
                else
                {
                    //Obtenemos el detalle del Error
                    Console.WriteLine("Error al Cancelar\n\n");
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

## Usuarios V2 ##
Métodos para realizar la consulta de informacion de usuarios, así como la creación, actualización y eliminacion  de los mismos

> [!IMPORTANT]
> Los métodos han tenido algunos cambios y mejoras con respecto a la versión 1.

<details>
  <summary>Crear usuario</summary>

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token
* Url Servicios SW
* Url Api
* Informacion del nuevo cliente

***Información del cliente:*** 

| Dato              | Descripción                                  |
|-------------------|----------------------------------------------|
| name              | Nombre del usuario                           |
| taxId             | RFC del usuario                              |
| email             | correo del nuevo usuario                     |
| stamps            | Cantidad de timbres a asignar                |
| isUnlimited       | Especificar si tendra timbres ilimitados     |
| password          | Contraseña del usuario                       |
| notificationEmail | Correo a donde quiere recibir notificaciones |
| phone             | Número del telefono del usuario              |

**Ejemplo de consumo de la libreria para crear un usuario**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Account.AccountUser;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                AccountUser user = new AccountUser("https://services.test.sw.com.mx", "https://api.test.sw.com.mx", "user", "password");

                //Obtenemos el response haciendo uso del metodo CreateUser, agregandole la informacion de nuestro nuevo usuario
                 var response = user.CreateUser(new AccountUserRequest()
                {
                    name = "Usuarionuevo",
                    taxId = "XAXX010101000",
                    email = "cuenta_nuevo_usuario@gmail.com",
                    stamps = 1,
                    isUnlimited = false,
                    password = "nueva_contraseña!2",
                    notificationEmail = "correo_notificaciones_usuario@gmail.com",
                    phone = "0000000000"
                 });

                //Para Obtener el mensaje de respuesta.
                response.data;
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**Ejemplo de consumo de la libreria para crear un usuario mediante token**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Account.AccountUser;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser
                //A esta le pasamos la Url y el token token
                AccountUser user = new AccountUser("https://api.test.sw.com.mx", "TT2lYQ0t4L0R....ReplaceForRealToken");

                //Obtenemos el response haciendo uso del metodo CreateUser, agregandole la informacion de nuestro nuevo usuario
                 var response = user.CreateUser(new AccountUserRequest()
                {
                    name = "Usuarionuevo",
                    taxId = "XAXX010101000",
                    email = "cuenta_nuevo_usuario@gmail.com",
                    stamps = 1,
                    isUnlimited = false,
                    password = "nueva_contraseña!2",
                    notificationEmail = "correo_notificaciones_usuario@gmail.com",
                    phone = "0000000000"
                 });

                //Para Obtener el mensaje de respuesta.
                response.data;
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

:pushpin: ***NOTA:*** La contraseña debe cumplir con las siguientes politicas:
* La contraseña no debe ser igual que el nombre de usuario.
* La contraseña debe incluir al menos una letra mayúscula.
* La contraseña debe incluir al menos una letra minúscula
* La contraseña debe incluir al menos un número.
* La contraseña debe incluir al menos un símbolo (carácter especial).
* La contraseña no debe incluir espacios en blanco.
* La contraseña debe tener entre 10 y 20 caracteres.
* La contraseña no debe incluir símbolos especiales fuera de lo común.
* Los caracteres especiales aceptados son los siguientes: !@#$%^&*()_+=\[{\]};:<>|./?,-]
</details>

<details>
  <summary>Actualizacion de datos de usuario</summary>

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token
* Url Servicios SW
* Url Api
* IdUser
* Datos nuevos del cliente

***Información nueva del cliente:*** 

| Dato              | Descripción                              |
|-------------------|------------------------------------------|
| idUser            | Id del usuario a actualizar              |
| name              | Nuevo nombre del usuario                 |
| taxId             | Nuevo RFC del usuario                    |
| notificationEmail | Nuevo correo para recibir notificaciones |
| isUnlimited       | Especificar si tendra timbres ilimitados |
| phone             | Número del telefono del usuario          |



> [!NOTE]  
> Puedes asignarles “null” a las propiedades que no vayas a actualizar.

**Ejemplo de consumo de la libreria para actualizar usuarios**
```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountUser;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                AccountUser user = new AccountUser("https://services.test.sw.com.mx", "https://api.test.sw.com.mx", "user", "password");
                //Obtenemos y convertimos a tipo Guid el id del usuario
                Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E131");
                //Se envían los parámetros a la función UpdateUser
                var response = user.UpdateUser(idUser,"Nombre Actualizado", "AAAA000101010", "nuevo_correo_notificaciones@example.com", false, null);
                //Para Obtener el mensaje de respuesta.
                response.data;
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**Ejemplo de consumo de la libreria para actualizar usuarios mediante token**
```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountUser;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser 
                //A esta le pasamos la Url y  el token
                AccountUser user = new AccountUser("https://api.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Obtenemos y convertimos a tipo Guid el id del usuario
                Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E131");
                //Se envían los parámetros a la función UpdateUser
                var response = user.UpdateUser(idUser,"Nombre Actualizado", "AAAA000101010", "nuevo_correo_notificaciones@example.com", false, null);
                //Para Obtener el mensaje de respuesta.
                response.data;
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
  <summary>Eliminación de usuario</summary>

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token
* Url Servicios SW
* Url Api
* IdUser

**Ejemplo de consumo de la libreria para eliminar usuarios**

```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountUser;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                AccountUser user = new AccountUser("https://services.test.sw.com.mx", "https://api.test.sw.com.mx", "user", "password");
                //Obtenemos y convertimos a tipo Guid el id del usuario
                Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E131");
                //Se envían los parámetros a la función DeleteUser
                  var response = user.DeleteUser(idUser);
                //Para Obtener el mensaje de respuesta.
                response.data;

                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**Ejemplo de consumo de la libreria para eliminar usuarios mediante token**

```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountUser;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser 
                //A esta le pasamos la Url y el token
                AccountUser user = new AccountUser("https://api.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Obtenemos y convertimos a tipo Guid el id del usuario
                Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E131");
                //Se envían los parámetros a la función DeleteUser
                  var response = user.DeleteUser(idUser);
                //Para Obtener el mensaje de respuesta.
                response.data;
                 
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>
<details>
  <summary>Obtener todos los usuarios</summary>

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token de la cuenta padre.
* Url Servicios SW
* Url Api

**Ejemplo de consumo de la libreria para obtener todos los usarios de una cuenta administradora o padre**
```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountUser;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                AccountUser infoUser = new AccountUser("https://services.test.sw.com.mx", "https://api.test.sw.com.mx", "user", "password");
                //Se instancia el metodo GetAllUsers();
                var response = infoUser.GetAllUsers();
                //Para consultar los datos de todos los usuarios obtenidos
                List<AccountUserData> user = response.data;
                foreach (var item in user)
                {
                    Console.WriteLine(item.idUser);
                    Console.WriteLine(item.idDealer);
                    Console.WriteLine(item.taxId);
                    Console.WriteLine(item.name);
                    Console.WriteLine(item.username);
                    Console.WriteLine(item.profile);
                    Console.WriteLine(item.accessToken);
                }
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
**Ejemplo de consumo de la libreria para obtener todos los usarios de una cuenta administradora o padre mediante token**
```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountUser;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser 
                //A esta le pasamos la Url y el token
                AccountUser infoUser = new AccountUser("https://api.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Se instancia el metodo GetAllUsers();
                var response = infoUser.GetAllUsers();
                //Para consultar los datos de todos los usuarios obtenidos
                List<AccountUserData> user = response.data;
                foreach (var item in user)
                {
                    Console.WriteLine(item.idUser);
                    Console.WriteLine(item.idDealer);
                    Console.WriteLine(item.taxId);
                    Console.WriteLine(item.name);
                    Console.WriteLine(item.username);
                    Console.WriteLine(item.profile);
                    Console.WriteLine(item.accessToken);
                }
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

</details>
<details>
  <summary>Obtener usuario por ID</summary>

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token de la cuenta padre
* Url Servicios SW
* Url Api
* IdUser

**Ejemplo de consumo de la libreria para obtener usuario por ID**
```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountUser;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                AccountUser infoUser = new AccountUser("https://services.test.sw.com.mx", "https://api.test.sw.com.mx", "user", "password");
                //Obtenemos y convertimos a tipo Guid el id del usuario
                Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E131");
                //Se envían los parámetros a la función GetUserById
                var response = infoUser.GetUserById(idUser);
                List<AccountUserData> user = response.data;
                foreach (var item in user)
                {
                    Console.WriteLine(item.idUser);
                    Console.WriteLine(item.idDealer);
                    Console.WriteLine(item.taxId);
                    Console.WriteLine(item.name);
                    Console.WriteLine(item.username);
                    Console.WriteLine(item.profile);
                    Console.WriteLine(item.accessToken);
                }
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
**Ejemplo de consumo de la libreria para obtener usuario por ID mediante el token**
```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountUser;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser 
                //A esta le pasamos la Url y el token
                AccountUser infoUser = new AccountUser("https://api.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Obtenemos y convertimos a tipo Guid el id del usuario
                Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E131");
                //Se envían los parámetros a la función GetUserById
                var response = infoUser.GetUserById(idUser);
                List<AccountUserData> user = response.data;
                foreach (var item in user)
                {
                    Console.WriteLine(item.idUser);
                    Console.WriteLine(item.idDealer);
                    Console.WriteLine(item.taxId);
                    Console.WriteLine(item.name);
                    Console.WriteLine(item.username);
                    Console.WriteLine(item.profile);
                    Console.WriteLine(item.accessToken);
                }
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>
<details>
  <summary>Obtener usuarios por Email</summary>

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token
* Url Servicios SW
* Url Api
* Email del usuario a consulta.

**Ejemplo de consumo de la libreria para obtener todos los usuarios**
```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountUser;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                AccountUser infoUser = new AccountUser("https://services.test.sw.com.mx", "https://api.test.sw.com.mx", "user", "password");
                //Se instancia el metodo GetUserByEmail
                var response = infoUser.GetUserByEmail("cuenta_usuario@gmail.com");
                //Para Obtener el mensaje de respuesta.
                List<AccountUserData> user = response.data;
                foreach (var item in user)
                {
                    Console.WriteLine(item.idUser);
                    Console.WriteLine(item.idDealer);
                    Console.WriteLine(item.taxId);
                    Console.WriteLine(item.name);
                    Console.WriteLine(item.username);
                    Console.WriteLine(item.profile);
                    Console.WriteLine(item.accessToken);
                }
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
**Ejemplo de consumo de la libreria para obtener todos los usuarios mediante token**
```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountUser;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser 
                //A esta le pasamos la Url y el token
                AccountUser infoUser = new AccountUser("https://api.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Se instancia el metodo GetUserByEmail
                var response = infoUser.GetUserByEmail("cuenta_usuario@gmail.com");
                //Para Obtener el mensaje de respuesta.
                List<AccountUserData> user = response.data;
                foreach (var item in user)
                {
                    Console.WriteLine(item.idUser);
                    Console.WriteLine(item.idDealer);
                    Console.WriteLine(item.taxId);
                    Console.WriteLine(item.name);
                    Console.WriteLine(item.username);
                    Console.WriteLine(item.profile);
                    Console.WriteLine(item.accessToken);
                }
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>
<details>
  <summary>Obtener usuarios por RFC</summary>

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token
* Url Servicios SW
* Url Api
* RFC del usuario a consultar.

**Ejemplo de consumo de la libreria para obtener todos los usuarios**
```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountUser;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                AccountUser infoUser = new AccountUser("https://services.test.sw.com.mx", "https://api.test.sw.com.mx", "user", "password");
                //Se instancia el metodo GetUserByTaxId
                var response = infoUser.GetUserByTaxId("XAXX010101000");
                //Para Obtener el mensaje de respuesta.
                List<AccountUserData> user = response.data;
                foreach (var item in user)
                {
                    Console.WriteLine(item.idUser);
                    Console.WriteLine(item.idDealer);
                    Console.WriteLine(item.taxId);
                    Console.WriteLine(item.name);
                    Console.WriteLine(item.username);
                    Console.WriteLine(item.profile);
                    Console.WriteLine(item.accessToken);
                }
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
**Ejemplo de consumo de la libreria para obtener todos los usuarios mediante token**
```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountUser;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser 
                //A esta le pasamos la Url y el token
                AccountUser infoUser = new AccountUser("https://api.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Se instancia el metodo GetUserByEmail
                var response = infoUser.GetUserByTaxId("XAXX010101000");
                //Para Obtener el mensaje de respuesta.
                List<AccountUserData> user = response.data;
                foreach (var item in user)
                {
                    Console.WriteLine(item.idUser);
                    Console.WriteLine(item.idDealer);
                    Console.WriteLine(item.taxId);
                    Console.WriteLine(item.name);
                    Console.WriteLine(item.username);
                    Console.WriteLine(item.profile);
                    Console.WriteLine(item.accessToken);
                }
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>
<details>
  <summary>Obtener usuarios que esten activos o desactivados</summary>

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token
* Url Servicios SW
* Url Api
* Indica si el Usuario es activo o no (true o false)

**Ejemplo de consumo de la libreria para obtener todos los usuarios**
```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountUser;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                AccountUser infoUser = new AccountUser("https://services.test.sw.com.mx", "https://api.test.sw.com.mx", "user", "password");
                //Se instancia el metodo GetUserByIsActive
                var response = infoUser.GetUserByIsActive(true);
                //Para Obtener el mensaje de respuesta.
                List<AccountUserData> user = response.data;
                foreach (var item in user)
                {
                    Console.WriteLine(item.idUser);
                    Console.WriteLine(item.idDealer);
                    Console.WriteLine(item.taxId);
                    Console.WriteLine(item.name);
                    Console.WriteLine(item.username);
                    Console.WriteLine(item.profile);
                    Console.WriteLine(item.accessToken);
                }
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
**Ejemplo de consumo de la libreria para obtener todos los usuarios mediante token**
```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountUser;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountUser 
                //A esta le pasamos la Url y el token
                AccountUser infoUser = new AccountUser("https://api.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Se instancia el metodo GetUserByIsActive
                var response = infoUser.GetUserByIsActive(true);
                //Para Obtener el mensaje de respuesta.
                List<AccountUserData> user = response.data;
                foreach (var item in user)
                {
                    Console.WriteLine(item.idUser);
                    Console.WriteLine(item.idDealer);
                    Console.WriteLine(item.taxId);
                    Console.WriteLine(item.name);
                    Console.WriteLine(item.username);
                    Console.WriteLine(item.profile);
                    Console.WriteLine(item.accessToken);
                }
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

## Consulta y Asignación de Timbres ##
Métodos para realizar la consulta de saldo así como la asignación y eliminación de timbres a un usuario.

<details>
  <summary>Consulta de timbres</summary>

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token
* Url Servicios SW
* Url Api

> [!IMPORTANT]  
> Los nombres de las variables en la respuesta han cambiado.

**Ejemplo de consumo de la libreria para consultar el saldo**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Account.AccountBalance;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountBalance 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a consultar el saldo
                AccountBalance account = new AccountBalance("https://services.test.sw.com.mx", "https://api.test.sw.com.mx", "user", "password");
                AccountResponse response = account.ConsultarSaldo();
              
                //Para Obtener el idSaldoCliente
                response.data.idUserBalance
                
                //Para Obtener el idClienteUsuario
                response.data.idUser
                
                //Para Obtener el saldo Timbres
                response.data.stampsBalance
                
                //Para Obtenerlos timbres Utilizados
                response.data.stampsUsed

                //Para Obtener la fechaExpiracion
                response.data.expirationDate;
                
                //Para Obtener si es Ilimitado
                response.data.unlimited
                
                //Para Obtener los timbres Asignados
                response.data.stampsAssigned
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
  <summary>Agregar timbres</summary>

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token
* Url Servicios SW
* Url Api
* IdUser
* Número de timbres
* Comentario

> [!NOTE] 
> El servicio regresa unicamente la cantidad de timbres despues del abono de timbres.

**Ejemplo de consumo de la libreria para agregar timbres**
```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountBalance;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountBalance 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                AccountBalance account = new AccountBalance("https://services.test.sw.com.mx", "https://api.test.sw.com.mx", "user", "password");
                //Obtenemos y convertimos a tipo Guid el id del usuario
                Guid idUser = Guid.Parse("32701CF2-DC63-4370-B47D-25024C44E131");
                //Se envían los parámetros a la función AgregarTimbres
                AccountResponse response = account.AgregarTimbres(idUser, 2, "Timbres agregados");
              
                //Para Obtener el número de timbre despues del abono de timbres
                response.data;
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>
<details>
  <summary>Eliminar timbres</summary>

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token
* Url Servicios SW
* Url Api
* IdUser
* Número de timbres
* Comentario

> [!NOTE]
> El servicio regresa unicamente la cantidad de timbres despues de remover los timbres.

**Ejemplo de consumo de la libreria para remover timbres**
```cs
using System;
using SW.Helpers;
using SW.Services.Account.AccountBalance;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AccountBalance 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                AccountBalance account = new AccountBalance("https://services.test.sw.com.mx", "https://api.test.sw.com.mx", "user", "password");
                //Obtenemos y convertimos a tipo Guid el id del usuario
                Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E131");
                //Se envían los parámetros a la función EliminarTimbres
                AccountResponse response = account.EliminarTimbres(idUser, 2, "Timbres removidos");
              
                //Para Obtener el número de timbre despues de remover timbres
                response.data;
                
                //En caso de error, se pueden visualizar los campos message y/o messageDetail
                response.message;
                response.messageDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>


## Validaciones ##

<details>
<summary>
Validación XML
</summary>

<br>Este servicio recibe un comprobante CFDI 4.0 en formato XML mediante el cual se valida integridad, sello, errores de estructura, matriz de errores del SAT incluyendo complementos, se valida que exista en el SAT, así como el estatus en el SAT.

Este método recibe los siguientes parámetros:
* Url Servicios SW
* Usuario y contraseña o token
* XML

**Ejemplo de consumo de la librería para validar el XML mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Validate;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Validate
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a validar el XML
                Validate validate = new Validate ("http://services.test.sw.com.mx", "user", "password");
                var xml = GetXml(build);
                ValidateXmlResponse response = validate.ValidateXml(xml);
                //Para iterar la lista sobre la validacion estructura
                List<Detail> detail1 = response.detail;
                Console.Write("Status: "+response.status);
                Console.Write("\ndetail: ");
                foreach (var i in detail1)
                {
                    foreach(var j in i.detail)
                    {
                        Console.Write("\n\tdetail: ");
                        Console.Write("\n\t\tMessage: "+ j.message);
                        Console.Write("\n\t\tMessageDetail: "+ j.messageDetail);
                        TextBoxOut.AppendText("\n\t\tType: "+ j.type);
                    }
                    Console.Write("\n\tSection: \n"+ i.section);
                }
				//Para obtener la cadena original SAT
				Console.Write(response.cadenaOriginalSAT + "\n");
				//Para obtener la cadena original del comprobante
				Console.Write(response.cadenaOriginalComprobante + "\n");
				//Para obtener el uuid
				Console.Write(response.uuid + "\n");
				//Para obtener el status SAT
				Console.Write(response.statusSat + "\n");
				//Para obtener el status code SAT
                Console.Write(response.statusCodeSat + "\n");
	            //En caso de error se pueden consultar los siguientes campos
	            Console.WriteLine(response.message);
	            Console.WriteLine(response.messageDetail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
**Ejemplo de consumo de la librería para validar el XML pero no consultar su estatus en el SAT mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Validate;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Validate
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a validar el XML
                Validate validate = new Validate ("http://services.test.sw.com.mx", "user", "password");
                var xml = GetXml(build);
                ValidateXmlResponse response = validate.ValidateXml(xml, false);
                //Para iterar la lista sobre la validacion estructura
                List<Detail> detail1 = response.detail;
                Console.Write("Status: "+response.status);
                Console.Write("\ndetail: ");
                foreach (var i in detail1)
                {
                    foreach(var j in i.detail)
                    {
                        Console.Write("\n\tdetail: ");
                        Console.Write("\n\t\tMessage: "+ j.message);
                        Console.Write("\n\t\tMessageDetail: "+ j.messageDetail);
                        TextBoxOut.AppendText("\n\t\tType: "+ j.type);
                    }
                    Console.Write("\n\tSection: \n"+ i.section);
                }
				//Para obtener la cadena original SAT
				Console.Write(response.cadenaOriginalSAT + "\n");
				//Para obtener la cadena original del comprobante
				Console.Write(response.cadenaOriginalComprobante + "\n");
				//Para obtener el uuid
				Console.Write(response.uuid + "\n");
				//Para obtener el status SAT
				Console.Write(response.statusSat + "\n");
				//Para obtener el status code SAT
                Console.Write(response.statusCodeSat + "\n");
	            //En caso de error se pueden consultar los siguientes campos
	            Console.WriteLine(response.message);
	            Console.WriteLine(response.messageDetail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
:pushpin: ***NOTA:*** Este método permite  validar la estructura del XML pero no su estatus en SAT, optimizando tiempos. En los atributos como
"statusSat" y "statusCodeSat" se obtendra un "No Aplica".
</details>

## PDF ##

<details>
<summary>
Generar PDF
</summary>

<br>Este método genera y obtiene un  PDF en base64 a partir de un documento XML timbrado y una plantilla. Puede ser consumido ingresando tu usuario y contraseña así como tambien ingresando solo el token. Este método recibe los siguientes parámetros:

* Url API
* Url servicios SW
* Logo Base64 (opcional)
* Template ID
* XML timbrado
* Datos extra (opcional)

**Ejemplo de consumo de la librería para la consulta mediante token**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Pdf;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Pdf pdf = new Pdf("https://api.test.sw.com.mx","https://services.test.sw.com.mx","user", "password");
                string xml = Encoding.UTF8.GetString(File.ReadAllBytes("file.xml"));
                var pdfResult = pdf.GenerarPdf(xml,"/9j/4AAQSk...",TemplatesId.cfdi40);
                //Devuleve el pdf en formato Base64
                Console.WriteLine(pdfResult.data.contentB64);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            //Puedes solicitar customizar tu propia plantilla para agregar datos adicionales que no vengan incluidos en el xml
            try
            {
                Pdf pdf = new Pdf("https://api.test.sw.com.mx","https://services.test.sw.com.mx","T2lYQ0t4L0R....ReplaceForRealToken");
                string xml = Encoding.UTF8.GetString(File.ReadAllBytes("file.xml"));
               Dictionary<string, object> extras_basico = new Dictionary<string, object>() { { "extras", new Dictionary<string, object> { { "Observaciones", "Entregar de 9am a 6pm" }, { "DireccionEntrega", "Calles gomez Farías esquina con Tlaloc" }, { "REFERENCIA", "Ejemplo de referencia" } } } };
                var pdfResult = pdf.GenerarPdf(xml,"/9j/4AAQSk...","templateIdCustom", extras_basico);
                //Devuleve el pdf en formato Base64
                Console.WriteLine(pdfResult.data.contentB64);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            try
            {
                //Puedes enviar el xml convertido en Base64
                Pdf pdf = new Pdf("https://api.test.sw.com.mx","https://services.test.sw.com.mx","T2lYQ0t4L0R....ReplaceForRealToken");
                Dictionary<string, object> extras_basico = new Dictionary<string, object>() { { "extras", new Dictionary<string, object> { { "Observaciones", "Entregar de 9am a 6pm" }, { "DireccionEntrega", "Calles gomez Farías esquina con Tlaloc" }, { "REFERENCIA", "Ejemplo de referencia" } } } };
                var pdfResult = pdf.GenerarPdf(xml,"/9j/4AAQSk...","templateIdCustom", extras_basico, null, true);
                //Devuleve el pdf en formato Base64
                Console.WriteLine(pdfResult.data.contentB64);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
```
:pushpin: ***NOTA:*** Existen varias plantillas de PDF segun el tipo de comprobante que necesites, las cuales son las siguientes:

|    Versión 4.0     |  Plantilla para el complemento  | 
|--------------------|---------------------------------|
| :white_check_mark: | Factura ingreso, egreso         | 
| :white_check_mark: | Nómina                          | 
| :white_check_mark: | Pagos                           | 
| :white_check_mark: | Carta porte                     |

Para mayor referencia de estas plantillas de PDF, favor de visitar el siguiente [link](https://developers.sw.com.mx/knowledge-base/plantillas-pdf/).
</details>


<details>
<summary>
Regenerar PDF
</summary>

<br>El servicio podrá generar o regenerar un PDF de un CFDI previamente timbrados y podrá guardar o remplazar el archivo PDF para ser visualizado posteriormente desde el portal de Smarter. Puede ser consumido ingresando tu usuario y contraseña así como tambien ingresando solo el token. Este método recibe los siguientes parámetros:

* Url Servicios SW(cuando se añaden usuario y contraseña)
* Url Api
* UUID
* Logo en base 64
* TemplateId
* Parametros Extras

**Ejemplo de consumo de la librería para la regeneración de PDF mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Pdf;

namespace ExampleSDK
{
    class Program
    {
      static void Main(string[] args)
        {
            try
            {
                //UUID del Xml al que quieres que se genere o regenere el PDF
                Guid uuid = "01724196-ac5a-4735-b621-e3b42bcbb459";
                //Creamos una instancia de tipo Pdf
                //A esta le pasamos la UrlApi, Url, Usuario y Contraseña para obtener el token
                Pdf regeneratePdf = new Pdf("https://api.test.sw.com.mx", "https://services.test.sw.com.mx", "user", "password");
                //Realizamos la petición de regenerar el pdf.
                 Dictionary<string, object> extras_basico = new Dictionary<string, object>() { { "extras", new Dictionary<string, object> { { "Observaciones", "Entregar de 9am a 6pm" }, { "DireccionEntrega", "Calles gomez Farías esquina con Tlaloc" }, { "REFERENCIA", "Ejemplo de referencia" } } } };
                PdfResponse response = regeneratePdf.RegenerarPdf(uuid, logob64, templateId, extras_basico);
                //Obtenemos el detalle de la respuesta
                 Console.WriteLine(response.status);
                 Console.WriteLine(response.message);
                 Console.WriteLine(response.messageDetail);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
<summary>
Reenvio Email
</summary>

<br>Este servicio realiza el reenvío de un xml y/o pdf existente mediante su UUID
a través de correo electrónico.

Este método recibe los siguientes parámetros:
* Url Servicios SW(cuando se añaden usuario y contraseña)
* Url Api
* UUID: Folio fiscal del comprobante timbrado
* Email: Correo electrónico (máximo 5 correos separados por ”,” )

**Ejemplo de consumo de la librería para la consulta mediante token**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Resend;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
		//Creamos una instancia de tipo Resend
		//A esta le pasamos el UrlAPi, asi como nuestro token
		Resend resend = new Resend("https://api.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
		//Creamos una array con los correos (Max. 5 correos, separados por ",")
		string[] email = {"prueba@test.com"};
		//Automaticamente recibiremos a nuestro correo el XML y/o PDF existente
		var response = resend.ResendEmail(Guid.Parse("b711186a-8452-4206-9fec-1b14baad281e"), email);
                //Para obtener el estatus
                Console.WriteLine(response.status);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

## Consulta Estatus ##

<details>
<summary>
Consulta Estatus SAT
</summary>

<br>Este servicio sirve para consultar el estatus de un CFDI antes y después de enviarlo a cancelar, con él sabremos sí puede ser cancelado de forma directa, o en caso de que se necesite consultar los CFDI relacionados para poder generar la cancelación.

:pushpin: ***NOTA:*** El servicio de consulta es de tipo SOAP y es proporcionado directamente por parte del SAT.

Este método recibe los siguientes parámetros:
* Url Servicios SW
* Usuario y contraseña o token
* RFC Emisor
* RFC Receptor
* Total declarado en el comprobante
* UUID del comprobante
* Últimos 8 caracteres del sello digital

**Ejemplo de consumo de la librería para la consulta del estatus SAT**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Status;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Status
                //A esta le pasamos la Url, del servicio del SAT
                //Automaticamente despues de obtenerlo se procedera a consultar la factura
                Status status = new Status("https://pruebacfdiconsultaqr.cloudapp.net/ConsultaCFDIService.svc");
                var response = status.GetStatusCFDI("IVD920810GU2", "AAA010101AAA", "603.20", "249c0fb3-475a-4b72-89f9-06cd3c1f302b","oxOSjA==");
                //Para obtener el codigo status
				Console.Write(response.CodigoEstatus);
				//Para obtener si es cancelable
                Console.Write(response.EsCancelable);
                //Para obtener el estado actual
                Console.Write(response.Estado);
                //Para obtener el estatus de la cancelación
                Console.Write(response.EstatusCancelacion);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

## CFDI Relacionados ##

A través de estos siguientes métodos obtendremos un listado de los UUID que se encuentren relacionados a una factura.

<details>
<summary>
Relacionados por CSD
</summary>

<br>Este método obtendra un listado de los UUID relacionados mediante CSD

Este método recibe los siguientes parámetros:
* Url Servicios SW
* Usuario y contraseña ó token
* Certificado en base64
* Llave en base64 
* RFC del emisor 
* Contraseña del certificado 
* UUID de la factura.

**Ejemplo de consumo de la librería para la consulta de CFDI relacionados por CSD mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Relations;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Relations
                //A esta le pasamos la Url, usuario y password o token de authentication
                //Automaticamente despues de obtenerlo se procedera a consultar las facturas relacionadas
                Relations relations = new Relations("http://services.test.sw.com.mx", "user", "password");
                //Obtenemos Certificado y lo convertimos a Base 64 
                string CerB64 = Convert.ToBase64String(File.ReadAllBytes("CSD_Pruebas_CFDI_LAN7008173R5.cer")); 
                //Obtenemos LLave y lo convertimos a Base 64 
                string KeyB64 = Convert.ToBase64String(File.ReadAllBytes("CSD_Pruebas_CFDI_LAN7008173R5.key"));
                RelationsResponse response = relations.RelationsByCSD(CerB64, KeyB64, "LAN7008173R5", "12345678a", "021ea2fb-2254-4232-983b-9808c2ed831b");
                //Para obtener el status de la consulta
				Console.Write(response.status);
				//Para obtener el codigoStatus
                Console.WriteLine(response.codStatus);
                //Para obtener el uuid consultado
	            Console.WriteLine(response.data.uuidConsultado);
	            //Para obtener el resultado de la consulta
	            Console.WriteLine(response.data.resultado);
	            //Para obtener los uuid padres
	            Console.WriteLine(response.data.uuidsRelacionadosPadres);
	            //Para obtener los uuid hijo
	            Console.WriteLine(response.data.uuidsRelacionadosHijos);
	            //En caso de error se pueden consultar los siguientes campos
	            Console.WriteLine(response.message);
	            Console.WriteLine(response.messageDetail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
<summary>
Relacionados por PFX
</summary>

<br>Este método obtendra un listado de los UUID relacionados mediante PFX.

Este método recibe los siguientes parámetros:
* Url Servicios SW
* Usuario y contraseña ó token
* UUID del comprobante
* RFC del emisor
* Archivo Pfx en Base64
* Contraseña del certificado

**Ejemplo de consumo de la librería para la consulta CFDI relacionados por PFX mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Relations;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Relations
                //A esta le pasamos la Url, usuario y password o token de authentication
                //Automaticamente despues de obtenerlo se procedera a consultar las facturas relacionadas
                Relations relations = new Relations("http://services.test.sw.com.mx", "user", "password");
                //Convertimos el PFX a base 64
                string pfxB64 = Convert.ToBase64String(file.pfx);
                RelationsResponse response = relations.RelationsByPFX(pfxB64, "LAN7008173R5", "12345678a", "021ea2fb-2254-4232-983b-9808c2ed831b");
                //Para obtener el status de la consulta
				Console.Write(response.status);
				//Para obtener el codigoStatus
                Console.WriteLine(response.codStatus);
                //Para obtener el uuid consultado
	            Console.WriteLine(response.data.uuidConsultado);
	            //Para obtener el resultado de la consulta
	            Console.WriteLine(response.data.resultado);
	            //Para obtener los uuid padres
	            Console.WriteLine(response.data.uuidsRelacionadosPadres);
	            //Para obtener los uuid hijo
	            Console.WriteLine(response.data.uuidsRelacionadosHijos);
	            //En caso de error se pueden consultar los siguientes campos
	            Console.WriteLine(response.message);
	            Console.WriteLine(response.messageDetail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
<summary>
Relacionados por XML
</summary>

<br>Este método obtendra un listado de los UUID relacionados mediante el XML.

Este método recibe los siguientes parámetros:
* Url Servicios SW
* Usuario y contraseña ó token
* XML del comprobante


**Ejemplo de XML**
```xml
<?xml version="1.0" encoding="utf-8"?>
<PeticionConsultaRelacionados xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" Uuid="51BADE4D-8285-4597-A092-7DB1D50E5EFD" RfcReceptor="LAN7008173R5" RfcPacEnviaSolicitud="DAL050601L35" 
    xmlns="http://cancelacfd.sat.gob.mx">
    <Signature xmlns="http://www.w3.org/2000/09/xmldsig#">
        <SignedInfo>
            <CanonicalizationMethod Algorithm="http://www.w3.org/TR/2001/REC-xml-c14n-20010315" />
            <SignatureMethod Algorithm="http://www.w3.org/2000/09/xmldsig#rsa-sha1" />
            <Reference URI="">
                <Transforms>
                    <Transform Algorithm="http://www.w3.org/2000/09/xmldsig#enveloped-signature" />
                </Transforms>
                <DigestMethod Algorithm="http://www.w3.org/2000/09/xmldsig#sha1" />
                <DigestValue>yYGkb9DCJgiGl2O4vCf5B3gXTTI=</DigestValue>
            </Reference>
        </SignedInfo>
        <SignatureValue>VBBjMXJgS/oCb4iTazKrPmhWSICGT5wbeTf8G4tW2UuqnKBLS1NWD7Uf37kAX8+GBB04So7YlTcEw3I/X2JkHDadSxCiZ940YksNIVddmCqllJL6giMHVQJoXcTH8WQ9pO/4TbREQZ8/jxPqIvxCXrOn963PKFrZFB8eo5RQxLUa12WMi5RWgh8dSUwQxS2W3dm1XXP8bqXPOjy7GtZc3ObeTLMcXo/YoLyEAobVCnP+igOEXLxKEN2HZPzHGtA2g/5ONxuhu3UTxix9D/5ItjXdH9nk7VL0A58Xgw3qv6Q0vjmlxyu7RO0E2O3D2tLejfExt3WvsjZ8xvEKXSFp+A==</SignatureValue>
        <KeyInfo>
            <X509Data>
                <X509IssuerSerial>
                    <X509IssuerName>OID.1.2.840.113549.1.9.2=Responsable: ACDMA, OID.2.5.4.45=SAT970701NN3, L=Coyoacán, S=Distrito Federal, C=MX, PostalCode=06300, STREET="Av. Hidalgo 77, Col. Guerrero", E=asisnet@pruebas.sat.gob.mx, OU=Administración de Seguridad de la Información, O=Servicio de Administración Tributaria, CN=A.C. 2 de pruebas(4096)</X509IssuerName>
                    <X509SerialNumber>3230303031303030303030333030303232383135</X509SerialNumber>
                </X509IssuerSerial>
                <X509Certificate>MIIFxTCCA62gAwIBAgIUMjAwMDEwMDAwMDAzMDAwMjI4MTUwDQYJKoZIhvcNAQELBQAwggFmMSAwHgYDVQQDDBdBLkMuIDIgZGUgcHJ1ZWJhcyg0MDk2KTEvMC0GA1UECgwmU2VydmljaW8gZGUgQWRtaW5pc3RyYWNpw7NuIFRyaWJ1dGFyaWExODA2BgNVBAsML0FkbWluaXN0cmFjacOzbiBkZSBTZWd1cmlkYWQgZGUgbGEgSW5mb3JtYWNpw7NuMSkwJwYJKoZIhvcNAQkBFhphc2lzbmV0QHBydWViYXMuc2F0LmdvYi5teDEmMCQGA1UECQwdQXYuIEhpZGFsZ28gNzcsIENvbC4gR3VlcnJlcm8xDjAMBgNVBBEMBTA2MzAwMQswCQYDVQQGEwJNWDEZMBcGA1UECAwQRGlzdHJpdG8gRmVkZXJhbDESMBAGA1UEBwwJQ295b2Fjw6FuMRUwEwYDVQQtEwxTQVQ5NzA3MDFOTjMxITAfBgkqhkiG9w0BCQIMElJlc3BvbnNhYmxlOiBBQ0RNQTAeFw0xNjEwMjUyMTUyMTFaFw0yMDEwMjUyMTUyMTFaMIGxMRowGAYDVQQDExFDSU5ERU1FWCBTQSBERSBDVjEaMBgGA1UEKRMRQ0lOREVNRVggU0EgREUgQ1YxGjAYBgNVBAoTEUNJTkRFTUVYIFNBIERFIENWMSUwIwYDVQQtExxMQU43MDA4MTczUjUgLyBGVUFCNzcwMTE3QlhBMR4wHAYDVQQFExUgLyBGVUFCNzcwMTE3TURGUk5OMDkxFDASBgNVBAsUC1BydWViYV9DRkRJMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgvvCiCFDFVaYX7xdVRhp/38ULWto/LKDSZy1yrXKpaqFXqERJWF78YHKf3N5GBoXgzwFPuDX+5kvY5wtYNxx/Owu2shNZqFFh6EKsysQMeP5rz6kE1gFYenaPEUP9zj+h0bL3xR5aqoTsqGF24mKBLoiaK44pXBzGzgsxZishVJVM6XbzNJVonEUNbI25DhgWAd86f2aU3BmOH2K1RZx41dtTT56UsszJls4tPFODr/caWuZEuUvLp1M3nj7Dyu88mhD2f+1fA/g7kzcU/1tcpFXF/rIy93APvkU72jwvkrnprzs+SnG81+/F16ahuGsb2EZ88dKHwqxEkwzhMyTbQIDAQABox0wGzAMBgNVHRMBAf8EAjAAMAsGA1UdDwQEAwIGwDANBgkqhkiG9w0BAQsFAAOCAgEAJ/xkL8I+fpilZP+9aO8n93+20XxVomLJjeSL+Ng2ErL2GgatpLuN5JknFBkZAhxVIgMaTS23zzk1RLtRaYvH83lBH5E+M+kEjFGp14Fne1iV2Pm3vL4jeLmzHgY1Kf5HmeVrrp4PU7WQg16VpyHaJ/eonPNiEBUjcyQ1iFfkzJmnSJvDGtfQK2TiEolDJApYv0OWdm4is9Bsfi9j6lI9/T6MNZ+/LM2L/t72Vau4r7m94JDEzaO3A0wHAtQ97fjBfBiO5M8AEISAV7eZidIl3iaJJHkQbBYiiW2gikreUZKPUX0HmlnIqqQcBJhWKRu6Nqk6aZBTETLLpGrvF9OArV1JSsbdw/ZH+P88RAt5em5/gjwwtFlNHyiKG5w+UFpaZOK3gZP0su0sa6dlPeQ9EL4JlFkGqQCgSQ+NOsXqaOavgoP5VLykLwuGnwIUnuhBTVeDbzpgrg9LuF5dYp/zs+Y9ScJqe5VMAagLSYTShNtN8luV7LvxF9pgWwZdcM7lUwqJmUddCiZqdngg3vzTactMToG16gZA4CWnMgbU4E+r541+FNMpgAZNvs2CiW/eApfaaQojsZEAHDsDv4L5n3M1CC7fYjE/d61aSng1LaO6T1mh+dEfPvLzp7zyzz+UgWMhi5Cs4pcXx1eic5r7uxPoBwcCTt3YI1jKVVnV7/w=</X509Certificate>
            </X509Data>
        </KeyInfo>
    </Signature>
</PeticionConsultaRelacionados>
```

**Ejemplo de consumo de la librería para la consulta CFDI relacionados por XML mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Relations;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Relations
                //A esta le pasamos la Url, usuario y password o token de authentication
                //Automaticamente despues de obtenerlo se procedera a consultar las facturas relacionadas
                Relations relations = new Relations("http://services.test.sw.com.mx", "user", "password");
                RelationsResponse response = relations.RelationsByXML(XML);
                //Para obtener el status de la consulta
				Console.Write(response.status);
				//Para obtener el codigoStatus
                Console.WriteLine(response.codStatus);
                //Para obtener el uuid consultado
	            Console.WriteLine(response.data.uuidConsultado);
	            //Para obtener el resultado de la consulta
	            Console.WriteLine(response.data.resultado);
	            //Para obtener los uuid padres
	            Console.WriteLine(response.data.uuidsRelacionadosPadres);
	            //Para obtener los uuid hijo
	            Console.WriteLine(response.data.uuidsRelacionadosHijos);
	            //En caso de error se pueden consultar los siguientes campos
	            Console.WriteLine(response.message);
	            Console.WriteLine(response.messageDetail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
<summary>
Relacionados por UUID
</summary>

<br>Este método obtendra un listado de los UUID relacionados mediante el UUID de la factura.

Este método recibe los siguientes parámetros:
* Url Servicios SW
* Usuario y contraseña ò token
* UUID de la factura que ser requiere consultar relacionados
* RFC del emisor

:pushpin: ***NOTA:*** El usuario deberá tener sus certificados en el administrador de timbres para la utilización de este método.

**Ejemplo de consumo de la librería para la consulta CFDI relacionados por UUID mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Relations;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Relations
                //A esta le pasamos la Url, usuario y password o token de authentication
                //Automaticamente despues de obtenerlo se procedera a consultar las facturas relacionadas
                Relations relations = new Relations("http://services.test.sw.com.mx", "user", "password");
                RelationsResponse response = relations.RelationsByRfcUuid("LAN7008173R5", "01724196-ac5a-4735-b621-e3b42bcbb459");
                //Para obtener el status de la consulta
				Console.Write(response.status);
				//Para obtener el codigoStatus
                Console.WriteLine(response.codStatus);
                //Para obtener el uuid consultado
	            Console.WriteLine(response.data.uuidConsultado);
	            //Para obtener el resultado de la consulta
	            Console.WriteLine(response.data.resultado);
	            //Para obtener los uuid padres
	            Console.WriteLine(response.data.uuidsRelacionadosPadres);
	            //Para obtener los uuid hijo
	            Console.WriteLine(response.data.uuidsRelacionadosHijos);
	            //En caso de error se pueden consultar los siguientes campos
	            Console.WriteLine(response.message);
	            Console.WriteLine(response.messageDetail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

## Consulta solicitudes pendientes Aceptar / Rechazar ##
A través de este método obtendremos una lista de los UUID que tenemos pendientes por aceptar o rechazar.

<details>
  <summary>Ejemplos</summary>

<br>Este método recibe los siguientes parámetros:
* Url Servicios SW
* Usuario y contraseña ò token
* RFC Receptor


**Ejemplo de consumo de la librería para la consulta de solicitudes pendientes mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Pendings;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Pending
                //A esta le pasamos la Url, usuario y password o token de authentication
                //Automaticamente despues de obtenerlo se procedera a consultar las facturas relacionadas
                Pending pendientes = new Pending("http://services.test.sw.com.mx", "user", "password");
                PendingsResponse response = pendientes.PendingsByRfc("LAN7008173R5");
                //Para obtener el status de la consulta
				Console.Write(response.status);
				//Para obtener el codigoStatus
                Console.WriteLine(response.codStatus);
                //Para obtener la lista de uuid's
	            Console.WriteLine(response.data.uuid);
	            //En caso de error se pueden consultar los siguientes campos
	            Console.WriteLine(response.message);
	            Console.WriteLine(response.messageDetail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

## Aceptar / Rechazar ##
A través de estos siguientes métodos aceptaremos o rechazaremos los UUID solicitados para el proceso de cancelación.

<details>
<summary>
Aceptar / Rechazar por CSD
</summary>

<br>Método mediante el cual el receptor podrá manifestar la aceptación o rechazo de la solicitud de cancelación mediante CSD.

Este método recibe los siguientes parámetros:
* Url Servicios SW
* Usuario y contraseña ò token
* Certificado del receptor en **Base64**
* Llave(key) del receptor en **Base64**
* RFC del emisor
* Contraseña del certificado
* Arreglo de objetos donde se especifican los UUID y acción a realizar

**Ejemplo de consumo de la librería para la aceptacion/rechazo de la solicitud por CSD mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.AcceptReject;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AcceptReject
                //A esta le pasamos la Url, usuario y password o token de authentication
                //Automaticamente despues de obtenerlo se procedera a procesar las facturas con su acción
                AcceptReject acceptReject = new AcceptReject ("http://services.test.sw.com.mx", "user", "password");
                //Obtenemos Certificado y lo convertimos a Base 64 
                string CerB64 = Convert.ToBase64String(File.ReadAllBytes("CSD_Pruebas_CFDI_LAN7008173R5.cer")); 
                //Obtenemos LLave y lo convertimos a Base 64 
                string KeyB64 = Convert.ToBase64String(File.ReadAllBytes("CSD_Pruebas_CFDI_LAN7008173R5.key"));
                AcceptRejectResponse response = acceptReject.AcceptByCSD(CerB64, KeyB64, "LAN7008173R5", "12345678a", new AceptacionRechazoItem[] { new AceptacionRechazoItem() { uuid = "01724196-ac5a-4735-b621-e3b42bcbb459", action = EnumAcceptReject.Aceptacion } });
                //Para obtener el status de la consulta
				Console.Write(response.status);
				//Para obtener el codigoStatus
                Console.WriteLine(response.codStatus);
                //Para obtener una lista con los folios
	            Console.WriteLine(response.data.folios);
	            //Para obtener el acuse
	            Console.WriteLine(response.data.acuse);
	            //En caso de error se pueden consultar los siguientes campos
	            Console.WriteLine(response.message);
	            Console.WriteLine(response.messageDetail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
<summary>
Aceptar / Rechazar por PFX
</summary>

<br>Método mediante el cual el receptor podrá manifestar la aceptación o rechazo de la solicitud de cancelación mediante PFX.

Este método recibe los siguientes parámetros:
* Url Servicios SW
* Usuario y contraseña ó token
* Archivo Pfx en **Base64**
* Contraseña del certificado
* RFC del emisor
* Arreglo de objetos donde se especifican los UUID y acción a realizar

**Ejemplo de consumo de la librería para la aceptación/rechazo de la solicitud por PFX mediante usuario y contraseña**

```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.AcceptReject;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AcceptReject
                //A esta le pasamos la Url, usuario y password o token de authentication
                //Automaticamente despues de obtenerlo se procedera a procesar las facturas con su acción
                AcceptReject acceptReject = new AcceptReject ("http://services.test.sw.com.mx", "user", "password");
                //Obtenemos Certificado y lo convertimos a Base 64 
                string pfxB64 = Convert.ToBase64String(File.ReadAllBytes("file.pfx")); 
                AcceptRejectResponse response = acceptReject.AcceptByPFX(pfxB64,  "LAN7008173R5", "12345678a", new AceptacionRechazoItem[] { new AceptacionRechazoItem() { uuid = "01724196-ac5a-4735-b621-e3b42bcbb459", action = EnumAcceptReject.Aceptacion } });
                //Para obtener el status de la consulta
				Console.Write(response.status);
				//Para obtener el codigoStatus
                Console.WriteLine(response.codStatus);
                //Para obtener una lista con los folios
	            Console.WriteLine(response.data.folios);
	            //Para obtener el acuse
	            Console.WriteLine(response.data.acuse);
	            //En caso de error se pueden consultar los siguientes campos
	            Console.WriteLine(response.message);
	            Console.WriteLine(response.messageDetail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
<summary>
Aceptar / Rechazar por XML
</summary>

<br>Método mediante el cual el receptor podrá manifestar la aceptación o rechazo de la solicitud de cancelación mediante XML.

Este método recibe los siguientes parámetros:
* Url Servicios SW
* Usuario y contraseña ó token
* XML con datos requeridos para la aceptación/rechazo de la cancelación

**Ejemplo de XML**
```xml
<?xml version='1.0' encoding='utf-8'?>
<SolicitudAceptacionRechazo xmlns:xsd='http://www.w3.org/2001/XMLSchema' 
    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' RfcReceptor='LAN7008173R5' RfcPacEnviaSolicitud='DAL050601L35' Fecha='2018-08-22T18:38:05' 
    xmlns='http://cancelacfd.sat.gob.mx'>
    <Folios>
        <UUID>06a46e4b-b154-4c12-bb77-f9a63ed55ff2</UUID>
        <Respuesta>Aceptacion</Respuesta>
    </Folios>
    <Signature xmlns='http://www.w3.org/2000/09/xmldsig#'>
        <SignedInfo>
            <CanonicalizationMethod Algorithm='http://www.w3.org/TR/2001/REC-xml-c14n-20010315' />
            <SignatureMethod Algorithm='http://www.w3.org/2000/09/xmldsig#rsa-sha1' />
            <Reference URI=''>
                <Transforms>
                    <Transform Algorithm='http://www.w3.org/2000/09/xmldsig#enveloped-signature' />
                </Transforms>
                <DigestMethod Algorithm='http://www.w3.org/2000/09/xmldsig#sha1' />
                <DigestValue>AQ36cbqKJKHy5vaS6GhDTWtwKE4=</DigestValue>
            </Reference>
        </SignedInfo>
        <SignatureValue>HVlFUPmRLyxeztem827eaasDObRXi+oqedCNNvDyMsRizqsS99cHt5mJCEE4vWgpDGPGLrph/yd++R4aN+V562DPp9qreFkisFpEvJy5Z8o/KzG7vc5qqaD8z9ohPpRERPHvxFrIm3ryEBqnSV6zqJG02PuxkWvYonVc+B7RdsO5iAiDTMs9guUhOvHBK8BVXQHKCbUAPCp/4YepZ4LUkcdloCAMPsN0x9GaUty2RMtNJuwaRWy+5IIBUCeXXZmQhoQfS0QfPpCByt0ago5v+FocJQiYQrsUV/8mesmNw5JoOCmufQYliQFyZgsstV8+h76dU/rwLr6R8YlFOkTxKg==</SignatureValue>
        <KeyInfo>
            <X509Data>
                <X509IssuerSerial>
                    <X509IssuerName>OID.1.2.840.113549.1.9.2=Responsable: ACDMA, OID.2.5.4.45=SAT970701NN3, L=Coyoacán, S=Distrito Federal, C=MX, PostalCode=06300, STREET='Av. Hidalgo 77, Col. Guerrero', E=asisnet@pruebas.sat.gob.mx, OU=Administración de Seguridad de la Información, O=Servicio de Administración Tributaria, CN=A.C. 2 de pruebas(4096)</X509IssuerName>
                    <X509SerialNumber>3230303031303030303030333030303232383135</X509SerialNumber>
                </X509IssuerSerial>
                <X509Certificate>MIIFxTCCA62gAwIBAgIUMjAwMDEwMDAwMDAzMDAwMjI4MTUwDQYJKoZIhvcNAQELBQAwggFmMSAwHgYDVQQDDBdBLkMuIDIgZGUgcHJ1ZWJhcyg0MDk2KTEvMC0GA1UECgwmU2VydmljaW8gZGUgQWRtaW5pc3RyYWNpw7NuIFRyaWJ1dGFyaWExODA2BgNVBAsML0FkbWluaXN0cmFjacOzbiBkZSBTZWd1cmlkYWQgZGUgbGEgSW5mb3JtYWNpw7NuMSkwJwYJKoZIhvcNAQkBFhphc2lzbmV0QHBydWViYXMuc2F0LmdvYi5teDEmMCQGA1UECQwdQXYuIEhpZGFsZ28gNzcsIENvbC4gR3VlcnJlcm8xDjAMBgNVBBEMBTA2MzAwMQswCQYDVQQGEwJNWDEZMBcGA1UECAwQRGlzdHJpdG8gRmVkZXJhbDESMBAGA1UEBwwJQ295b2Fjw6FuMRUwEwYDVQQtEwxTQVQ5NzA3MDFOTjMxITAfBgkqhkiG9w0BCQIMElJlc3BvbnNhYmxlOiBBQ0RNQTAeFw0xNjEwMjUyMTUyMTFaFw0yMDEwMjUyMTUyMTFaMIGxMRowGAYDVQQDExFDSU5ERU1FWCBTQSBERSBDVjEaMBgGA1UEKRMRQ0lOREVNRVggU0EgREUgQ1YxGjAYBgNVBAoTEUNJTkRFTUVYIFNBIERFIENWMSUwIwYDVQQtExxMQU43MDA4MTczUjUgLyBGVUFCNzcwMTE3QlhBMR4wHAYDVQQFExUgLyBGVUFCNzcwMTE3TURGUk5OMDkxFDASBgNVBAsUC1BydWViYV9DRkRJMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgvvCiCFDFVaYX7xdVRhp/38ULWto/LKDSZy1yrXKpaqFXqERJWF78YHKf3N5GBoXgzwFPuDX+5kvY5wtYNxx/Owu2shNZqFFh6EKsysQMeP5rz6kE1gFYenaPEUP9zj+h0bL3xR5aqoTsqGF24mKBLoiaK44pXBzGzgsxZishVJVM6XbzNJVonEUNbI25DhgWAd86f2aU3BmOH2K1RZx41dtTT56UsszJls4tPFODr/caWuZEuUvLp1M3nj7Dyu88mhD2f+1fA/g7kzcU/1tcpFXF/rIy93APvkU72jwvkrnprzs+SnG81+/F16ahuGsb2EZ88dKHwqxEkwzhMyTbQIDAQABox0wGzAMBgNVHRMBAf8EAjAAMAsGA1UdDwQEAwIGwDANBgkqhkiG9w0BAQsFAAOCAgEAJ/xkL8I+fpilZP+9aO8n93+20XxVomLJjeSL+Ng2ErL2GgatpLuN5JknFBkZAhxVIgMaTS23zzk1RLtRaYvH83lBH5E+M+kEjFGp14Fne1iV2Pm3vL4jeLmzHgY1Kf5HmeVrrp4PU7WQg16VpyHaJ/eonPNiEBUjcyQ1iFfkzJmnSJvDGtfQK2TiEolDJApYv0OWdm4is9Bsfi9j6lI9/T6MNZ+/LM2L/t72Vau4r7m94JDEzaO3A0wHAtQ97fjBfBiO5M8AEISAV7eZidIl3iaJJHkQbBYiiW2gikreUZKPUX0HmlnIqqQcBJhWKRu6Nqk6aZBTETLLpGrvF9OArV1JSsbdw/ZH+P88RAt5em5/gjwwtFlNHyiKG5w+UFpaZOK3gZP0su0sa6dlPeQ9EL4JlFkGqQCgSQ+NOsXqaOavgoP5VLykLwuGnwIUnuhBTVeDbzpgrg9LuF5dYp/zs+Y9ScJqe5VMAagLSYTShNtN8luV7LvxF9pgWwZdcM7lUwqJmUddCiZqdngg3vzTactMToG16gZA4CWnMgbU4E+r541+FNMpgAZNvs2CiW/eApfaaQojsZEAHDsDv4L5n3M1CC7fYjE/d61aSng1LaO6T1mh+dEfPvLzp7zyzz+UgWMhi5Cs4pcXx1eic5r7uxPoBwcCTt3YI1jKVVnV7/w=</X509Certificate>
            </X509Data>
        </KeyInfo>
    </Signature>
</SolicitudAceptacionRechazo>
```

**Ejemplo de consumo de la librería para la aceptación/rechazo de la solicitud por XML mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.AcceptReject;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AcceptReject
                //A esta le pasamos la Url, usuario y password o token de authentication
                //Automaticamente despues de obtenerlo se procedera a procesar las facturas con su acción
                AcceptReject acceptReject = new AcceptReject ("http://services.test.sw.com.mx", "user", "password");
                AcceptRejectResponse response = acceptReject.AcceptByXML(Encoding.UTF8.GetBytes(acuse), EnumAcceptReject.Aceptacion);
                //Para obtener el status de la consulta
				Console.Write(response.status);
				//Para obtener el codigoStatus
                Console.WriteLine(response.codStatus);
                //Para obtener una lista con los folios
	            Console.WriteLine(response.data.folios);
	            //Para obtener el acuse
	            Console.WriteLine(response.data.acuse);
	            //En caso de error se pueden consultar los siguientes campos
	            Console.WriteLine(response.message);
	            Console.WriteLine(response.messageDetail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
<summary>
Aceptar / Rechazar por UUID
</summary>

<br>Método mediante el cual el receptor podrá manifestar la aceptación o rechazo de la solicitud de cancelación mediante UUID.

Este método recibe los siguientes parámetros:
* Url Servicios SW
* Usuario y contraseña ó token
* RFC del receptor
* UUID de la factura que se requiere aceptar/rechazar
* Acción que se requiera realizar Aceptación/Rechazo

:pushpin: ***NOTA:*** El usuario deberá tener sus certificados en el administrador de timbres para la utilización de este método.

**Ejemplo de consumo de la librería para la aceptación/rechazo de la solicitud por UUID mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.AcceptReject;
namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo AcceptReject
                //A esta le pasamos la Url, usuario y password o token de authentication
                //Automaticamente despues de obtenerlo se procedera a procesar las facturas con su acción
                AcceptReject acceptReject = new AcceptReject ("http://services.test.sw.com.mx", "user", "password");
                AcceptRejectResponse response = acceptReject.AcceptByRfcUuid("LAN7008173R5", "01724196-ac5a-4735-b621-e3b42bcbb459", EnumAcceptReject.Aceptacion);
                //Para obtener el status de la consulta
				Console.Write(response.status);
				//Para obtener el codigoStatus
                Console.WriteLine(response.codStatus);
                //Para obtener una lista con los folios
	            Console.WriteLine(response.data.folios);
	            //Para obtener el acuse
	            Console.WriteLine(response.data.acuse);
	            //En caso de error se pueden consultar los siguientes campos
	            Console.WriteLine(response.message);
	            Console.WriteLine(response.messageDetail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>
</details>


## Certificados ##
Servicio para gestionar los certificados CSD de tu cuenta, será posible cargar, consultar y eliminar los certificados. Para administrar los certificados de manera gráfica, puede hacerlo desde el [Administrador de timbres](https://portal.sw.com.mx/).

<details>
  <summary>Cargar certificado</summary>
Método para cargar un certificado en la cuenta.

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token
* Url Servicios SW
* CSD en Base64
* Key en Base64
* Contraseña del certificado

**Ejemplo de consumo de la libreria para la carga de un certificado**
```cs
using SW.Services.Csd;
using System;
using System.IO;

namespace ExampleSDKS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string cerb64 = Convert.ToBase64String(File.ReadAllBytes("Resources/CertificadosDePrueba/EKU9003173C9.cer"));
                string keyb64 =Convert.ToBase64String(File.ReadAllBytes("Resources/CertificadosDePrueba/EKU9003173C9.key"));
                string cerPassword = "12345678a";
                //Creamos una instancia de tipo CsdUtils esta le pasamos la Url y credenciales para autenticarnos
                CsdUtils csd = new CsdUtils("http://services.test.sw.com.mx", "user", "password");
                //Obtenemos la respuesta del metodo UploadMyCsd con los datos del CSD que requieres agregar a tu cuenta **Nota: type (Default="stamp"), is_active (Default=true)
                var response = csd.UploadMyCsd(cerb64, keyb64, cerPassword, "stamp", true);
                //Mostramos el mensaje
                Console.WriteLine(response.data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>
<details>
  <summary>Consultar certificados</summary>
Servicio para consultar todos los certificados cargados en la cuenta.

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token
* Url Servicios SW

**Ejemplo de consumo de la libreria para consulta de certificados**
```cs
using SW.Services.Csd;
using System;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo CsdUtils esta le pasamos la Url y credenciales para autenticarnos
                CsdUtils csd = new CsdUtils("http://services.test.sw.com.mx", "user", "password");
                //Obtenemos la respuesta del metodo GetListCsd, así en el response obtendremos la lista de CSD cargados previamente
                var response = csd.GetListCsd();
                //Mostramos el mensaje
                Console.WriteLine(response.data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>
<details>
  <summary>Consultar certificado por NoCertificado</summary>
Método para obtener un certificado cargado enviando como parámetro el número de certificado.

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token
* Url Servicios SW
* Número de certificado a obtener

**Ejemplo de consumo de la libreria para consulta de un certificado por su NoCertificado**
```cs
using SW.Services.Csd;
using System;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo CsdUtils esta le pasamos la Url y credenciales para autenticarnos
                CsdUtils csd = new CsdUtils("http://services.test.sw.com.mx", "user", "password");
                //Obtenemos la respuesta del metodo SearchMyCsd con el parametro del Numero de certificado
                var response = csd.SearchMyCsd("20001000000300022816");
                //Mostramos el mensaje
                Console.WriteLine(response.data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>
<details>
  <summary>Consultar certificado por RFC</summary>
Método para obtener un certificado cargado enviando como parámetro el RFC.

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token
* Url Servicios SW
* RFC de certificado a obtener

**Ejemplo de consumo de la libreria para consulta de un certificado por su RFC**
```cs
using SW.Services.Csd;
using System;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo CsdUtils esta le pasamos la Url y credenciales para autenticarnos
                CsdUtils csd = new CsdUtils("http://services.test.sw.com.mx", "user", "password");
                //Obtenemos la respuesta del metodo GetListCsdByRfc con el parametro del RFC
                var response = csd.GetListCsdByRfc("XAXX010101000");
                //Mostramos el mensaje
                Console.WriteLine(response.data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>
<details>
  <summary>Eliminar certificado</summary>
Método para eliminar un certificado de la cuenta.

<br>Este método recibe los siguientes parametros:
* Usuario y contraseña o Token
* Url Servicios SW
* Número de certificado a obtener

**Ejemplo de consumo de la libreria para eliminar certificado**
```cs
using SW.Services.Csd;
using System;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo CsdUtils esta le pasamos la Url y credenciales para autenticarnos
                CsdUtils csd = new CsdUtils("http://services.test.sw.com.mx", "user", "password");
                //Obtenemos la respuesta del metodo DisableMyCsd enviando el parametro Numero de certificado
                var response = csd.DisableMyCsd("20001000000300022816");
                //Mostramos el mensaje
                Console.WriteLine(response.data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>


## Recuperar XML por UUID ##
Servicio para recuperar información de un XML timbrado con SW.

<details>
<summary>Recuperar XML por UUID</summary>

<br>Este método recibe los siguientes parametros:
* Url Api SW
* UUID del XML a encontrar

**Ejemplo de consumo de la libreria recuperar XML por UUID con token**
```cs
using SW.Services.Storage;
using System;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                 //Instanciamos el servicio Storage para usarlo con token como medio de autenticacion
                Storage storage = new Storage("https://api.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Obtenemos la respuesta del metodo GetByUUID enviando el parametro UUID para buscar el XML
                var response = storage.GetByUUID(new Guid("7354cc1f-3fb0-4808-ae90-fdc5d346eca3"));         
                //Mostramos el mensaje
                Console.WriteLine(response.data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
**Ejemplo de consumo de la libreria recuperar XML por UUID con usuario**
```cs
using SW.Services.Storage;
using System;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                 //Instanciamos el servicio Storage para usarlo mediante usuario y contraseña.
                Storage storage = new Storage("https://api.test.sw.com.mx","http://services.test.sw.com.mx","user","password");
                //Obtenemos la respuesta del metodo GetByUUID enviando el parametro UUID para buscar el XML
                var response = storage.GetByUUID(new Guid("7354cc1f-3fb0-4808-ae90-fdc5d346eca3"));         
                //Mostramos el mensaje
                Console.WriteLine(response.data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

## TimbradoV4 ##

### **Email** ###

Este servicio recibe un comprobante CFDI para ser timbrado y recibe un listado de uno o hasta 5 correos electrónicos a los que se requiera enviar el XML timbrado.

Existen varias versiones de respuesta a este método, las cuales puede consultar mas a detalle en el siguiente [link](https://developers.sw.com.mx/knowledge-base/versiones-de-respuesta-timbrado/).

<details>
  <summary>Timbrado CFDI (StampV4)</summary>

**<br>Ejemplo del consumo de la librería para el servicio StampV4(Email) XML en formato string enviando 1 correo mediante usuario y contraseña**
```cs
using SW.Services.Stamp;
using System;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //obtenemos el XML
                var xml = Encoding.UTF8.GetString(File.ReadAllBytes(file));
                //creamos la variable con el correo
                string email = "ejemplo@gmail.com";
                //Creamos una instancia de tipo StampV4 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a timbrar el XML
                StampV4 stamp = new StampV4("http://services.test.sw.com.mx", "user", "password");
                var response = (StampResponseV1)stamp.TimbrarV1(xml, email, null);
                if(response.status == "success")
                {
                    Console.WriteLine(response.data.tfd);
                }
                else if(response.status == "error")
                {
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**<br>Ejemplo del consumo de la librería para el servicio StampV4(Email) XML en formato string enviando 1 correo mediante token**[¿Como obtener token?](http://developers.sw.com.mx/knowledge-base/generar-un-token-infinito/)
```cs
using SW.Services.Stamp;
using System;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //obtenemos el XML
                var xml = Encoding.UTF8.GetString(File.ReadAllBytes(file));
                //creamos la variable con el correo
                string email = "ejemplo@gmail.com";
                //Creamos una instancia de tipo StampV4 
                //A esta le pasamos la Url y el token.
                StampV4 stamp = new StampV4("http://services.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Realizamos la peticion
                var response = (StampResponseV1)stamp.TimbrarV1(xml, email, null);
                if(response.status == "success")
                {
                    Console.WriteLine(response.data.tfd);
                }
                else if(response.status == "error")
                {
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**<br>Ejemplo del consumo de la librería para el servicio StampV4(Email) XML en formato string enviando varios correos mediante token**[¿Como obtener token?](http://developers.sw.com.mx/knowledge-base/generar-un-token-infinito/)
```cs
using SW.Services.Stamp;
using System;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //obtenemos el XML
                var xml = Encoding.UTF8.GetString(File.ReadAllBytes(file));
                //creamos la variable con los correos (Max. 5)
                string emails = "ejemplo1@gmail.com,ejemplo2@gmail.com,ejemplo3@gmail.com,ejemplo4@gmail.com,ejemplo5@gmail.com";
                //Creamos una instancia de tipo StampV4 
                //A esta le pasamos la Url y el token.
                StampV4 stamp = new StampV4("http://services.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Realizamos la peticion
                var response = (StampResponseV1)stamp.TimbrarV1(xml, emails, null);
                if(response.status == "success")
                {
                    Console.WriteLine(response.data.tfd);
                }
                else if(response.status == "error")
                {
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

</details>
<details>
  <summary>Timbrado Json (IssueJsonV4)</summary>

  **Ejemplo del consumo de la librería para el servicio IssueJsonV4 (Email) Json en formato string mediante usuario y contraseña.**
```cs
using SW.Services.Stamp;
using SW.Services.Issue;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSDK
{
    class Program
    {
        static async Task Main(string[] args)
        {
          try
            {
                //obtenemos el archivo Json
                var json = Encoding.UTF8.GetString(File.ReadAllBytes(file));
                //creamos el listado de correos(máximo 5)
                string[] email = { "prueba@test.com", "someone@email.com" };
                //Creamos una instancia de tipo IssueJsonV4 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                IssueJsonV4 stamp = new IssueJsonV4("http://services.test.sw.com.mx", "user", "password");
                //Realizamos la petición
                var response =  (StampResponseV1)issue.TimbrarJsonV1(json, email);
                Console.WriteLine(response.Status);
                Console.WriteLine(response.Data.Tfd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
**Ejemplo del consumo de la librería para el servicio IssueJsonV4 (Email) Json en formato string mediante token.**
```cs
using SW.Services.Stamp;
using SW.Services.Issue;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSDK
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //obtenemos el archivo Json
                var json = Encoding.UTF8.GetString(File.ReadAllBytes(file));
                //creamos el listado de correos(máximo 5)
                string[] email = { "prueba@test.com", "someone@email.com" };
                //Creamos una instancia de tipo IssueJsonV4 
                //A esta le pasamos la Url y el token
                IssueJsonV4 stamp = new IssueJsonV4("http://services.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Realizamos la peticion
                var response = (StampResponseV1)issue.TimbrarJsonV1(json, email);
                Console.WriteLine(response.Status);
                Console.WriteLine(response.Data.Tfd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

### **CustomId** ###
Este servicio recibe un comprobante CFDI para ser timbrado y que recibe un header conocido como CustomID, el cuál tiene el objetivo de agregar un filtro adicional al timbrado para evitar la duplicidad de timbrado.
El CustomId es un string y el valor es asignado por el usuario, el cual tiene un límite de 100 caracteres.

Existen varias versiones de respuesta a este método, las cuales puede consultar mas a detalle en el siguiente [link](https://developers.sw.com.mx/knowledge-base/versiones-de-respuesta-timbrado/).

<details>
  <summary>Timbrado CFDI (StampV4)</summary>

**<br>Ejemplo del consumo de la librería para el servicio StampV4(CustomId) XML en formato string mediante usuario y contraseña**
```cs
using SW.Services.Stamp;
using System;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //obtenemos el XML
                var xml = Encoding.UTF8.GetString(File.ReadAllBytes(file));
                //creamos la variable de nuestro customId
                string CustomId = "RandomCustomId_Max100Char";
                //Creamos una instancia de tipo StampV4 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a timbrar el XML
                StampV4 stamp = new StampV4("http://services.test.sw.com.mx", "user", "password");
                var response = (StampResponseV1)stamp.TimbrarV1(xml, null, CustomId);
                if(response.status == "success")
                {
                    Console.WriteLine(response.data.tfd);
                }
                else if(response.status == "error")
                {
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**<br>Ejemplo del consumo de la librería para el servicio StampV4(CustomId) XML en formato string mediante token** [¿Como obtener token?](http://developers.sw.com.mx/knowledge-base/generar-un-token-infinito/)
```cs
```cs
using SW.Services.Stamp;
using System;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //obtenemos el XML
                var xml = Encoding.UTF8.GetString(File.ReadAllBytes(file));
                //creamos la variable de nuestro customId
                string CustomId = "RandomCustomId_Max100Char";
                //Creamos una instancia de tipo StampV4 
                //A esta le pasamos la Url y el token.
                StampV4 stamp = new StampV4("http://services.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Realizamos la peticion
                var response = (StampResponseV1)stamp.TimbrarV1(xml, null, CustomId);
                if(response.status == "success")
                {
                    Console.WriteLine(response.data.tfd);
                }
                else if(response.status == "error")
                {
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**<br>Ejemplo del consumo de la librería para el servicio StampV4(CustomId) XML en Base64 mediante token** [¿Como obtener token?](http://developers.sw.com.mx/knowledge-base/generar-un-token-infinito/)
```cs
```cs
using SW.Services.Stamp;
using System;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //obtenemos el XML
                var xml = Convert.ToBase64String(Encoding.UTF8.GetBytes("file.xml"));
                //creamos la variable de nuestro customId
                string CustomId = "RandomCustomId_Max100Char";
                //Creamos una instancia de tipo StampV4 
                //A esta le pasamos la Url y el token.
                StampV4 stamp = new StampV4("http://services.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Realizamos la peticion
                var response = (StampResponseV1)stamp.TimbrarV1(xml, null, CustomId,true);
                if(response.status == "success")
                {
                    Console.WriteLine(response.data.tfd);
                }
                else if(response.status == "error")
                {
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
 <summary>Timbrado Json (IssueJsonV4)</summary>

 **Ejemplo del consumo de la librería para el servicio IssueJsonV4 (CustomId) Json en formato string mediante usuario y contraseña.**
```cs
using SW.Services.Stamp;
using SW.Services.Issue;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSDK
{
    class Program
    {
        static async Task Main(string[] args)
        {
          try
            {
                //obtenemos el archivo Json
                var json = Encoding.UTF8.GetString(File.ReadAllBytes(file));
                //creamos la variable de nuestro customId
                var customId = Guid.NewGuid().ToString();
                //Creamos una instancia de tipo IssueJsonV4 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                IssueJsonV4 stamp = new IssueJsonV4("http://services.test.sw.com.mx", "user", "password");
                //Realizamos la petición
                var response =  (StampResponseV1)issue.TimbrarJsonV1(json, null, customId);
                Console.WriteLine(response.Status);
                Console.WriteLine(response.Data.Tfd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
**Ejemplo del consumo de la librería para el servicio IssueJsonV4 (CustomId) Json en formato string mediante token.**[¿Como obtener token?](http://developers.sw.com.mx/knowledge-base/generar-un-token-infinito/)
```cs
using SW.Services.Stamp;
using SW.Services.Issue;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSDK
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //obtenemos el archivo Json
                var json = Encoding.UTF8.GetString(File.ReadAllBytes(file));
                //creamos la variable de nuestro customId
                var customId = Guid.NewGuid().ToString();
                //Creamos una instancia de tipo IssueJsonV4 
                //A esta le pasamos la Url y el token
                IssueJsonV4 stamp = new IssueJsonV4("http://services.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Realizamos la peticion
                var response = (StampResponseV1)issue.TimbrarJsonV1(json, null, customId);
                Console.WriteLine(response.Status);
                Console.WriteLine(response.Data.Tfd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

### **PDF** ###
Este servicio recibe un comprobante CFDI para ser timbrado y que recibe un header conocido como extra mediante el cual se confirma la generación de un PDF del CFDI timbrado que será guardado en automático en el ADT.

Existen varias versiones de respuesta a este método, las cuales puede consultar mas a detalle en el siguiente [link](https://developers.sw.com.mx/knowledge-base/versiones-de-respuesta-timbrado/).

***NOTA:*** En caso de que no se cuente con una plantilla PDF customizada los PDF’s serán generados con las plantillas genéricas.

<details>
 <summary>Timbrado Json (IssueJsonV4)</summary>

 **Ejemplo del consumo de la librería para el servicio IssueJsonV4 (PDF) Json en formato string mediante usuario y contraseña.**
```cs
using SW.Services.Stamp;
using SW.Services.Issue;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSDK
{
    class Program
    {
        static async Task Main(string[] args)
        {
          try
            {
                //obtenemos el archivo Json
                var json = Encoding.UTF8.GetString(File.ReadAllBytes(file));
                //Creamos una instancia de tipo IssueJsonV4 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                IssueJsonV4 stamp = new IssueJsonV4("http://services.test.sw.com.mx", "user", "password");
                //Realizamos la petición enviando en el cuarto parametro true
                //para indicar la generación de pdf
                var response =  (StampResponseV1)issue.TimbrarJsonV1(json, null, null, true);
                Console.WriteLine(response.Status);
                Console.WriteLine(response.Data.Tfd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
**Ejemplo del consumo de la librería para el servicio IssueJsonV4 (PDF) Json en formato string mediante token.**[¿Como obtener token?](http://developers.sw.com.mx/knowledge-base/generar-un-token-infinito/)
```cs
using SW.Services.Stamp;
using SW.Services.Issue;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSDK
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //obtenemos el archivo Json
                var json = Encoding.UTF8.GetString(File.ReadAllBytes(file));
                //Creamos una instancia de tipo IssueJsonV4 
                //A esta le pasamos la Url y el token
                IssueJsonV4 stamp = new IssueJsonV4("http://services.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                //Realizamos la petición enviando en el cuarto parametro true
                //para indicar la generación de pdf
                var response = (StampResponseV1)issue.TimbrarJsonV1(json, null, null, true);
                Console.WriteLine(response.Status);
                Console.WriteLine(response.Data.Tfd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

## Timbrado Retenciones ##


<details>
<summary>
Timbrado Retenciones V3
</summary>

**TimbrarV3** Recibe el contenido de un **XML** ya emitido (sellado) en formato **String**, posteriormente si la factura y el token son correctos devuelve el CFDI timbrado, en caso contrario lanza una excepción.

Este método recibe los siguientes parámetros:
* Archivo en formato **String** ó **Base64**
* Usuario y contraseña ó Token
* Url Servicios SW

**Ejemplo de consumo de la librería para timbrar XML en formato string utilizando usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.StampRetention;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Stamp 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a timbrar el xml
                StampRetention StampRetention = new StampRetention("http://services.test.sw.com.mx", "user", "password");
                string xml = Encoding.UTF8.GetString(File.ReadAllBytes("file.xml"));
                var response = (StampRetentionResponseV3)stampRetention.TimbrarV3(xml);
                Console.WriteLine(response.status);
                Console.WriteLine(response.data.retencion);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**Ejemplo de consumo de la librería para timbrar XML en formato string utilizando token** [¿Como obtener token?](http://developers.sw.com.mx/knowledge-base/generar-un-token-infinito/)
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.StampRetention;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo Stamp 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a timbrar el xml
                StampRetention StampRetention = new StampRetention("http://services.test.sw.com.mx", "T2lYQ0t4L0R....ReplaceForRealToken");
                string xml = Encoding.UTF8.GetString(File.ReadAllBytes("file.xml"));
                var response = (StampRetentionResponseV3)stampRetention.TimbrarV3(xml);
                Console.WriteLine(response.status);
                Console.WriteLine(response.data.retencion);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

| Version |                         Respuesta                             | 
|---------|---------------------------------------------------------------|
|  V3     | Devuelve el CFDI timbrado                                     | 


## Cancelación Retenciones ##

<b>Este servicio se utiliza para cancelar facturas de retenciones y se puede hacer mediante varios métodos Cancelación.

<details>
<summary>
<b>Cancelación de retención por XML
</summary>

<br>Servicio para cancelar enviando un XML con la información y folios de las facturas a cancelar. 

Este método recibe los siguientes parámetros:
* Usuario y contraseña
* Url Servicios SW
* XML sellado con los UUID a cancelar.

**Ejemplo de XML para Cancelar retenciones**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<Cancelacion xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" Fecha="2025-08-12T05:35:05" RfcEmisor="EKU9003173C9"
	xmlns="http://www.sat.gob.mx/esquemas/retencionpago/1">
	<Folios>
		<Folio UUID="3044cc3f-572f-4535-85e2-374c205f5b11" Motivo="02" FolioSustitucion=""/>
	</Folios>
	<Signature xmlns="http://www.w3.org/2000/09/xmldsig#">
		<SignedInfo>
			<CanonicalizationMethod Algorithm="http://www.w3.org/TR/2001/REC-xml-c14n-20010315"/>
			<SignatureMethod Algorithm="http://www.w3.org/2000/09/xmldsig#rsa-sha1"/>
			<Reference URI="">
				<Transforms>
					<Transform Algorithm="http://www.w3.org/2000/09/xmldsig#enveloped-signature"/>
				</Transforms>
				<DigestMethod Algorithm="http://www.w3.org/2000/09/xmldsig#sha1"/>
				<DigestValue>kLy96SjbSI/bQpAkaSnH5wAUZ1o=</DigestValue>
			</Reference>
		</SignedInfo>
		<SignatureValue>M9aigTNqcPgFHgj16YxpsrJYg5kG6OBB7KybbOiWWNtpYSAVICUVc5tYqrwDQ6jUqVQhQ/RsbJeKv2H0CdH1mZKwAzlZHy9zjIviPMpckjTBBQdf/gHQmOxozRP3a9FsS/oFd9CbtIh6f+XAhDaVUeleRNaXsjwDz+9l5FTRTrP+clGDfEAYfc6imbpprAq6kd6jefBxzIMaHWjT0pO6LBGRVdhk6T244ZHi9xO2V1j4MQOZWz0Ra83enkfEpkA60lP1x4SBoRvwYPnlYcZ6y4lGPVrUbhk2B8mxrduNMW+1x+Lq+EDcb/3P7+/XA2Cy/QKGX3dIRsnOQpbe+N19GQ==</SignatureValue>
		<KeyInfo>
			<X509Data>
				<X509Certificate>MIIFsDCCA5igAwIBAgIUMzAwMDEwMDAwMDA1MDAwMDM0MTYwDQYJKoZIhvcNAQELBQAwggErMQ8wDQYDVQQDDAZBQyBVQVQxLjAsBgNVBAoMJVNFUlZJQ0lPIERFIEFETUlOSVNUUkFDSU9OIFRSSUJVVEFSSUExGjAYBgNVBAsMEVNBVC1JRVMgQXV0aG9yaXR5MSgwJgYJKoZIhvcNAQkBFhlvc2Nhci5tYXJ0aW5lekBzYXQuZ29iLm14MR0wGwYDVQQJDBQzcmEgY2VycmFkYSBkZSBjYWxpejEOMAwGA1UEEQwFMDYzNzAxCzAJBgNVBAYTAk1YMRkwFwYDVQQIDBBDSVVEQUQgREUgTUVYSUNPMREwDwYDVQQHDAhDT1lPQUNBTjERMA8GA1UELRMIMi41LjQuNDUxJTAjBgkqhkiG9w0BCQITFnJlc3BvbnNhYmxlOiBBQ0RNQS1TQVQwHhcNMjMwNTE4MTE0MzUxWhcNMjcwNTE4MTE0MzUxWjCB1zEnMCUGA1UEAxMeRVNDVUVMQSBLRU1QRVIgVVJHQVRFIFNBIERFIENWMScwJQYDVQQpEx5FU0NVRUxBIEtFTVBFUiBVUkdBVEUgU0EgREUgQ1YxJzAlBgNVBAoTHkVTQ1VFTEEgS0VNUEVSIFVSR0FURSBTQSBERSBDVjElMCMGA1UELRMcRUtVOTAwMzE3M0M5IC8gVkFEQTgwMDkyN0RKMzEeMBwGA1UEBRMVIC8gVkFEQTgwMDkyN0hTUlNSTDA1MRMwEQYDVQQLEwpTdWN1cnNhbCAxMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtmecO6n2GS0zL025gbHGQVxznPDICoXzR2uUngz4DqxVUC/w9cE6FxSiXm2ap8Gcjg7wmcZfm85EBaxCx/0J2u5CqnhzIoGCdhBPuhWQnIh5TLgj/X6uNquwZkKChbNe9aeFirU/JbyN7Egia9oKH9KZUsodiM/pWAH00PCtoKJ9OBcSHMq8Rqa3KKoBcfkg1ZrgueffwRLws9yOcRWLb02sDOPzGIm/jEFicVYt2Hw1qdRE5xmTZ7AGG0UHs+unkGjpCVeJ+BEBn0JPLWVvDKHZAQMj6s5Bku35+d/MyATkpOPsGT/VTnsouxekDfikJD1f7A1ZpJbqDpkJnss3vQIDAQABox0wGzAMBgNVHRMBAf8EAjAAMAsGA1UdDwQEAwIGwDANBgkqhkiG9w0BAQsFAAOCAgEAFaUgj5PqgvJigNMgtrdXZnbPfVBbukAbW4OGnUhNrA7SRAAfv2BSGk16PI0nBOr7qF2mItmBnjgEwk+DTv8Zr7w5qp7vleC6dIsZFNJoa6ZndrE/f7KO1CYruLXr5gwEkIyGfJ9NwyIagvHHMszzyHiSZIA850fWtbqtythpAliJ2jF35M5pNS+YTkRB+T6L/c6m00ymN3q9lT1rB03YywxrLreRSFZOSrbwWfg34EJbHfbFXpCSVYdJRfiVdvHnewN0r5fUlPtR9stQHyuqewzdkyb5jTTw02D2cUfL57vlPStBj7SEi3uOWvLrsiDnnCIxRMYJ2UA2ktDKHk+zWnsDmaeleSzonv2CHW42yXYPCvWi88oE1DJNYLNkIjua7MxAnkNZbScNw01A6zbLsZ3y8G6eEYnxSTRfwjd8EP4kdiHNJftm7Z4iRU7HOVh79/lRWB+gd171s3d/mI9kte3MRy6V8MMEMCAnMboGpaooYwgAmwclI2XZCczNWXfhaWe0ZS5PmytD/GDpXzkX0oEgY9K/uYo5V77NdZbGAjmyi8cE2B2ogvyaN2XfIInrZPgEffJ4AB7kFA2mwesdLOCh0BLD9itmCve3A1FGR4+stO2ANUoiI3w3Tv2yQSg4bjeDlJ08lXaaFCLW2peEXMXjQUk7fmpb5MNuOUTW6BE=</X509Certificate>
				<X509IssuerSerial>
					<X509IssuerName>CN=AC UAT, O=SERVICIO DE ADMINISTRACION TRIBUTARIA, OU=SAT-IES Authority, E=oscar.martinez@sat.gob.mx, STREET=3ra cerrada de caliz, PostalCode=06370, C=MX, ST=CIUDAD DE MEXICO, L=COYOACAN, OID.2.5.4.45=2.5.4.45, OID.1.2.840.113549.1.9.2=responsable: ACDMA-SAT</X509IssuerName>
					<X509SerialNumber>3330303031303030303030353030303033343136</X509SerialNumber>
				</X509IssuerSerial>
			</X509Data>
		</KeyInfo>
	</Signature>
</Cancelacion>
```

Para caso de motivo 01 deberá añadir el atributo "FolioSustitucion" dentro del Nodo <Folio>

Ejemplo de nodo Folio: 
```xml
<Folios>
	<Folio UUID="b374db50-a0a3-4028-9d01-32b93e2b925a" Motivo="01" FolioSustitucion="b3641a4b-7177-4323-aaa0-29bd34bf1ff8" />
</Folios>
```

**Ejemplo de consumo de la librería para cancelar con XML mediante usuario y contraseña**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.CancelationRetention;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Obtenemos el XML de cancelacion
                byte[] xml = File.ReadAllBytes("cancelacion.xml");
                //Creamos una instancia de tipo CancelationRetention 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a Cancelar el XML o CFDI
                CancelationRetention cancelationRetention = new CancelationRetention("http://services.test.sw.com.mx", "user", "password");
                CancelationResponse response = cancelation.CancelarUno(xml);
                if (response.status == "success" && response.data != null)
                {
                    //Acuse de cancelación
                    Console.WriteLine(response.data.acuse);
                    //Estatus por UUID
                    foreach (var folio in response.data.uuid)
                    {
                        Console.WriteLine("UUID: {0} Estatus: {1}", folio.Key, folio.Value);
                    }
                }
                else
                {
                    //Obtenemos el detalle del Error
                    Console.WriteLine("Error al Cancelar\n\n");
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

**Ejemplo de consumo de la librería para cancelar con XML mediante token** [¿Como obtener token?](http://developers.sw.com.mx/knowledge-base/generar-un-token-infinito/)
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.CancelationRetention;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Obtenemos el XML de cancelacion
                byte[] xml = File.ReadAllBytes("cancelacion.xml");
                //Creamos una instancia de tipo CancelationRetention 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a Cancelar el XML o CFDI
                CancelationRetention cancelationRetention = new CancelationRetention("http://services.test.sw.com.mx", "TQL2....");
                CancelationResponse response = cancelation.CancelarUno(xml);
                if (response.status == "success" && response.data != null)
                {
                    //Acuse de cancelación
                    Console.WriteLine(response.data.acuse);
                    //Estatus por UUID
                    foreach (var folio in response.data.uuid)
                    {
                        Console.WriteLine("UUID: {0} Estatus: {1}", folio.Key, folio.Value);
                    }
                }
                else
                {
                    //Obtenemos el detalle del Error
                    Console.WriteLine("Error al Cancelar\n\n");
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
<summary>
<b>Cancelación de retención por CSD
</summary>

<br>Servicio para cancelar enviando un único UUID utilizando el certificado y llave privada del emisor.

Este método recibe los siguientes parámetros:
* Usuario y contraseña
* Url Servicios SW
* Certificado (.cer) en **Base64**
* Key (.key) en **Base64**
* RFC emisor
* Password del archivo key
* UUID
* Motivo
* Folio Sustitución (Si el motivo es 01: "Comprobante emitido con errores con relación")

[¿Como obtener token?](http://developers.sw.com.mx/knowledge-base/generar-un-token-infinito/)

**Ejemplo de consumo de la librería para cancelar retención con CSD con motivo de cancelación 02 "Comprobante emitido con errores sin relación", mediante token**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.CancelationRetention;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo CancelationRetention 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a Cancelar el XML o CFDI
                CancelationRetention cancelation = new CancelationRetention("http://services.test.sw.com.mx", "TQL2....");
                //Datos de Cancelación
                string password = "12345678a";
                string rfc = "EKU9003173C9";
                string uuid = "478569b5-c323-4dc4-91cf-b6e9f6979527";
                //Obtenemos Certificado y lo convertimos a Base 64
                string csdBase64 = Convert.ToBase64String(File.ReadAllBytes("EKU9003173C9.cer"));
                //Obtenemos LLave y lo convertimos a Base 64
                string keyBase64 = Convert.ToBase64String(File.ReadAllBytes("EKU9003173C9.key"));

                //Realizamos la petición de cancelación al servicio.
                CancelationResponse response = cancelation.CancelarUnoCSD(csdBase64,keyBase64,rfc,password,uuid,"02");
                if (response.status == "success" && response.Data != null)

                {
                    //Acuse de cancelación
                    Console.WriteLine(response.data.acuse);
                    //Estatus por UUID
                    foreach (var folio in response.data.uuid)
                    {
                        Console.WriteLine("UUID: {0} Estatus: {1}", folio.Key, folio.Value);
                    }
                }
                else
                {
                    //Obtenemos el detalle del Error
                    Console.WriteLine("Error al Cancelar\n\n");
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
**Ejemplo de consumo de la librería para cancelar retención con CSD con motivo de cancelación 01 "Comprobante emitido con errores con relación", mediante token**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.CancelationRetention;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo CancelationRetention 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a Cancelar el XML o CFDI
                CancelationRetention cancelation = new CancelationRetention("http://services.test.sw.com.mx", "TQL2....");
                //Datos de Cancelación
                string password = "12345678a";
                string rfc = "EKU9003173C9";
                string uuid = "478569b5-c323-4dc4-91cf-b6e9f6979527";
                string folioSustitucion = "01724196-ac5a-4735-b621-e3b42bcbb459";
                //Obtenemos Certificado y lo convertimos a Base 64
                string csdBase64 = Convert.ToBase64String(File.ReadAllBytes("EKU9003173C9.cer"));
                //Obtenemos LLave y lo convertimos a Base 64
                string keyBase64 = Convert.ToBase64String(File.ReadAllBytes("EKU9003173C9.key"));

                //Realizamos la petición de cancelación al servicio.
                CancelationResponse response = cancelation.CancelarUnoCSD(csdBase64,keyBase64,rfc,password,uuid,"01", folioSustitucion);
                if (response.status == "success" && response.Data != null)

                {
                    //Acuse de cancelación
                    Console.WriteLine(response.data.acuse);
                    //Estatus por UUID
                    foreach (var folio in response.data.uuid)
                    {
                        Console.WriteLine("UUID: {0} Estatus: {1}", folio.Key, folio.Value);
                    }
                }
                else
                {
                    //Obtenemos el detalle del Error
                    Console.WriteLine("Error al Cancelar\n\n");
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

<details>
<summary>
<b>Cancelación de retención por PFX
</summary>

<br>Servicio para cancelar enviando un único UUID utilizando el archivo .PFX del emisor.

Este método recibe los siguientes parámetros:
* Usuario y contraseña
* Url Servicios SW
* Archivo PFX en **Base64**
* RFC emisor
* Password (PFX)
* UUID
* Motivo
* Folio Sustitución (Si el motivo es 01: "Comprobante emitido con errores con relación")

[¿Como obtener token?](http://developers.sw.com.mx/knowledge-base/generar-un-token-infinito/)

**Ejemplo de consumo de la librería para cancelar retención con PFX con motivo de cancelación 02 "Comprobante emitido con errores sin relación", mediante token**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.CancelationRetention;

namespace ExampleSDK
{
    class Program
    {
       static void Main(string[] args)
        {
            try
            {
                //Datos de Cancelación
                string uuid = "01724196-ac5a-4735-b621-e3b42bcbb459";
                string rfc = "LAN8507268IA";
                string passwordKey = "12345678a";
                string motivo = "02";
                //Obtenemos el XML de cancelacion
                byte[] pfx = File.ReadAllBytes(Path.Combine(@"Resources\CertificadosDePrueba", "CSD_Prueba_CFDI_LAN8507268IA.pfx"));
                //Convertimos el PFX a base 64
                string pfxB64 = Convert.ToBase64String(pfx);
                //Creamos una instancia de tipo CancelationRetention 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a Cancelar el XML o CFDI
                CancelationRetention cancelationRetention = new CancelationRetention("http://services.test.sw.com.mx", "TQL2....");
                //Realizamos la petición de cancelación al servicio.
                CancelationResponse response = cancelationRetention.CancelarUnoPFX(pfxB64, rfc, passwordKey, uuid, motivo);
                if (response.status == "success" && response.Data != null)

                {
                    //Acuse de cancelación
                    Console.WriteLine(response.data.acuse);
                    //Estatus por UUID
                    foreach (var folio in response.data.uuid)
                    {
                        Console.WriteLine("UUID: {0} Estatus: {1}", folio.Key, folio.Value);
                    }
                }
                else
                {
                    //Obtenemos el detalle del Error
                    Console.WriteLine("Error al Cancelar\n\n");
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
**Ejemplo de consumo de la librería para cancelar retención con PFX con motivo de cancelación 01 "Comprobante emitido con errores con relación", mediante token**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.CancelationRetention;

namespace ExampleSDK
{
    class Program
    {
       static void Main(string[] args)
        {
            try
            {
                //Datos de Cancelación
                string uuid = "01724196-ac5a-4735-b621-e3b42bcbb459";
                string rfc = "LAN8507268IA";
                string passwordKey = "12345678a";
                string motivo = "01";
                string folioSustitucion = "01724196-ac5a-4735-b621-e3b42bcbb459";
                //Obtenemos el XML de cancelacion
                byte[] pfx = File.ReadAllBytes(Path.Combine(@"Resources\CertificadosDePrueba", "CSD_Prueba_CFDI_LAN8507268IA.pfx"));
                //Convertimos el PFX a base 64
                string pfxB64 = Convert.ToBase64String(pfx);
                //Creamos una instancia de tipo CancelationRetention 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a Cancelar el XML o CFDI
                CancelationRetention cancelationRetention = new CancelationRetention("http://services.test.sw.com.mx", "TQL2....");
                //Realizamos la petición de cancelación al servicio.
                CancelationResponse response = cancelationRetention.CancelarUnoPFX(pfxB64, rfc, passwordKey, uuid, motivo, folioSustitucion);
                if (response.status == "success" && response.Data != null)

                {
                    //Acuse de cancelación
                    Console.WriteLine(response.data.acuse);
                    //Estatus por UUID
                    foreach (var folio in response.data.uuid)
                    {
                        Console.WriteLine("UUID: {0} Estatus: {1}", folio.Key, folio.Value);
                    }
                }
                else
                {
                    //Obtenemos el detalle del Error
                    Console.WriteLine("Error al Cancelar\n\n");
                    Console.WriteLine(response.message);
                    Console.WriteLine(response.messageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
</details>

Para mayor referencia de un listado completo de los servicios favor de visitar el siguiente [link](http://developers.sw.com.mx/).

Si deseas contribuir a la libreria o tienes dudas envianos un correo a **soporte@sw.com.mx**.
