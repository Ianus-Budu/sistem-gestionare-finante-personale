using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.ChainOfResponsibility
{
    public abstract class TransactionHandler
    {
        protected TransactionHandler? _next;

        public TransactionHandler SetNext(TransactionHandler next)
        {
            _next = next;
            return next;
        }

        protected bool Next(Transaction transaction, out string? error)
        {
            if (_next == null)
            {
                error = null;
                return true;
            }

            return _next.Handle(transaction, out error);
        }

        public abstract bool Handle(Transaction transaction, out string? error);
    }
}