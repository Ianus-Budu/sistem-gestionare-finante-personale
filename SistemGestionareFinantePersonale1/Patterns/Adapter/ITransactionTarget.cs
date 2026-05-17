using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.Adapter
{
    public interface ITransactionTarget
    {
        Transaction GetTransaction();
    }
}