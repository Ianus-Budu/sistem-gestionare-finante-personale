using Microsoft.AspNetCore.Mvc;
using SistemGestionareFinantePersonale1.Patterns.Interfaces;
using SistemGestionareFinantePersonale1.Patterns.FactoryMethod;
using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public IActionResult Index()
        {
            var transactions = _transactionService.GetAll();
            return View(transactions);
        }

        // 🔽 AICI apare PATTERNUL (IMPORTANT)
        [HttpPost]
        public IActionResult Add(string type, decimal amount)
        {
            TransactionCreator creator;

            if (type == "Income")
                creator = new IncomeTransactionCreator();
            else
                creator = new ExpenseTransactionCreator();

            Transaction transaction = creator.CreateTransaction(amount);

            _transactionService.Add(transaction);

            return RedirectToAction("Index");
        }
    }
}