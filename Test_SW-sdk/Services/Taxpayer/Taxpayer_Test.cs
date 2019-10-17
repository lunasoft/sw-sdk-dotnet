using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Services.Taxpayer;
using Test_SW.Helpers;
using System.Diagnostics;

namespace Test_SW.Services.Taxpayer_Test
{
    [TestClass]
   public class Taxpayer_Test
    {
        [TestMethod]
        public void TaxpayerByUser()
        {
            var build = new BuildSettings();
            Taxpayer Taxpayer = new Taxpayer(build.Url,build.User,build.Password);
            var response = Taxpayer.GetTaxpayer("ZNS1101105T3");

            Debug.WriteLine(response.data.id);
            Debug.WriteLine(response.data.nombre_Contribuyente);
            Debug.WriteLine(response.data.numero_fecha_oficio_global_contribuyentes_que_desvirtuaron);
            Debug.WriteLine(response.data.numero_fecha_oficio_global_definitivos);
            Debug.WriteLine(response.data.numero_fecha_oficio_global_sentencia_favorable);
            Debug.WriteLine(response.data.numero_y_fecha_oficio_global_presuncion);
            Debug.WriteLine(response.data.publicacion_DOF_definitivos);
            Debug.WriteLine(response.data.publicacion_DOF_desvirtuados);
            Debug.WriteLine(response.data.publicacion_DOF_presuntos);
            Debug.WriteLine(response.data.publicacion_DOF_sentencia_favorable);
            Debug.WriteLine(response.data.publicacion_pagina_SAT_definitivos);
            Debug.WriteLine(response.data.publicacion_pagina_SAT_desvirtuados);
            Debug.WriteLine(response.data.publicacion_pagina_SAT_presuntos);
            Debug.WriteLine(response.data.publicacion_pagina_SAT_sentencia_favorable);
            Debug.WriteLine(response.data.rfc);
            Debug.WriteLine(response.data.situacion_del_contribuyente);

            Assert.IsTrue(response.data != null && response.status == "success");
        }
        [TestMethod]
        public void TaxpayerByToken()
        {
            var build = new BuildSettings();
            Taxpayer Taxpayer = new Taxpayer(build.Url, build.Token);
            var response = Taxpayer.GetTaxpayer("ZNS1101105T3");
            Assert.IsTrue(response.data != null && response.status == "success");
        }
        [TestMethod]
        public void Taxpayer_Test_45_TaxpayerByUserError()
        {
            var build = new BuildSettings();
            Taxpayer Taxpayer = new Taxpayer(build.Url, build.User, build.Password);
            var response = Taxpayer.GetTaxpayer("ZNS1101105T4");
            Assert.IsTrue(response.status == "error", response.message);
        }
        [TestMethod]
        public void Taxpayer_Test_45_TaxpayerByTokenerror()
        {
            var build = new BuildSettings();
            Taxpayer Taxpayer = new Taxpayer(build.Url, build.Token);
            var response = Taxpayer.GetTaxpayer("ZNS1101105T4");
            Assert.IsTrue(response.status == "error", response.message);
        }
    }
}
