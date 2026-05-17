using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SistemGestionareFinantePersonale1.Models
{
    public class ReceiptUploadViewModel
    {
        [Required]
        public IFormFile? Image { get; set; }

        public string? Type { get; set; }
        public decimal? Amount { get; set; }

        public string? QrText { get; set; }
        public string? State { get; set; }
        public string? VisitorSummary { get; set; }
        public string? Error { get; set; }
    }
}
