using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using SW.Entities;
using SW.Helpers;

namespace SW.Services.Pdf
{
    public class PdfResponse : Response
    {
        public Data data { get; set; }
        public int responseCode { get; set; }
    }

    public class Data
    {
        public string contentB64 { get; set; }
        public int contentSizeBytes { get; set; }
        public string uuid { get; set; }
        public string serie { get; set; }
        public string folio { get; set; }
        public DateTime stampDate { get; set; }
        public DateTime issuedDate { get; set; }
        public string rfcIssuer { get; set; }
        public string rfcReceptor { get; set; }
        public string total { get; set; }
    }

}
