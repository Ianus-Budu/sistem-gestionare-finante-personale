namespace SistemGestionareFinantePersonale1.Patterns.FactoryMethod
{
    public class ExpenseTransaction
    {
        public decimal Amount { get; set; }

        public string GetDescription() => "Expense";
        public decimal GetAmount() => Amount;
    }
}
