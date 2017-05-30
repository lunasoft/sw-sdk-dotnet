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

Para mayor referencia de un listado completo de los servicios favor de visitar el siguiente [link](http://developers.sw.com.mx/).

Si deseas contribuir a la libreria o tienes dudas envianos un correo a **soporte@sw.com.mx**.
