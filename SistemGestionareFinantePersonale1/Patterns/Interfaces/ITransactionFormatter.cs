using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.Interfaces
{
    public interface ITransactionFormatter
    {
        string Format(Transaction transaction);
    }
}