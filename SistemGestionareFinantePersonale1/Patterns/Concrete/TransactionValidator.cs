using SistemGestionareFinantePersonale1.Models;
using SistemGestionareFinantePersonale1.Patterns.Interfaces;

namespace SistemGestionareFinantePersonale1.Patterns.Concrete
{
    public class TransactionValidator : ITransactionValidator
    {
        public bool Validate(Transaction transaction)
        {
            // Minimal: o tranzacție e validă dacă suma > 0
            return transaction.Amount > 0;
        }
    }
}