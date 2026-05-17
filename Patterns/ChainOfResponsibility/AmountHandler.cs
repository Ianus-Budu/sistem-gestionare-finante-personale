using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.ChainOfResponsibility
{
    public class AmountHandler : TransactionHandler
    {
        public override bool Handle(Transaction transaction, out string? error)
        {
            if (transaction.Amount <= 0)
            {
                error = "Suma trebuie să fie > 0.";
                return false;
            }

            return Next(transaction, out error);
        }
    }
}