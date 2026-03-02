using SistemGestionareFinantePersonale1.Patterns.Interfaces;

namespace SistemGestionareFinantePersonale1.Patterns.AbstractFactory
{
    public class DefaultTransactionFactory : ITransactionFactory
    {
        public ITransactionValidator CreateValidator()
            => new DefaultTransactionValidator();

        public ITransactionFormatter CreateFormatter()
            => new DefaultTransactionFormatter();
    }
}