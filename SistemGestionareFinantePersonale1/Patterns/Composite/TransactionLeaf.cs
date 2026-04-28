using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.Composite
{
    public class TransactionLeaf : ITransactionComponent
    {
        private readonly Transaction _transaction;

        public TransactionLeaf(Transaction transaction)
        {
            _transaction = transaction;
        }

        public decimal GetAmount()
        {
            return _transaction.Amount;
        }
    }
}