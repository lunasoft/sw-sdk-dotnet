using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SW.Services.Taxpayer
{
    public class TaxpayerResponse : Entities.Response
    {
        [DataMember]
        public Data data { get; set; }
    }
    public partial class Data
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string rfc { get; set; }
        [DataMember]
        public string nombre_Contribuyente { get; set; }
        [DataMember]
        public string situacion_del_contribuyente { get; set; }
        [DataMember]
        public string numero_y_fecha_oficio_global_presuncion { get; set; }
        [DataMember]
        public string publicacion_pagina_SAT_presuntos { get; set; }
        [DataMember]
        public string publicacion_DOF_presuntos { get; set; }
        [DataMember]
        public string publicacion_pagina_SAT_desvirtuados { get; set; }
        [DataMember]
        public string numero_fecha_oficio_global_contribuyentes_que_desvirtuaron { get; set; }
        [DataMember]
        public string publicacion_DOF_desvirtuados { get; set; }
        [DataMember]
        public string numero_fecha_oficio_global_definitivos { get; set; }
        [DataMember]
        public string publicacion_pagina_SAT_definitivos { get; set; }
        [DataMember]
        public string publicacion_DOF_definitivos { get; set; }
        [DataMember]
        public string numero_fecha_oficio_global_sentencia_favorable { get; set; }
        [DataMember]
        public string publicacion_pagina_SAT_sentencia_favorable { get; set; }
        [DataMember]
        public string publicacion_DOF_sentencia_favorable { get; set; }
    }
}
