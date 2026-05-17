using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.Memento
{
    public class TransactionMemento
    {
        public Transaction State { get; }

        public TransactionMemento(Transaction state)
        {
            State = state;
        }
    }
}