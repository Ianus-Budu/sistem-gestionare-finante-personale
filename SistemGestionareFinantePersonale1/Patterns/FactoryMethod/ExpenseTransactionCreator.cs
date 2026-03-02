using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.FactoryMethod
{
    public class ExpenseTransactionCreator : TransactionCreator
    {
        public override Transaction CreateTransaction(decimal amount)
        {
            return new Transaction
            {
                Type = "Expense",
                Amount = amount
            };
        }
    }
}