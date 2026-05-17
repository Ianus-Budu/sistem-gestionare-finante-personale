using System.Collections.Generic;

namespace SistemGestionareFinantePersonale1.Patterns.Memento
{
    public class TransactionCaretaker
    {
        private List<TransactionMemento> _history = new();

        public void Save(TransactionMemento memento)
        {
            _history.Add(memento);
        }

        public TransactionMemento GetLast()
        {
            return _history.Last();
        }
    }
}