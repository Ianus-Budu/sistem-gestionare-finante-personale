using SistemGestionareFinantePersonale1.Patterns.Prototype;

namespace SistemGestionareFinantePersonale1.Models
{
    public class Transaction : IPrototype<Transaction>
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        public Transaction Clone()
        {
            return (Transaction)this.MemberwiseClone();
        }
    }
}
