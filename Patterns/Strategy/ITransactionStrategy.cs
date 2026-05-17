namespace SistemGestionareFinantePersonale1.Patterns.Strategy
{
    public interface ICalculationStrategy
    {
        decimal Calculate(decimal amount);
    }
}