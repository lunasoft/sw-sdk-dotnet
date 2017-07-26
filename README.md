![NET](http://resources.workable.com/wp-content/uploads/2015/08/Microsoft-dotNET-logo.jpg)
![NET](https://smarterwebci.visualstudio.com/_apis/public/build/definitions/402b9165-314f-4f5f-8073-9ae3a2e962ef/23/badge)
##### Servicios de Timbrado para documentos CFDI a traves del Proveedor de Certificación de CFDI  **SW SmarterWeb**

Compatibilidad
-------------
* CFDI 3.3
* .Net Framework 3.5 or later 

Dependencias
------------
* [RestSharp](http://restsharp.org/)

Documentación
------------
* [Inicio Rápido](http://developers.sw.com.mx/knowledge-base/cfdi-33/)
* [Libreria dot-net](http://developers.sw.com.mx/article-categories/csharp/)
* [Documentacion Oficial Servicios](http://developers.sw.com.mx)
 
----------------
Instalaci&oacute;n
---------
Instalar la libreria a traves Package Manager Console [nuget.org](https://www.nuget.org/packages/SW-sdk)
```cs
Install-Package SW-sdk
```
En caso de no utilizar Package Manager Console puedes descargar la libreria directamente a traves del siguiente [link](https://github.com/lunasoft/sw-sdk-dotnet/releases) y agregarla como Referencia local a tu proyecto. Asegurate de utilizar la ultima version publicada.

Implementaci&oacute;n
---------
La librería contara con dos servicios principales los que son la Autenticacion y el Timbrado de CFDI.

#### Aunteticaci&oacute;n #####
El servicio de Autenticación es utilizado principalmente para obtener el **token** el cual sera utilizado para poder timbrar nuestro CFDI (xml) ya emitido (sellado), para poder utilizar este servicio es necesario que cuente con un **usuario** y **contraseña** para posteriormente obtenga el token, usted puede utilizar los que estan en este ejemplo para el ambiente de **Pruebas**.

**Obtener Token**
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
                Authentication auth = new Authentication("http://services.test.sw.com.mx", "demo", "123456789");
                AuthResponse response = auth.GetToken();
            }
            catch (Exception e)
            {

            }

        }
    }
}

```
#### Timbrar CFDI V1 #####
**TimbrarV1** Recibe el contenido de un **XML** ya emitido (sellado) en formato **String**  ó tambien puede ser en **Base64**, posteriormente si la factura y el token son correctos devuelve el complemento timbre en un string (**TFD**), en caso contrario lanza una excepción.

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
                Stamp stamp = new Stamp("http://services.test.sw.com.mx", "demo", "123456789");
                string xml = Encoding.UTF8.GetString(File.ReadAllBytes("file.xml"));
                StampResponseV1 response = stamp.TimbrarV1(xml);
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
            }
            catch (Exception e)
            {
            }
        }
    }
}
```

**Timbrar XML en Base64 utilizando token**
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
                xml = Convert.ToBase64String(xmlBase);
                StampResponseV1 response = stamp.TimbrarV1(xml, true);
            }
            catch (Exception e)
            {
            }
        }
    }
}
```

Cancelación
-------------
**Cancelacion** Se utiliza para cancelar documentos xml y se puede hacer mediante dos metodos **Cancelación CSD** y **Cancelacion por XML**.

 ##### Cancelacion por CSD #####
Como su nombre lo indica, este metodo recibe todos los elementos que componen el CSD los cuales son los siguientes:

* Certificado (.cer) en **Base64**
* Key (.key) en **Base64**
* Password del archivo key
* RFC emisor

**Ejemplo de consumo de la libreria para cancelar con CSD**
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
                Cancelation cancelation = new Cancelation("http://services.test.sw.com.mx", "demo", "123456789");
               
                //Obtenemos Certificado y lo convertimos a Base 64
                string CerB64 = Convert.ToBase64String(File.ReadAllBytes("CSD_Prueba_CFDI_LAN8507268IA.cer"));
                //Obtenemos LLave y lo convertimos a Base 64
                string KeyB64 = Convert.ToBase64String(File.ReadAllBytes("CSD_Prueba_CFDI_LAN8507268IA.key"));
               
                CancelationResponse response = cancelation.CancelarByCSD(CerB64, KeyB64, "LAN8507268IA", "12345678a", "01724196-ac5a-4735-b621-e3b42bcbb459");
              
                //Para Obtener el Acuse de Cancelación
                response.Data.Acuse
                
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

#### Cancelacion por XML ####
Este metodo recibe únicamente el XML sellado con los UUID a cancelar.

**Ejemplo de XML para Cancelar**
```xml
<?xml version="1.0" encoding="utf-8"?>
<Cancelacion xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" RfcEmisor="LAN7008173R5" Fecha="2017-07-06T17:00:31" xmlns="http://cancelacfd.sat.gob.mx">
  <Folios>
    <UUID>3eaeabc9-ea41-4627-9609-c6856b78e2b1</UUID>
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
        <DigestValue>rs2ZcFnS9hbfmyJLmR3Mtnklt7g=</DigestValue>
      </Reference>
    </SignedInfo>
    <SignatureValue>O/I7ILsU2y1fqeb2NBZSQKlQC3DpN/bgcDB5LWCMIYp4mFCLmLxEq/6ADz0xVQWUw49BqWDZ1GAI4ODIZLDQtafHSIE7BXKy8huvKD1dtpRLQ/39IfpxXsz1g6Q14mH3LxDOQugk/GhKMWILXZnIipyQosv3IbgLMZ/V/4btK7xrFX/KiOt0PcefChyaerj9A815dA3J4JgpBUNzbOz9VlhvdZMJskrHxzZ5riU1TAuSw/oi68dJfA7S+6XrTmeFDQzYxACHyOzj24RjLi/31+Fc/wiqQXNu9O6oWl8p5+GVoz2xtU4aRqLxVh73L6WAAef/WDeKDMfIge1BtMrxYw==</SignatureValue>
    <KeyInfo>
      <X509Data>
        <X509IssuerSerial>
          <X509IssuerName>OID.1.2.840.113549.1.9.2=Responsable: ACDMA, OID.2.5.4.45=SAT970701NN3, L=Coyoac?n, S=Distrito Federal, C=MX, PostalCode=06300, STREET="Av. Hidalgo 77, Col. Guerrero", E=asisnet@pruebas.sat.gob.mx, OU=Administraci?n de Seguridad de la Informaci?n, O=Servicio de Administraci?n Tributaria, CN=A.C. 2 de pruebas(4096)</X509IssuerName>
          <X509SerialNumber>3230303031303030303030333030303232383135</X509SerialNumber>
        </X509IssuerSerial>
        <X509Certificate>MIIFxTCCA62gAwIBAgIUMjAwMDEwMDAwMDAzMDAwMjI4MTUwDQYJKoZIhvcNAQELBQAwggFmMSAwHgYDVQQDDBdBLkMuIDIgZGUgcHJ1ZWJhcyg0MDk2KTEvMC0GA1UECgwmU2VydmljaW8gZGUgQWRtaW5pc3RyYWNpw7NuIFRyaWJ1dGFyaWExODA2BgNVBAsML0FkbWluaXN0cmFjacOzbiBkZSBTZWd1cmlkYWQgZGUgbGEgSW5mb3JtYWNpw7NuMSkwJwYJKoZIhvcNAQkBFhphc2lzbmV0QHBydWViYXMuc2F0LmdvYi5teDEmMCQGA1UECQwdQXYuIEhpZGFsZ28gNzcsIENvbC4gR3VlcnJlcm8xDjAMBgNVBBEMBTA2MzAwMQswCQYDVQQGEwJNWDEZMBcGA1UECAwQRGlzdHJpdG8gRmVkZXJhbDESMBAGA1UEBwwJQ295b2Fjw6FuMRUwEwYDVQQtEwxTQVQ5NzA3MDFOTjMxITAfBgkqhkiG9w0BCQIMElJlc3BvbnNhYmxlOiBBQ0RNQTAeFw0xNjEwMjUyMTUyMTFaFw0yMDEwMjUyMTUyMTFaMIGxMRowGAYDVQQDExFDSU5ERU1FWCBTQSBERSBDVjEaMBgGA1UEKRMRQ0lOREVNRVggU0EgREUgQ1YxGjAYBgNVBAoTEUNJTkRFTUVYIFNBIERFIENWMSUwIwYDVQQtExxMQU43MDA4MTczUjUgLyBGVUFCNzcwMTE3QlhBMR4wHAYDVQQFExUgLyBGVUFCNzcwMTE3TURGUk5OMDkxFDASBgNVBAsUC1BydWViYV9DRkRJMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgvvCiCFDFVaYX7xdVRhp/38ULWto/LKDSZy1yrXKpaqFXqERJWF78YHKf3N5GBoXgzwFPuDX+5kvY5wtYNxx/Owu2shNZqFFh6EKsysQMeP5rz6kE1gFYenaPEUP9zj+h0bL3xR5aqoTsqGF24mKBLoiaK44pXBzGzgsxZishVJVM6XbzNJVonEUNbI25DhgWAd86f2aU3BmOH2K1RZx41dtTT56UsszJls4tPFODr/caWuZEuUvLp1M3nj7Dyu88mhD2f+1fA/g7kzcU/1tcpFXF/rIy93APvkU72jwvkrnprzs+SnG81+/F16ahuGsb2EZ88dKHwqxEkwzhMyTbQIDAQABox0wGzAMBgNVHRMBAf8EAjAAMAsGA1UdDwQEAwIGwDANBgkqhkiG9w0BAQsFAAOCAgEAJ/xkL8I+fpilZP+9aO8n93+20XxVomLJjeSL+Ng2ErL2GgatpLuN5JknFBkZAhxVIgMaTS23zzk1RLtRaYvH83lBH5E+M+kEjFGp14Fne1iV2Pm3vL4jeLmzHgY1Kf5HmeVrrp4PU7WQg16VpyHaJ/eonPNiEBUjcyQ1iFfkzJmnSJvDGtfQK2TiEolDJApYv0OWdm4is9Bsfi9j6lI9/T6MNZ+/LM2L/t72Vau4r7m94JDEzaO3A0wHAtQ97fjBfBiO5M8AEISAV7eZidIl3iaJJHkQbBYiiW2gikreUZKPUX0HmlnIqqQcBJhWKRu6Nqk6aZBTETLLpGrvF9OArV1JSsbdw/ZH+P88RAt5em5/gjwwtFlNHyiKG5w+UFpaZOK3gZP0su0sa6dlPeQ9EL4JlFkGqQCgSQ+NOsXqaOavgoP5VLykLwuGnwIUnuhBTVeDbzpgrg9LuF5dYp/zs+Y9ScJqe5VMAagLSYTShNtN8luV7LvxF9pgWwZdcM7lUwqJmUddCiZqdngg3vzTactMToG16gZA4CWnMgbU4E+r541+FNMpgAZNvs2CiW/eApfaaQojsZEAHDsDv4L5n3M1CC7fYjE/d61aSng1LaO6T1mh+dEfPvLzp7zyzz+UgWMhi5Cs4pcXx1eic5r7uxPoBwcCTt3YI1jKVVnV7/w=</X509Certificate>
      </X509Data>
    </KeyInfo>
  </Signature>
</Cancelacion>
```

**Ejemplo de consumo de la libreria para cancelar con XML**
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
                Cancelation cancelation = new Cancelation("http://services.test.sw.com.mx", "demo", "123456789");
               
                //Obtenemos el XML de cancelacion
                string xml = File.ReadAllBytes("Resources/xml.xml");
                
                CancelationResponse response = cancelation.CancelarByXML(xml);
              
                //Para Obtener el Acuse de Cancelación
                response.Data.Acuse
                
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

Consulta de Saldos
-------------
Este servicio recibe el token y genera los elementos que componen la consulta de saldos:

Se deberá autenticar en nuestros servicios en orden de obtener token de acceso, o si se desea,  se puede usar el token infinito.

**Ejemplo de consumo de la libreria para consultar el saldo**
```cs
using System;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.BalanceAccount;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Creamos una instancia de tipo BalanceAccount 
                //A esta le pasamos la Url, Usuario y Contraseña para obtener el token
                //Automaticamente despues de obtenerlo se procedera a Cancelar el xml o cfdi
                BalanceAccount account = new BalanceAccount("http://services.test.sw.com.mx", "demo", "123456789");
                AccountResponse response = account.ConsultarSaldo();
              
                //Para Obtener el idSaldoCliente
                response.Data.idSaldoCliente;
                
                //Para Obtener el idClienteUsuario
                response.Data.idClienteUsuario;
                
                //Para Obtener el saldo Timbres
                response.Data.saldoTimbres;
                
                //Para Obtenerlos timbres Utilizados
                response.Data.timbresUtilizados;
                
                //Para Obtener la fechaExpiracion
                response.Data.fechaExpiracion;
                
                //Para Obtener si es Ilimitado
                response.Data.unlimited;
                
                //Para Obtener los timbres Asignados
                response.Data.timbresAsignados;
                
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

Para mayor referencia de un listado completo de los servicios favor de visitar el siguiente [link](http://developers.sw.com.mx/).

Si deseas contribuir a la libreria o tienes dudas envianos un correo a **soporte@sw.com.mx**.
