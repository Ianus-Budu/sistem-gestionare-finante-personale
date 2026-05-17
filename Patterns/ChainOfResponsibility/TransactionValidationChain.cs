using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.ChainOfResponsibility
{
    public static class TransactionValidationChain
    {
        public static TransactionHandler BuildDefault()
        {
            var type = new TypeHandler();
            var amount = new AmountHandler();
            var desc = new DescriptionHandler();

            type.SetNext(amount).SetNext(desc);
            return type;
        }

        public static bool Validate(Transaction transaction, out string? error)
        {
            var chain = BuildDefault();
            return chain.Handle(transaction, out error);
        }
    }
}
