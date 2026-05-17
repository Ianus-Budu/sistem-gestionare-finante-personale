using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.Decorator
{
    public class BasicTransaction : ITransactionComponent
    {
        private readonly Transaction _transaction;

        public BasicTransaction(Transaction transaction)
        {
            _transaction = transaction;
        }

        public string GetDescription()
        {
            return _transaction.Description;
        }
    }
}