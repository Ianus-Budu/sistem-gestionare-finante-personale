using SistemGestionareFinantePersonale1.Models;
using SistemGestionareFinantePersonale1.Patterns.Interfaces;

namespace SistemGestionareFinantePersonale1.Patterns.Concrete
{
    public class TransactionFormatter : ITransactionFormatter
    {
        public string Format(Transaction transaction)
        {
            // Minimal: afișează tipul tranzacției și suma
            return $"{transaction.Type}: {transaction.Amount} MDL";
        }
    }
}