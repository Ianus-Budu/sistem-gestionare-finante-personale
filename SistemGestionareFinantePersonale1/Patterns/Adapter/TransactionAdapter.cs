using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.Adapter
{
    public class TransactionAdapter : ITransactionTarget
    {
        private readonly ExternalTransaction _external;

        public TransactionAdapter(ExternalTransaction external)
        {
            _external = external;
        }

        public Transaction GetTransaction()
        {
            return new Transaction
            {
                Amount = (decimal)_external.Value,
                Description = _external.Info,
                Type = "Income",
                Date = DateTime.Now
            };
        }
    }
}