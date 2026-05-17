susing Microsoft.AspNetCore.Mvc;
using SistemGestionareFinantePersonale1.Patterns.Interfaces;
using SistemGestionareFinantePersonale1.Patterns.FactoryMethod;
using SistemGestionareFinantePersonale1.Models;
using SistemGestionareFinantePersonale1.Patterns.Facade;
using SistemGestionareFinantePersonale1.Patterns.Adapter;
using SistemGestionareFinantePersonale1.Patterns.Flyweight;

namespace SistemGestionareFinantePersonale1.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly TransactionFacade _facade;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
            _facade = new TransactionFacade(transactionService);
        }

        public IActionResult Index()
        {
            var transactions = _transactionService.GetAll();
            return View(transactions);
        }

        // 🔽 GET (formular)
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string type, decimal amount)
        {
            TransactionCreator creator;

            if (type == "Income")
                creator = new IncomeTransactionCreator();
            else
                creator = new ExpenseTransactionCreator();

            Transaction transaction = creator.CreateTransaction(amount);

            // 🔽 ADAUGI ASTA (Adapter)
            var external = new ExternalTransaction
            {
                Value = (double)amount,
                Info = "External transaction"
            };

            var adapter = new TransactionAdapter(external);
            var adaptedTransaction = adapter.GetTransaction();

            adaptedTransaction.Category = CategoryFactory.GetCategory(type);

            // folosim rezultatul adaptat
            _facade.AddTransaction(adaptedTransaction);

            return RedirectToAction("Index");
        }
    }
}

        // 🔽 POST (Factory Method - LASI ASA!)
        //    [HttpPost]
        //    public IActionResult Add(string type, decimal amount)
        //    {
        //        TransactionCreator creator;

//        if (type == "Income")
//            creator = new IncomeTransactionCreator();
//        else
//            creator = new ExpenseTransactionCreator();

//        Transaction transaction = creator.CreateTransaction(amount);

//        _facade.AddTransaction(transaction);

//        return RedirectToAction("Index");
//    }
//}
// }
//    public class TransactionController : Controller
//    {
//        private readonly ITransactionService _transactionService;

//        public TransactionController(ITransactionService transactionService)
//        {
//            _transactionService = transactionService;
//        }

//        public IActionResult Index()
//        {
//            var transactions = _transactionService.GetAll();
//            return View(transactions);
//        }

//        // 🔽 AICI apare PATTERNUL (IMPORTANT)
//        [HttpPost]
//        public IActionResult Add(string type, decimal amount)
//        {
//            TransactionCreator creator;

//            if (type == "Income")
//                creator = new IncomeTransactionCreator();
//            else
//                creator = new ExpenseTransactionCreator();

//            Transaction transaction = creator.CreateTransaction(amount);

//            _transactionService.Add(transaction);

//            return RedirectToAction("Index");
//        }
//    }
//}   