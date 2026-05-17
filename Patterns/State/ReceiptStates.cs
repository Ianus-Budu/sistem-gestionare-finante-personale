namespace SistemGestionareFinantePersonale1.Patterns.State
{
    public sealed class AwaitingUploadState : IReceiptState
    {
        public string Name => "Așteaptă încărcarea";
        public bool CanExtractQr => false;
        public bool CanCreateTransaction => false;

        public IReceiptState OnUpload() => new UploadedState();
        public IReceiptState OnQrExtracted() => this;
        public IReceiptState OnTransactionCreated() => this;
    }

    public sealed class UploadedState : IReceiptState
    {
        public string Name => "Încărcat";
        public bool CanExtractQr => true;
        public bool CanCreateTransaction => false;

        public IReceiptState OnUpload() => this;
        public IReceiptState OnQrExtracted() => new QrExtractedState();
        public IReceiptState OnTransactionCreated() => this;
    }

    public sealed class QrExtractedState : IReceiptState
    {
        public string Name => "QR extras";
        public bool CanExtractQr => false;
        public bool CanCreateTransaction => true;

        public IReceiptState OnUpload() => this;
        public IReceiptState OnQrExtracted() => this;
        public IReceiptState OnTransactionCreated() => new CompletedState();
    }

    public sealed class CompletedState : IReceiptState
    {
        public string Name => "Finalizat";
        public bool CanExtractQr => false;
        public bool CanCreateTransaction => false;

        public IReceiptState OnUpload() => new UploadedState();
        public IReceiptState OnQrExtracted() => this;
        public IReceiptState OnTransactionCreated() => this;
    }
}
