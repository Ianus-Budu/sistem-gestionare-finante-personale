using System;

namespace SistemGestionareFinantePersonale1.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public object Type { get; set; }
    }
}

