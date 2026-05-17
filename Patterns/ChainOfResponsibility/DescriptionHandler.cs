using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.ChainOfResponsibility
{
    public class DescriptionHandler : TransactionHandler
    {
        public override bool Handle(Transaction transaction, out string? error)
        {
            if (string.IsNullOrEmpty(transaction.Description))
            {
                error = "Descrierea este obligatorie.";
                return false;
            }

            return Next(transaction, out error);
        }
    }
}