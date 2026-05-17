using SistemGestionareFinantePersonale1.Models;
using SistemGestionareFinantePersonale1.Patterns.Visitor;

namespace SistemGestionareFinantePersonale1.Patterns.Mediator
{
    public class ReceiptMediator : IReceiptMediator
    {
        private readonly ReceiptDocument _document;

        public ReceiptMediator(ReceiptDocument document)
        {
            _document = document;
        }

        public void QrExtracted(string qrText)
        {
            _document.QrText = qrText;
        }

        public Transaction? BuildTransaction(string type, decimal amount)
        {
            // For now, we create a transaction using the provided values.
            // QR parsing can be added later.
            return new Transaction
            {
                Type = type,
                Amount = amount,
                Description = "Bon fiscal",
                Date = DateTime.Now
            };
        }
    }
}
