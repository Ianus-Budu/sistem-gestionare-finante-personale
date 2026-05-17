namespace SistemGestionareFinantePersonale1.Patterns.Decorator
{
    public abstract class TransactionDecorator : ITransactionComponent
    {
        protected ITransactionComponent _component;

        public TransactionDecorator(ITransactionComponent component)
        {
            _component = component;
        }

        public virtual string GetDescription()
        {
            return _component.GetDescription();
        }
    }
}