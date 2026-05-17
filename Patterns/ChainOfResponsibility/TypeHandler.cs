using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.ChainOfResponsibility
{
    public class TypeHandler : TransactionHandler
    {
        public override bool Handle(Transaction transaction, out string? error)
        {
            if (string.IsNullOrWhiteSpace(transaction.Type) || (transaction.Type != "Income" && transaction.Type != "Expense"))
            {
                error = "Tip tranzacție invalid.";
                return false;
            }

            return Next(transaction, out error);
        }
    }
}
