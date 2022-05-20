using SW.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Retention
{
    public class StampRetentionResponse : Response
    {
        public Data data { get; set; }
    }
    public partial class Data
    {
        public string retencion { get; set; }
    }
}
