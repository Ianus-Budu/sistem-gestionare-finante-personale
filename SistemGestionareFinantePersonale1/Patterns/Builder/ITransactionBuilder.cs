public interface ITransactionBuilder
{
    ITransactionBuilder SetType(string type);
    ITransactionBuilder SetAmount(decimal amount);
    ITransactionBuilder SetCategory(string category);
    ITransactionBuilder SetDescription(string description);
    Transaction Build();
}