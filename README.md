![NET](http://resources.workable.com/wp-content/uploads/2015/08/Microsoft-dotNET-logo.jpg)
![NET](https://smarterwebci.visualstudio.com/_apis/public/build/definitions/402b9165-314f-4f5f-8073-9ae3a2e962ef/23/badge)
##### Servicios de Timbrado para documentos CFDI a traves del Proveedor de Certificación de CFDI  **SW SmarterWeb**

# Compatibilidad #
* CFDI 3.3
* .Net Framework 3.5 or later 

# Dependencias #
* [RestSharp](http://restsharp.org/)

# Documentación #
* [Inicio Rápido](http://developers.sw.com.mx/knowledge-base/cfdi-33/)
* [Libreria dot-net](http://developers.sw.com.mx/article-categories/csharp/)
* [Documentacion Oficial Servicios](http://developers.sw.com.mx)
 
----------------
# Instalaci&oacute;n #
Instalar la libreria a traves Package Manager Console [nuget.org](https://www.nuget.org/packages/SW-sdk)
```cs
Install-Package SW-sdk
```
En caso de no utilizar Package Manager Console puedes descargar la libreria directamente a traves del siguiente [link](https://github.com/lunasoft/sw-sdk-dotnet/releases) y agregarla como Referencia local a tu proyecto. Asegurate de utilizar la ultima version publicada.

# Implementaci&oacute;n #
La librería contara con dos servicios principales los que son la Autenticacion y el Timbrado de CFDI.

## Aunteticaci&oacute;n ##
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
## Timbrar CFDI V1 ##
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

# Cancelación #
**Cancelacion** Se utiliza para cancelar documentos xml y se puede hacer mediante varios metodos **Cancelación CSD**, **Cancelación PFX**, **Cancelacion por XML** y **Cancelación UUID**.

 ## Cancelacion por CSD ##
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
              
                if (response.status == "success" && response.Data != null)
                {
                    //Acuse de cancelación
                    Console.WriteLine(response.Data.Acuse);
                    //Estatus por UUID
                    foreach (var folio in response.Data.uuid)
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

## Cancelacion por XML ##
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
                byte[] xml = File.ReadAllBytes("Resources/xml.xml");
                
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
## Cancelacion por PFX ##
Este metodo recibe únicamente el PFX , password, rfc y uuid.

**Ejemplo de consumo de la libreria para cancelar con PFX**
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
                Cancelation cancelation = new Cancelation("http://services.test.sw.com.mx", "demo", "123456789");
               
                //Obtenemos el XML de cancelacion
                 byte[] pfx = File.ReadAllBytes(Path.Combine(@"Resources\CertificadosDePrueba", "CSD_Prueba_CFDI_LAN8507268IA.pfx"));
                 //Convertimos el PFX a base 64
                 string pfxB64 = Convert.ToBase64String(pfx);

                //Realizamos la petición de cancelación al servicio.
                CancelationResponse response = cancelation.CancelarByPFX(pfxB64, rfc, passwordKey, uuid);
                if (response.status == "success" && response.Data != null)
                {
                    //Acuse de cancelación
                    Console.WriteLine(response.Data.Acuse);
                    //Estatus por UUID
                    foreach (var folio in response.Data.uuid)
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

## Cancelacion por UUID ##
Este metodo recibe únicamente el rfc y uuid.

**Ejemplo de consumo de la libreria para cancelar con UUID**
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
                Cancelation cancelation = new Cancelation("http://services.test.sw.com.mx", "demo", "123456789");
                //Realizamos la petición de cancelación al servicio.
                CancelationResponse response = cancelation.CancelarByRfcUuid(rfc,  uuid);
                if (response.status == "success" && response.Data != null)
                {
                    //Acuse de cancelación
                    Console.WriteLine(response.Data.Acuse);
                    //Estatus por UUID
                    foreach (var folio in response.Data.uuid)
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
# Consulta de Saldos #
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
                //Automaticamente despues de obtenerlo se procedera a consultar el saldo
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

# Validaciones #
Este servicio recibe el token y verifica que los datos, según sea el método que se consuma.

Se deberá autenticar en nuestros servicios en orden de obtener token de acceso, o si se desea,  se puede usar el token infinito.

## Validacion XML ##
Este servicio verifica integridad, estatus en el SAT, estructura válida.

**Ejemplo de consumo de la librería para validar el XML**
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
                Validate validate = new Validate ("http://services.test.sw.com.mx", "demo", "123456789");
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

## Validacion LRFC ##
Este servicio verifica que el RFC proporcionado se encuentre en la lista LRFC que publica el SAT.

**Ejemplo de consumo de la librería para validar el LRFC**
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
                Validate validate = new Validate ("http://services.test.sw.com.mx", "demo", "123456789");
                ValidateLrfcResponse response = validate.ValidateLrfc("LAN7008173R5");
				//Para obtener el status
				Console.Write(response.status + "\n");
				//Para obtener el contribuyente
				Console.Write(response.data.contribuyenteRFC + "\n");
				//Para obtener el campo sncf
				Console.Write(response.data.sncf + "\n");
				//Para obtener si es subcontratado
				Console.Write(response.data.subcontratacion + "\n");
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

## Validacion LCO ##
Este servicio verifica que el número de certificado proporcionado se encuentre válido, así como la vigencia del mismo.

**Ejemplo de consumo de la librería para validar la LCO**
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
                Validate validate = new Validate ("http://services.test.sw.com.mx", "demo", "123456789");
                ValidateLcoResponse response = validate.ValidateLco("20001000000300022815");
				//Para obtener el status de la respuesta
				Console.Write(response.status);
				//Para obtener el estatus del certificado
                Console.Write(response.data.estatusCertificado);
                //Para obtener la fecha inicial del certificado
                Console.Write(response.data.fechaInicio.ToString());
                //Para obtener la fecha de vencimiento
                Console.Write(response.data.fechaFinal.ToString());
                //Para obtener el número de certificado
                Console.Write(response.data.noCertificado);
                //Para obtener el RFC asociado a ese certificado
                Console.Write(response.data.rfc);
                //Para obtener la validez de obligaciones
                Console.Write(response.data.validezObligaciones);
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

# Consulta Estatus SAT #
Este método recibe RFC emisor, RFC receptor, total y UUID de la factura a la cual consultaremos su Estatus en el SAT.
**Ejemplo de consumo de la librería para la consulta**
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
                Status status = new Status("https://consultaqr.facturaelectronica.sat.gob.mx/ConsultaCFDIService.svc");
                var response = status.GetStatusCFDI("GOM0809114P5", "LSO1306189R5", "206.85", "021ea2fb-2254-4232-983b-9808c2ed831b");
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

# CFDI Relacionados #
A través de estos siguientes métodos obtendremos un listado de los UUID que se encuentren relacionados a una factura.
## Relacionados por CSD ##
Este método recibe el **certificado** en base64, **llave** en base64, **RFC**, **password** del certificado, y el **UUID** de la factura.
**Ejemplo de consumo de la librería para la consulta**
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
                Relations relations = new Relations("http://services.test.sw.com.mx", "demo", "123456789");
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
## Relacionados por PFX ##
Este método recibe el **PFX** en base64, **password** del certificado, **RFC**, y el **UUID** de la factura.
**Ejemplo de consumo de la librería para la consulta**
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
                Relations relations = new Relations("http://services.test.sw.com.mx", "demo", "123456789");
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

## Relacionados por XML ##
Este método recibe el **XML** de relacionados.
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


**Ejemplo de consumo de la librería para la consulta**
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
                Relations relations = new Relations("http://services.test.sw.com.mx", "demo", "123456789");
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

## Relacionados por UUID ##
Este método recibe el **RFC** y el **UUID** de la factura.
***NOTA:*** El usuario deberá tener sus certificados en el administrador de timbres para la utilización de este método.
**Ejemplo de consumo de la librería para la consulta**
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
                Relations relations = new Relations("http://services.test.sw.com.mx", "demo", "123456789");
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

# Consulta solicitudes pendientes Aceptar / Rechazar #
A través de este método obtendremos una lista de los UUID que tenemos pendientes por aceptar o rechazar.
Este método recibe el **RFC** del cual obtendremos la lista.

**Ejemplo de consumo de la librería para la consulta**
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
                Pending pendientes = new Pending("http://services.test.sw.com.mx", "demo", "123456789");
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
# Aceptar / Rechazar #
A través de estos siguientes métodos aceptaremos o rechazaremos los UUID.

## Aceptar / Rechazar por CSD ##
Este método recibe el **certificado** en base64, **llave** en base64, **RFC**, **password** del certificado, y los **UUID** con su respectiva acción.
**Ejemplo de consumo de la librería para la utilización**
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
                AcceptReject acceptReject = new AcceptReject ("http://services.test.sw.com.mx", "demo", "123456789");
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
## Aceptar / Rechazar por PFX ##
Este método recibe el **PFX** en base64, **password** del certificado, **RFC**, y los **UUID** de las facturas con su respectiva acción a realizar.
**Ejemplo de consumo de la librería para la utilización**
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
                AcceptReject acceptReject = new AcceptReject ("http://services.test.sw.com.mx", "demo", "123456789");
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

## Aceptar / Rechazar por XML ##
Este método recibe el **XML** de aceptación / rechazo.
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


**Ejemplo de consumo de la librería para la utilización**
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
                AcceptReject acceptReject = new AcceptReject ("http://services.test.sw.com.mx", "demo", "123456789");
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

## Aceptar / Rechazar por UUID ##
Este método recibe el **RFC**,  **UUID** de la factura y **acción** a realizar.
***NOTA:*** El usuario deberá tener sus certificados en el administrador de timbres para la utilización de este método.
**Ejemplo de consumo de la librería para la utilización**
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
                AcceptReject acceptReject = new AcceptReject ("http://services.test.sw.com.mx", "demo", "123456789");
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
## Taxpayer / Consultar RFC en Lista 69-B por RFC ##
Este método recibe el **RFC** a consultar.
***NOTA:*** No siempre responde con todos los campos del ejemplo, en caso que no contenga ese dato el registro del RFC regresa una cadena vacía.
**Ejemplo de consumo de la librería para la utilización**
```cs
using SW.Services.Taxpayer;
using System;
using System.IO;
using System.Text;
using SW.Helpers;

namespace ExampleSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Taxpayer Taxpayer = new Taxpayer("http://services.test.sw.com.mx", "demo", "123456789");
                var response = Taxpayer.GetTaxpayer("ZNS1101105T3");
                Console.WriteLine(response.data.id);
                Console.WriteLine(response.data.rfc);
                Console.WriteLine(response.data.nombre_Contribuyente);
                Console.WriteLine(response.data.numero_fecha_oficio_global_contribuyentes_que_desvirtuaron);
                Console.WriteLine(response.data.numero_fecha_oficio_global_definitivos);
                Console.WriteLine(response.data.numero_fecha_oficio_global_sentencia_favorable);
                Console.WriteLine(response.data.numero_y_fecha_oficio_global_presuncion);
                Console.WriteLine(response.data.publicacion_DOF_definitivos);
                Console.WriteLine(response.data.publicacion_DOF_desvirtuados);
                Console.WriteLine(response.data.publicacion_DOF_presuntos);
                Console.WriteLine(response.data.publicacion_DOF_sentencia_favorable);
                Console.WriteLine(response.data.publicacion_pagina_SAT_definitivos);
                Console.WriteLine(response.data.publicacion_pagina_SAT_desvirtuados);
                Console.WriteLine(response.data.publicacion_pagina_SAT_presuntos);
                Console.WriteLine(response.data.publicacion_pagina_SAT_sentencia_favorable);
                Console.WriteLine(response.data.situacion_del_contribuyente);
				  }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

## StampCustomIdV4 / Método para evitar CFDI duplicados ##
Método de timbrado customIdV4XML
***NOTA:*** Este método recibe un customId, si anteriormente fue timbrada una factura con este customId te regresara el TFD y CFDI anteriormente timbrado.
**Ejemplo de consumo de la librería para la utilización**
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
                //url para pord http://services.sw.com.mx y http://api.sw.com.mx
                StampV4XML stamp = new StampV4XML("http://services.test.sw.com.mx", "http://api.test.sw.com.mx", "T2lYQ0t4L0RHVkR4dH........");
                
                string CustomId = "RamdoncustomId_Max100Char"
                response = (StampResponseV2) stamp.TimbrarV2(xml, null, CustomId);

                if(response.status == "success" && response.data != null)
                {
                    //Puedes obtener CFDI y TFD de manera normal.
                    Console.WriteLine(response.data.tfd);
                    Console.WriteLine(response.data.cfdi);
                }
                else if(response.status == "error" && response.message == "No es posible obtener el url para decargar el XML")
                {
                    //Aquí solo podrás obtener el TFD y deberá intentar más tarde, la condición es utilizar el mismo customID
                    Console.WriteLine(response.data.tfd);
                    Console.WriteLine(response.messageDetail);
                }
                else if(response.status == "error" && response.message == "CFDI3307 - Timbre duplicado. El customId proporcionado está duplicado.")
                {
                    //Puedes obtener CFDI y TFD de manera normal.
                    Console.WriteLine(response.data.tfd);
                    Console.WriteLine(response.data.cfdi);
                }
            }
            catch (Exception e)
            {
                //Una Exception relacionado a este metod puede ser "No es posible obtener el UUID"
                Console.WriteLine(e.Message);
            }
        }
    }
}
```
Para mayor referencia de un listado completo de los servicios favor de visitar el siguiente [link](http://developers.sw.com.mx/).

Si deseas contribuir a la libreria o tienes dudas envianos un correo a **soporte@sw.com.mx**.
