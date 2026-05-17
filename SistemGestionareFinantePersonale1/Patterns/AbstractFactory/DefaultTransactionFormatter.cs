using SistemGestionareFinantePersonale1.Models;
using SistemGestionareFinantePersonale1.Patterns.Interfaces;

namespace SistemGestionareFinantePersonale1.Patterns.AbstractFactory
{
    public class DefaultTransactionFormatter : ITransactionFormatter
    {
        public string Format(decimal amount) => $"{amount} MDL";

        public string Format(Transaction transaction)
        {
            string st = $"{transaction} MDL";
            return st;
        }
    }
}
