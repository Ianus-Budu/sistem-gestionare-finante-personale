namespace SistemGestionareFinantePersonale1.Patterns.Strategy
{
    public class ExpenseStrategy : ICalculationStrategy
    {
        public decimal Calculate(decimal amount)
        {
            return amount * 0.9m; // exemplu: reducere/taxă
        }
    }
}