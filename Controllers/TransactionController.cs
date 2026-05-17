using Microsoft.AspNetCore.Mvc;
using SistemGestionareFinantePersonale1.Models;
using SistemGestionareFinantePersonale1.Patterns.Adapter;
using SistemGestionareFinantePersonale1.Patterns.Command;
using SistemGestionareFinantePersonale1.Patterns.Facade;
using SistemGestionareFinantePersonale1.Patterns.FactoryMethod;
using SistemGestionareFinantePersonale1.Patterns.Flyweight;
using SistemGestionareFinantePersonale1.Patterns.Interfaces;
using SistemGestionareFinantePersonale1.Patterns.Observer;
using SistemGestionareFinantePersonale1.Patterns.Proxy;
using SistemGestionareFinantePersonale1.Patterns.Strategy;
using SistemGestionareFinantePersonale1.Services;
using SistemGestionareFinantePersonale1.Patterns.ChainOfResponsibility;
using SistemGestionareFinantePersonale1.Patterns.State;
using SistemGestionareFinantePersonale1.Patterns.Mediator;
using SistemGestionareFinantePersonale1.Patterns.TemplateMethod;
using SistemGestionareFinantePersonale1.Patterns.Visitor;
using SistemGestionareFinantePersonale1.Patterns.Decorator;

namespace SistemGestionareFinantePersonale1.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly TransactionFacade _facade;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = new TransactionServiceProxy(transactionService);
            _facade = new TransactionFacade(transactionService);
        }

        public IActionResult Index()
        {
            var transactions = _transactionService.GetAll();
            return View(transactions);
        }

        // 🔽 GET (formular)
        public IActionResult Add(string? type = null)
        {
            ViewBag.Type = string.IsNullOrWhiteSpace(type) ? "Expense" : type;
            return View();
        }

        [HttpGet]
        public IActionResult UploadReceipt()
        {
            return View(new ReceiptUploadViewModel { Type = "Expense" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadReceipt(ReceiptUploadViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var receiptContext = new ReceiptContext();
            receiptContext.Upload();
            model.State = receiptContext.State.Name;

            try
            {
                using var stream = model.Image!.OpenReadStream();
                var qrText = ReceiptQrDecoder.DecodeQrFromImageStream(stream);

                if (string.IsNullOrWhiteSpace(qrText))
                {
                    model.Error = "Nu am găsit niciun QR în imagine. Încearcă o poză mai clară.";
                    return View(model);
                }

                model.QrText = qrText;
                receiptContext.QrExtracted();
                model.State = receiptContext.State.Name;

                var document = new ReceiptDocument { QrText = qrText };
                var mediator = new ReceiptMediator(document);
                mediator.QrExtracted(qrText);

                var visitor = new ReceiptSummaryVisitor();
                document.Accept(visitor);
                model.VisitorSummary = visitor.Summary;

                return View(model);
            }

            catch (Exception ex)
            {
                model.Error = $"Eroare la citirea imaginii: {ex.Message}";
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFromReceipt(ReceiptUploadViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.QrText))
            {
                ModelState.AddModelError("", "QR lipsă.");
                return View("UploadReceipt", model);
            }

            if (model.Amount is null || model.Amount <= 0)
            {
                ModelState.AddModelError("", "Suma trebuie să fie > 0.");
                return View("UploadReceipt", model);
            }

            var type = string.IsNullOrWhiteSpace(model.Type) ? "Expense" : model.Type;

            var receiptContext = new ReceiptContext();
            receiptContext.Upload();
            receiptContext.QrExtracted();

            var document = new ReceiptDocument();
            var mediator = new ReceiptMediator(document);
            mediator.QrExtracted(model.QrText);

            var processor = new BasicReceiptProcessor();
            var transaction = processor.Process(type, model.Amount.Value, document.QrText!);

            if (type == "Expense")
            {
                var decorated = new TaxDecorator(new BasicTransaction(transaction));
                transaction.Description = decorated.GetDescription();
            }

            ICalculationStrategy strategy = type == "Income" ? new IncomeStrategy() : new ExpenseStrategy();
            transaction.Amount = strategy.Calculate(transaction.Amount);
            transaction.Category = CategoryFactory.GetCategory(type);

            if (!TransactionValidationChain.Validate(transaction, out var validationError))
            {
                ModelState.AddModelError("", validationError ?? "Tranzacție invalidă.");
                model.State = receiptContext.State.Name;
                return View("UploadReceipt", model);
            }

            var notifier = new TransactionNotifier();
            notifier.Attach(new ConsoleObserver());
            notifier.Notify("Tranzacție creată din bon fiscal!");

            var command = new AddTransactionCommand(_transactionService, transaction);
            command.Execute();

            receiptContext.TransactionCreated();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Add(string type, decimal amount, string? description, string? category)
        {
            if (amount <= 0)
            {
                ModelState.AddModelError("", "Suma trebuie să fie mai mare ca zero.");
                return View();
            }

            TransactionCreator creator;

            if (type == "Income")
                creator = new IncomeTransactionCreator();
            else
                creator = new ExpenseTransactionCreator();

            Transaction transaction = creator.CreateTransaction(amount);

            // 🔽 Adapter
            var external = new ExternalTransaction
            {
                Value = (double)amount,
                Info = string.IsNullOrWhiteSpace(description)
                    ? (type == "Expense" ? "Cheltuială" : "Venit")
                    : description
            };

            var adapter = new TransactionAdapter(external);
            var adaptedTransaction = adapter.GetTransaction();

            transaction.Amount = adaptedTransaction.Amount;
            transaction.Description = adaptedTransaction.Description;
            transaction.Date = adaptedTransaction.Date;

            // 🔽 Strategy
            ICalculationStrategy strategy;

            if (type == "Income")
                strategy = new IncomeStrategy();
            else
                strategy = new ExpenseStrategy();

            transaction.Amount = strategy.Calculate(transaction.Amount);

            // 🔽 Flyweight
            transaction.Category = CategoryFactory.GetCategory(string.IsNullOrWhiteSpace(category) ? type : category);

            if (type == "Expense")
            {
                var decorated = new TaxDecorator(new BasicTransaction(transaction));
                transaction.Description = decorated.GetDescription();
            }

            if (!TransactionValidationChain.Validate(transaction, out var validationError))
            {
                ModelState.AddModelError("", validationError ?? "Tranzacție invalidă.");
                return View();
            }

            // 🔽 Observer
            var notifier = new TransactionNotifier();
            notifier.Attach(new ConsoleObserver());
            notifier.Notify("Tranzacție adăugată!");

            // 🔽 Command (înlocuiește Facade)
            var command = new AddTransactionCommand(_transactionService, transaction);
            command.Execute();

            return RedirectToAction("Index");
        }
    }
}
//        [HttpPost]
//        public IActionResult Add(string type, decimal amount)
//        {
//            TransactionCreator creator;

//            if (type == "Income")
//                creator = new IncomeTransactionCreator();
//            else
//                creator = new ExpenseTransactionCreator();

//            Transaction transaction = creator.CreateTransaction(amount);

//            // 🔽 ADAUGI ASTA (Adapter)
//            var external = new ExternalTransaction
//            {
//                Value = (double)amount,
//                Info = "External transaction"
//            };

//            var adapter = new TransactionAdapter(external);
//            var adaptedTransaction = adapter.GetTransaction();
//            ICalculationStrategy strategy;

//            if (type == "Income")

//                strategy = new IncomeStrategy();

//            else

//                strategy = new ExpenseStrategy();

//            adaptedTransaction.Amount = strategy.Calculate(adaptedTransaction.Amount);
//            adaptedTransaction.Category = CategoryFactory.GetCategory(type);


//            // folosim rezultatul adaptat
//            _facade.AddTransaction(adaptedTransaction);

//            return RedirectToAction("Index");
//        }
//    }
//}  
