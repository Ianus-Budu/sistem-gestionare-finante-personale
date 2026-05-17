using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.TemplateMethod
{
    public class BasicReceiptProcessor : ReceiptProcessingTemplate
    {
        protected override void ApplyQrData(Transaction transaction, string qrText)
        {
            // Minimal: keep QR in description so we demonstrate Template Method without tight coupling.
            transaction.Description = $"Bon fiscal: {qrText}";
        }
    }
}
