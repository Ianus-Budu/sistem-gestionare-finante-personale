using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.FactoryMethod
{
    public abstract class TransactionCreator
    {
        public abstract Transaction CreateTransaction(decimal amount);
    }
}