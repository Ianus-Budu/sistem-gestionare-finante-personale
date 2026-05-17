namespace SistemGestionareFinantePersonale1.Patterns.State
{
    public interface IReceiptState
    {
        string Name { get; }
        bool CanExtractQr { get; }
        bool CanCreateTransaction { get; }

        IReceiptState OnUpload();
        IReceiptState OnQrExtracted();
        IReceiptState OnTransactionCreated();
    }
}
