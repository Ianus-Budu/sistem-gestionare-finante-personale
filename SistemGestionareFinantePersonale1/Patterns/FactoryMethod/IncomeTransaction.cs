namespace SistemGestionareFinantePersonale1.Patterns.FactoryMethod
{
    public class IncomeTransaction
    {
        public decimal Amount { get; set; }

        public string GetDescription() => "Income";
        public decimal GetAmount() => Amount;
    }
}
