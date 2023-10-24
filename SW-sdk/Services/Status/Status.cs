using SAT.Services.ConsultaCFDIService;

namespace SW.Services.Status
{
    public class Status : StatusService
    {
        public Status(string url) : base(url)
        {
        }

        internal override Acuse StatusRequest(string rfcEmisor, string rfcReceptor, string total, string uuid, string sello)
        {
            return this.RequestStatus(rfcEmisor, rfcReceptor, total, uuid, sello);
        }
       
        public Acuse GetStatusCFDI(string rfcEmisor, string rfcReceptor, string Total, string uuid, string sello)
        {
            return StatusRequest(rfcEmisor, rfcReceptor, Total, uuid, sello);
        }
    }
}
