using SistemGestionareFinantePersonale1.Models;
using SistemGestionareFinantePersonale1.Patterns.Interfaces;

namespace SistemGestionareFinantePersonale1.Patterns.Facade
{
    public class TransactionFacade
    {
        private readonly ITransactionService _service;

        public TransactionFacade(ITransactionService service)
        {
            _service = service;
        }

        public void AddTransaction(Transaction transaction)
        {
            // validare simplă
            if (transaction.Amount <= 0)
                throw new Exception("Invalid amount");

            _service.Add(transaction);
        }
    }
}