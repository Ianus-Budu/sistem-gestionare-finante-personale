namespace SistemGestionareFinantePersonale1.Patterns.Strategy
{
    public class IncomeStrategy : ICalculationStrategy
    {
        public decimal Calculate(decimal amount)
        {
            return amount;
        }
    }
}