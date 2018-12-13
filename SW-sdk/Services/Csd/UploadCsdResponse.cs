using System.Collections.Generic;
using SW.Helpers;
using SW.Entities;
using System.Runtime.Serialization;
using System;

namespace SW.Services.Csd
{
    public class UploadCsdResponse : Response
    {
        [DataMember(Name = "data")]
        public string data { get; set; }
    }
}