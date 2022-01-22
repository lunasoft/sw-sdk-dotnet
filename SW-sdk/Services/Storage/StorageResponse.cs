using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SW.Services.Storage
{
    public class StorageResponse : Entities.Response
    {
        [DataMember]
        public Data data { get; set; }
    }
    public partial class Data
    {
        [DataMember]
        public MetaData metaData { get; set; }
        [DataMember]
        public List<Records> records { get; set; }
    }
    public partial class Records
    {
        [DataMember]
        public string status { get; set; }
      
        [DataMember]
        public string urlPdf { get; set; }
        [DataMember]
        public string urlXml { get; set; }
        [DataMember]
        public bool hasAddenda { get; set; }
        [DataMember]
        public object addenda { get; set; }
        [DataMember]
        public string idDealer { get; set; }
        [DataMember]
        public string idUser { get; set; }
        [DataMember]
        public string version { get; set; }
        [DataMember]
        public string serie { get; set; }
        [DataMember]
        public string folio { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public string numeroCertificado { get; set; }
        [DataMember]
        public double subtotal { get; set; }
        [DataMember]
        public double descuento { get; set; }
        [DataMember]
        public double total { get; set; }
        [DataMember]
        public string moneda { get; set; }
        [DataMember]
        public double tipoCambio { get; set; }
        [DataMember]
        public string tipoDeComprobante { get; set; }
        [DataMember]
        public string metodoPago { get; set; }
        [DataMember]
        public string formaPago { get; set; }
        [DataMember]
        public string condicionesPago { get; set; }
        [DataMember]
        public string luegarExpedicion { get; set; }
        [DataMember]
        public string rfcEmisor { get; set; }
        [DataMember]
        public string nombreEmisor { get; set; }
        [DataMember]
        public string regimenFiscal { get; set; }
        [DataMember]
        public string rfcReceptor { get; set; }
        [DataMember]
        public string nombreReceptor { get; set; }
        [DataMember]
        public string residenciaFiscal { get; set; }
        [DataMember]
        public string numRegIdTrib { get; set; }
        [DataMember]
        public string usoCFDI { get; set; }
        [DataMember]
        public double totalImpuestosTraslados { get; set; }
        [DataMember]
        public double totalImpuestosRetencion { get; set; }
        [DataMember]
        public double trasladosIVA { get; set; }
        [DataMember]
        public double trasladosIEPS { get; set; }
        [DataMember]
        public double retencionesISR { get; set; }
        [DataMember]
        public double retencionesIVA { get; set; }
        [DataMember]
        public double retencionesIEPS { get; set; }
        [DataMember]
        public double totalImpuestosLocalesTraslados { get; set; }
        [DataMember]
        public double totalImpuestosLocalesRetencion { get; set; }
        [DataMember]
        public string complementos { get; set; }
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public DateTime fechaTimbrado { get; set; }
        [DataMember]
        public string rfcProvCertif { get; set; }
        [DataMember]
        public string selloCFD { get; set; }
        [DataMember]
        public string urlAckCfdi { get; set; }
        [DataMember]
        public string urlAckCancellation { get; set; }
        [DataMember]
        public string urlAddenda { get; set; }
        [DataMember]
        public DateTime? fechaGeneracionPdf { get; set; }
        [DataMember]
        public string emisorRfc { get; set; }
        [DataMember]
        public string emisorNombre { get; set; }
        [DataMember]
        public string receptorRfc { get; set; }
        [DataMember]
        public string receptorNombre { get; set; }
        
    }
    public partial class MetaData
    {
        [DataMember]
        public string page { get; set; }
        [DataMember]
        public string perPage { get; set; }
        [DataMember]
        public string pageCount { get; set; }
        [DataMember]
        public string totalCount { get; set; }
        [DataMember]
        public Links links { get; set; }
    }
    public partial class Links
    {
        public string current { get; set; }
    }
}
