using System;

namespace SistemGestionareFinantePersonale1.Patterns.Observer
{
    public class ConsoleObserver : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine(message);
        }
    }
}