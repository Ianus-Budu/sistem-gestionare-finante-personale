using SistemGestionareFinantePersonale1.Models;
using SistemGestionareFinantePersonale1.Patterns.Flyweight;

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
        transaction.Category = CategoryFactory.GetCategory(category);
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