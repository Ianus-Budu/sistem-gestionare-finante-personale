namespace SistemGestionareFinantePersonale1.Patterns.Visitor
{
    public interface IReceiptVisitable
    {
        void Accept(IReceiptVisitor visitor);
    }

    public interface IReceiptVisitor
    {
        void Visit(ReceiptDocument document);
    }

    public class ReceiptDocument : IReceiptVisitable
    {
        public string? QrText { get; set; }

        public void Accept(IReceiptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class ReceiptSummaryVisitor : IReceiptVisitor
    {
        public string Summary { get; private set; } = string.Empty;

        public void Visit(ReceiptDocument document)
        {
            var text = document.QrText;
            Summary = string.IsNullOrWhiteSpace(text) ? "(fără QR)" : $"QR length: {text.Length}";
        }
    }
}
