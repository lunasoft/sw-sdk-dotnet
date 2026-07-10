using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Drawing;
using System.IO;

namespace SW.Helpers.Convertion
{
    /// <summary>
    /// Porte de SW.Tools.Helpers.QrCode. Requiere el paquete NuGet Gma.QrCodeNet
    /// referenciado directamente en este proyecto (antes llegaba transitivamente vía SW.Tools).
    /// </summary>
    internal static class QrCode
    {
        internal static string GetQrCode(string total, string sello, string rfcEmisor, string rfcReceptor, string uuid)
        {
            string prefix = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?";
            sello = sello.Substring(sello.Length - 8);
            total = Convert.ToDecimal(total).ToString("000000000000000000.000000");
            var path = String.Format("&id={0}&re={1}&rr={2}&tt={3}&fe={4}", uuid, rfcEmisor, rfcReceptor, total, sello);
            return Convert.ToBase64String(GetQrCode(prefix + path));
        }

        private static byte[] GetQrCode(string content)
        {
            byte[] result;
            QrEncoder qrEncoder = new QrEncoder();
            var qrCode = qrEncoder.Encode(content);
            var renderer = new GraphicsRenderer(new FixedCodeSize(140, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            using (var stream = new MemoryStream())
            {
                renderer.WriteToStream(qrCode.Matrix, System.Drawing.Imaging.ImageFormat.Png, stream);
                result = stream.ToArray();
            }
            return result;
        }
    }
}
