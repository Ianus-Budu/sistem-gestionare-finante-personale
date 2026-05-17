using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.Decorator
{
    public interface ITransactionComponent
    {
        string GetDescription();
    }
}