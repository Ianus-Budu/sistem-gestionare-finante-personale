using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.Interfaces
{
    public interface ITransactionValidator
    {
        bool Validate(Transaction transaction);
    }
}