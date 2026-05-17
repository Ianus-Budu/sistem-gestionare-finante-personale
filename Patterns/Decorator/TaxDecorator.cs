namespace SistemGestionareFinantePersonale1.Patterns.Decorator
{
    public class TaxDecorator : TransactionDecorator
    {
        public TaxDecorator(ITransactionComponent component) : base(component) { }

        public override string GetDescription()
        {
            return base.GetDescription() + " + Tax applied";
        }
    }
}