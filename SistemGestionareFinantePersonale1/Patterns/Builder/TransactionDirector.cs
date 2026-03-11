public class TransactionDirector
{
    public Transaction CreateExpense(ITransactionBuilder builder)
    {
        return builder
            .SetType("Expense")
            .SetAmount(100)
            .SetCategory("Food")
            .SetDescription("Restaurant")
            .Build();
    }
}