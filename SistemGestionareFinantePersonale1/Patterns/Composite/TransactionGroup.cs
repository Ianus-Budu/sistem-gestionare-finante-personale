using System.Collections.Generic;

namespace SistemGestionareFinantePersonale1.Patterns.Composite
{
    public class TransactionGroup : ITransactionComponent
    {
        private readonly List<ITransactionComponent> _components = new();

        public void Add(ITransactionComponent component)
        {
            _components.Add(component);
        }

        public decimal GetAmount()
        {
            decimal total = 0;

            foreach (var c in _components)
            {
                total += c.GetAmount();
            }

            return total;
        }
    }
}