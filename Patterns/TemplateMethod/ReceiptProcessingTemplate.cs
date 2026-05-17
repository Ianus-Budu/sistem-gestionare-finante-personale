using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.TemplateMethod
{
    public abstract class ReceiptProcessingTemplate
    {
        public Transaction Process(string type, decimal amount, string qrText)
        {
            ValidateInputs(type, amount, qrText);
            var transaction = CreateBaseTransaction(type, amount);
            ApplyQrData(transaction, qrText);
            FinalizeTransaction(transaction);
            return transaction;
        }

        protected virtual void ValidateInputs(string type, decimal amount, string qrText)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("type");
            if (amount <= 0)
                throw new ArgumentException("amount");
            if (string.IsNullOrWhiteSpace(qrText))
                throw new ArgumentException("qrText");
        }

        protected virtual Transaction CreateBaseTransaction(string type, decimal amount)
        {
            return new Transaction
            {
                Type = type,
                Amount = amount,
                Date = DateTime.Now,
                Description = "Bon fiscal"
            };
        }

        protected abstract void ApplyQrData(Transaction transaction, string qrText);

        protected virtual void FinalizeTransaction(Transaction transaction)
        {
            transaction.Description = transaction.Description?.Trim();
        }
    }
}
