using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.Iterator
{
    public class TransactionCollection
    {
        private List<Transaction> _transactions = new();

        public void Add(Transaction t)
        {
            _transactions.Add(t);
        }

        public IEnumerator<Transaction> GetIterator()
        {
            return _transactions.GetEnumerator();
        }
    }
}