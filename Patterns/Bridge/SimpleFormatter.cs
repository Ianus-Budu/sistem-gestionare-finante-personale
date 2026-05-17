using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.Bridge
{
    public class SimpleFormatter : ITransactionFormatter
    {
        public string Format(Transaction transaction)
        {
            return $"{transaction.Amount} lei - {transaction.Description}";
        }
    }
}