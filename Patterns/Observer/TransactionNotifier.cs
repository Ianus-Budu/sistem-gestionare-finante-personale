using System.Collections.Generic;

namespace SistemGestionareFinantePersonale1.Patterns.Observer
{
    public class TransactionNotifier
    {
        private List<IObserver> _observers = new();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }
    }
}