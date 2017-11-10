using System.Collections.Generic;
using SW.Helpers;
using System.Runtime.Serialization;

namespace SW.Services.Account
{
    public class AccountResponse : Entities.Response
    {
        [DataMember]
        public Data data { get; set; }
    }
    public partial class Data
    {
        [DataMember]
        public string idSaldoCliente { get; set; }
        [DataMember]
        public string idClienteUsuario { get; set; }
        [DataMember]
        public int saldoTimbres { get; set; }
        [DataMember]
        public int timbresUtilizados { get; set; }
        [DataMember]
        public string fechaExpiracion { get; set; }
        [DataMember]
        public bool unlimited { get; set; }
        [DataMember]
        public int timbresAsignados { get; set; }
    }
}