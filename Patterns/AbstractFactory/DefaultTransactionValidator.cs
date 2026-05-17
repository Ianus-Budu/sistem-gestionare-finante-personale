using SistemGestionareFinantePersonale1.Models;
using SistemGestionareFinantePersonale1.Patterns.Interfaces;

namespace SistemGestionareFinantePersonale1.Patterns.AbstractFactory
{
    public class DefaultTransactionValidator : ITransactionValidator
    {
        public bool Validate(Transaction transaction)
        {
            throw new NotImplementedException();
        }

    }
}