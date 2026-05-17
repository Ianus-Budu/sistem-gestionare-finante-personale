using SistemGestionareFinantePersonale1.Patterns.Interfaces;

namespace SistemGestionareFinantePersonale1.Patterns.AbstractFactory
{
    public interface ITransactionFactory
    {
        ITransactionValidator CreateValidator();
        ITransactionFormatter CreateFormatter();
    }
}