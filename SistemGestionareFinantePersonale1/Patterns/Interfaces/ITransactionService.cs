using System.Collections.Generic;
using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.Interfaces
{
    public interface ITransactionService
    {
        void Add(Transaction transaction);
        IEnumerable<Transaction> GetAll();
    }
}
