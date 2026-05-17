using SistemGestionareFinantePersonale1.Models;
using SistemGestionareFinantePersonale1.Patterns.Interfaces;

namespace SistemGestionareFinantePersonale1.Patterns.Proxy
{
    public class TransactionServiceProxy : ITransactionService
    {
        private readonly ITransactionService _service;

        public TransactionServiceProxy(ITransactionService service)
        {
            _service = service;
        }

        public void Add(Transaction transaction)
        {
            if (transaction.Amount > 10000)
                throw new Exception("Limit exceeded");

            _service.Add(transaction);
        }

        // 🔽 CORECT
        public IEnumerable<Transaction> GetAll()
        {
            return _service.GetAll();
        }
    }
}