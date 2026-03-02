using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.FactoryMethod
{
    public class IncomeTransactionCreator : TransactionCreator
    {
        public override Transaction CreateTransaction(decimal amount)
        {
            return new Transaction
            {
                Type = "Income",
                Amount = amount
            };
        }
    }
}