using RestSharp;
using System;
using System.Text;
using SW.Helpers;

namespace SW.Services.Stamp
{
    public class Stamp : StampService
    {
        private StampResponse stampResponse = new StampResponse();
        private Services service;
        private RestClient client;

        public Stamp(string url, string user, string password) : base(url, user, password)
        {
        }
        public Stamp(string url, string token) : base(url, token)
        {
        }
        private void BuildSettings()
        {
            client = new RestClient(Url);
        }
        private RestRequest RequestStamping(byte[] xml, string version)
        {
            service = SetupRequest();
            RestRequest request = new RestRequest("cfdi33/stamp/{version}", Method.POST);
            request.AddHeader("Authorization", Token);
            request.AddUrlSegment("version", version);
            request.AddFileBytes("xml", xml,"xml");
            return request;
        }
        public override StampResponse Timbrar(string xmlString, StampTypes stampTypes)
        {
            try
            {
                new StampValidation(Url, User, Password, Token).ValidaXML(xmlString);
                byte[] xml = Encoding.UTF8.GetBytes(xmlString);
                stampResponse = Stamping(xml, stampTypes);
            }
            catch (ServicesException e)
            {
                stampResponse.Status = ResponseType.Fail;
                stampResponse.Message = e.Message;
            }
            catch (TestEnviromentException)
            {
                stampResponse.Status = ResponseType.Success;
                stampResponse.Data = new DataDemoResponse().Stamp();
            }
            catch (Exception e)
            {
                stampResponse.Status = ResponseType.Error;
                stampResponse.Message = e.Message;
            }
            return stampResponse;
        }
        public override StampResponse TimbrarBase64(string xmlString, StampTypes stampTypes)
        {
            try
            {
                byte[] xml = Convert.FromBase64String(xmlString);
                new StampValidation(Url, User, Password, Token).ValidaXML(xml);
                stampResponse = Stamping(xml, stampTypes);
            }
            catch (ServicesException e)
            {
                stampResponse.Status = ResponseType.Fail;
                stampResponse.Message = e.Message;
            }
            catch (TestEnviromentException)
            {
                stampResponse.Status = ResponseType.Success;
                stampResponse.Data = new DataDemoResponse().Stamp();
            }
            catch (Exception e)
            {
                stampResponse.Status = ResponseType.Error;
                stampResponse.Message = e.Message;
            }
            return stampResponse;
        }
        private StampResponse Stamping(byte[] xml, StampTypes stampTypes)
        {
            BuildSettings();
            RestRequest request = RequestStamping(xml, stampTypes.ToString());
            IRestResponse<StampResponse> response = client.Execute<StampResponse>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                new Validation().ValidateResponseStatus(response.StatusCode);
            }
            stampResponse = response.Data;
               
            return stampResponse;
        }
    }
}
