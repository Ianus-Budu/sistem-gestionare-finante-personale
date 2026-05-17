using SistemGestionareFinantePersonale1.Models;

public class TransactionBuilder : ITransactionBuilder
{
    private Transaction transaction = new Transaction();

    public ITransactionBuilder SetType(string type)
    {
        transaction.Type = type;
        return this;
    }

    public ITransactionBuilder SetAmount(decimal amount)
    {
        transaction.Amount = amount;
        return this;
    }

    public ITransactionBuilder SetCategory(string category)
    {
        transaction.Category = category;
        return this;
    }

    public ITransactionBuilder SetDescription(string description)
    {
        transaction.Description = description;
        return this;
    }

    public Transaction Build()
    {
        return transaction;
    }
}