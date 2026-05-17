using SistemGestionareFinantePersonale1.Models;
using SistemGestionareFinantePersonale1.Patterns.Interfaces;

namespace SistemGestionareFinantePersonale1.Patterns.Command
{
    public class AddTransactionCommand : ICommand
    {
        private readonly ITransactionService _service;
        private readonly Transaction _transaction;

        public AddTransactionCommand(ITransactionService service, Transaction transaction)
        {
            _service = service;
            _transaction = transaction;
        }

        public void Execute()
        {
            _service.Add(_transaction);
        }
    }
}