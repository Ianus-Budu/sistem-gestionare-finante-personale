using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.Bridge
{
    public class TransactionView
    {
        protected ITransactionFormatter _formatter;

        public TransactionView(ITransactionFormatter formatter)
        {
            _formatter = formatter;
        }

        public string Show(Transaction transaction)
        {
            return _formatter.Format(transaction);
        }
    }
}