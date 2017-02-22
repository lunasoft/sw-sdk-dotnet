using SW.Helpers;

namespace SW.Services.Stamp
{
    public class StampResponse : Response
    {
        public Data Data { get; set; }
    }
    public partial class Data
    {
        public string tfd { get; set; }
        public string cfdi { get; set; }
    }
}