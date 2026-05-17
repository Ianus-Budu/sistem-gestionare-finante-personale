using Microsoft.EntityFrameworkCore;
using SistemGestionareFinantePersonale1.Data;
using SistemGestionareFinantePersonale1.Models;
using SistemGestionareFinantePersonale1.Patterns.Interfaces;

namespace SistemGestionareFinantePersonale1.Patterns.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _db;

        public TransactionService(AppDbContext db)
        {
            _db = db;
        }

        public void Add(Transaction transaction)
        {
            _db.Transactions.Add(transaction);
            _db.SaveChanges();
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _db.Transactions.AsNoTracking().OrderByDescending(t => t.Date).ToList();
        }
    }
}

