using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SW.Helpers;
using Test_SW.Helpers;
using SW.Services.AcceptReject;
using System.Text;
using SW.Services.Status;
using System.Net;
using System.Security.Authentication;

namespace Test_SW.Services.Status_Test
{
    [TestClass]
    public class Status_Test
    {
        public Status_Test()
        {
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)(SslProtocols)0x00000C00;
        }
        //TEST
        //private string urlSAT = "https://srvconsultacfdiuat.cloudapp.net/ConsultaCFDIService.svc";
        //Prod
        private string urlSAT = "https://consultaqr.facturaelectronica.sat.gob.mx/ConsultaCFDIService.svc";

        [TestMethod]
        public void StatusCFDI_Vigente()
        {
            var build = new BuildSettings();
            Status status = new Status(urlSAT);
            var response = status.GetStatusCFDI("GOM0809114P5", "LSO1306189R5", "206.85", "021ea2fb-2254-4232-983b-9808c2ed831b", "oxOSjA==");
            Assert.IsTrue(response.Estado == "Vigente");
        }
        [TestMethod]
        public void StatusCFDI_NoEncontrado()
        {
            var build = new BuildSettings();
            Status status = new Status(urlSAT);
            var response = status.GetStatusCFDI("GOM0809114P5", "LSO1306189R5", "206.85", "021ea2fb-2254-4232-983b-9808c2ed831", "oxOSjA==");
            Assert.IsTrue(response.CodigoEstatus.Contains("602"));
        }
        [TestMethod]
        public void StatusCFDI_ExpresionNoValida()
        {
            var build = new BuildSettings();
            Status status = new Status(urlSAT);
            var response = status.GetStatusCFDI("GOM0809114P5", "LSO1306189R5", "?", "021ea2fb-2254-4232-983b-9808c2ed831", "oxOSjA==");
            Assert.IsTrue(response.CodigoEstatus.Contains("601"));
        }
        [TestMethod]
        public void StatusCFDI_Cancelado()
        {
            var build = new BuildSettings();
            Status status = new Status(urlSAT);
            var response = status.GetStatusCFDI("LSO1306189R5", "LSO1306189R5", "1.16", "e0aae6b3-43cc-4b9c-b229-7e221000e2bb", "oxOSjA==");
            Assert.IsTrue(response.Estado == "Cancelado");
        }
    }
}
