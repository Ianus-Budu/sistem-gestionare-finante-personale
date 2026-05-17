using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.Bridge
{
    public interface ITransactionFormatter
    {
        string Format(Transaction transaction);
    }
}