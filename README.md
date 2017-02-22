![NET](http://resources.workable.com/wp-content/uploads/2015/08/Microsoft-dotNET-logo.jpg)

A continuación encontrara la documentación necesaria para consumir nuestro SDK de servicios proveido por **SmarterWeb** para Timbrado de **CFDI 3.3**

Compatibilidad
-------------
* CFDI 3.3
* .Net Framework 3.5 or later 

Dependencias
------------
* [RestSharp](http://restsharp.org/)

----------------
Instalaci&oacute;n
---------
Para poder hacer uso de nuestro SDK de servicios primero tenemos que añadir la referencia de los respositorios de nuget proveidos por **SmarterWeb**.

#### Instalar la libreria (SDK) #####

* Paso 1:
Abrir la consola de **Pacakge Manager Console** dentro de visual studio
* Paso 2:
Escribir en la consola **Install-Package SW-sdk**
* Paso 4:
Usted debera ver un mensaje de confirmacion diciendo **Successfully installed 'SW-sdk 0.0.0.1'**


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
                StampResponse response = stamp.Timbrar(xml, StampTypes.v1);
            }
            catch (Exception e)
            {
            }
        }
    }
}
```

**Timbrar XML en formato string utilizando token**
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
                Stamp stamp = new Stamp("http://services.test.sw.com.mx", "T2lYQ0t4L0R....");
                string xml = Encoding.UTF8.GetString(File.ReadAllBytes("file.xml"));
                StampResponse response = stamp.Timbrar(xml, StampTypes.v1);
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
                Stamp stamp = new Stamp("http://services.test.sw.com.mx", "T2lYQ0t4L0R....");
                string xmlBase64 = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz4....";
                StampResponse response = stamp.TimbrarBase64(xmlBase64, StampTypes.v1);
            }
            catch (Exception e)
            {
            }
        }
    }
}
```