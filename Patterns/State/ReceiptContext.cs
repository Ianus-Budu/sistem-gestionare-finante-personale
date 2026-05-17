namespace SistemGestionareFinantePersonale1.Patterns.State
{
    public class ReceiptContext
    {
        public ReceiptContext(IReceiptState? initialState = null)
        {
            State = initialState ?? new AwaitingUploadState();
        }

        public IReceiptState State { get; private set; }

        public void Upload() => State = State.OnUpload();
        public void QrExtracted() => State = State.OnQrExtracted();
        public void TransactionCreated() => State = State.OnTransactionCreated();
    }
}
