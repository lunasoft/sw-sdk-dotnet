using System.Collections.Generic;
using SW.Helpers;

namespace SW.Services.Cancelation
{
    public class CancelationResponse : Response
    {
        public Data Data { get; set; }
    }
    public partial class Data
    {
        public string Acuse { get; set; }
        public Dictionary<string, string> uuid { get; set; }
    }
}