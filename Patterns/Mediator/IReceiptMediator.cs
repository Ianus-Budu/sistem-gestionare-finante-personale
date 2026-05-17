using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Patterns.Mediator
{
    public interface IReceiptMediator
    {
        void QrExtracted(string qrText);
        Transaction? BuildTransaction(string type, decimal amount);
    }
}
