using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SW.Helpers;
using Test_SW.Helpers;
using SW.Services.AcceptReject;
using System.Text;
using SW.Services.Status;
using System.Net;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using SAT.Services;
using SAT.Services.ConsultaCFDIService;

namespace Test_SW.Services.Status_Test
{
    [TestClass]
    public class Status_Test_45
    {
        //TEST
        private string urlSAT_test = "https://api.test.sw.com.mx/ConsultaCFDIService.svc";
        //Prod
        private string urlSAT = "https://consultaqr.facturaelectronica.sat.gob.mx/ConsultaCFDIService.svc";
        public Status_Test_45()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
        
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

        //TEST
        [TestMethod]
        public void StatusCFDI_Vigente_Test()
        {
            var build = new BuildSettings();
            Status status = new Status(urlSAT_test);
            var response = status.GetStatusCFDI("EKU9003173C9", "URE180429TM6", "10000.00", "cf6f2e04-85dd-49d7-87d0-b962b37e6004", "oxOSjA==");
            Assert.IsTrue(response.Estado == "Vigente");
        }
        [TestMethod]
        public void StatusCFDI_ExpresionNoValida_Text()
        {
            var build = new BuildSettings();
            Status status = new Status(urlSAT_test);
            var response = status.GetStatusCFDI("EKU9003173C9", "URE180429TM6", "199.16", "cf6f2e04-85dd-49d7-87d0-b962b37e6004", "oxOSjA==");
            Assert.IsTrue(response.CodigoEstatus.Contains("601"));
        }
        [TestMethod]
        public void StatusCFDI_Cancelado_Text()
        {
            var build = new BuildSettings();
            Status status = new Status(urlSAT_test);
            var response = status.GetStatusCFDI("EKU9003173C9", "URE180429TM6", "199.16", "c95e482b-5599-4de7-aaad-a0b1b3617cbc", "oxOSjA==");
            Assert.IsTrue(response.Estado == "Cancelado");
        }

        // Pruebas locales (sin red): transporte del servicio de estatus.
        [TestMethod]
        [TestCategory("WP3Unit")]
        public void Constructor_NoModificaValidacionTlsGlobal()
        {
            var original = ServicePointManager.ServerCertificateValidationCallback;
            new Status("http://localhost/consulta");
            new Status("https://localhost/consulta");
            Assert.AreSame(original, ServicePointManager.ServerCertificateValidationCallback);
        }

        [TestMethod]
        [TestCategory("WP3Unit")]
        public void Consulta_FalloDeTransporte_ConservaExcepcionOriginalYPila()
        {
            foreach (Exception expected in new[]
            {
                WcfException("CommunicationException", "Comunicacion de prueba"),
                new TimeoutException("Timeout de prueba"),
                WcfException("FaultException", "Fault de prueba")
            })
            {
                int queries = 0;
                int closes = 0;
                int aborts = 0;
                string trace = CaptureTrace(() =>
                {
                    Exception wrapper = CaptureFailure(() => InvokeConsulta(
                        () => { queries++; return ThrowFromConsulta(expected); },
                        () => closes++,
                        () => { aborts++; throw new InvalidOperationException("Abort de prueba"); }));
                    // Se relanza la instancia original con su pila, sin envolverla ni convertirla en null.
                    Assert.IsInstanceOfType(wrapper, typeof(TargetInvocationException));
                    Assert.AreSame(expected, wrapper.InnerException);
                    StringAssert.Contains(wrapper.InnerException.StackTrace, "ThrowFromConsulta");
                });
                Assert.AreEqual(1, queries);
                Assert.AreEqual(0, closes);
                Assert.AreEqual(1, aborts);
                StringAssert.Contains(trace, "Abort de prueba");
            }
        }

        private static Exception WcfException(string name, string message)
        {
            // Reflexion para no agregar referencias al csproj fuera de la linea Compile propia.
            Assembly assembly = typeof(SATServices).GetMethod("GetBinding").ReturnType.Assembly;
            return (Exception)Activator.CreateInstance(assembly.GetType("System.ServiceModel." + name, true), new object[] { message });
        }

        private static Acuse InvokeConsulta(Func<Acuse> query, Action close, Action abort)
        {
            MethodInfo method = typeof(StatusService).GetMethod("InvokeConsulta", BindingFlags.Static | BindingFlags.NonPublic);
            Assert.IsNotNull(method);
            return (Acuse)method.Invoke(null, new object[] { query, close, abort });
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static Acuse ThrowFromConsulta(Exception failure)
        {
            throw failure;
        }

        private static string CaptureTrace(Action action)
        {
            using (StringWriter writer = new StringWriter())
            using (TextWriterTraceListener listener = new TextWriterTraceListener(writer))
            {
                Trace.Listeners.Add(listener);
                try
                {
                    action();
                    listener.Flush();
                    return writer.ToString();
                }
                finally
                {
                    Trace.Listeners.Remove(listener);
                }
            }
        }

        private static Exception CaptureFailure(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                return ex;
            }
            Assert.Fail("La consulta debio propagar una excepcion.");
            return null;
        }
    }
}
