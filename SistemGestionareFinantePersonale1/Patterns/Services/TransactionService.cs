using System.Collections.Generic;
using SistemGestionareFinantePersonale1.Models;
using SistemGestionareFinantePersonale1.Patterns.Interfaces;

namespace SistemGestionareFinantePersonale1.Patterns.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly List<Transaction> _transactions = new();

        public void Add(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _transactions;
        }
    }
}

