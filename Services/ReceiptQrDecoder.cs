using System.Drawing;
using ZXing;
using ZXing.Windows.Compatibility;

namespace SistemGestionareFinantePersonale1.Services
{
    public static class ReceiptQrDecoder
    {
        public static string? DecodeQrFromImageStream(Stream imageStream)
        {
            using var bitmap = new Bitmap(imageStream);

            var reader = new BarcodeReader
            {
                AutoRotate = true,
                TryInverted = true
            };

            var result = reader.Decode(bitmap);
            return result?.Text;
        }
    }
}
