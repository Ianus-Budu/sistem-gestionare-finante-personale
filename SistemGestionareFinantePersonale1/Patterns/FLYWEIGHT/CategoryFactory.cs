using System.Collections.Generic;

namespace SistemGestionareFinantePersonale1.Patterns.Flyweight
{
    public class CategoryFactory
    {
        private static Dictionary<string, TransactionCategory> _categories = new();

        public static TransactionCategory GetCategory(string name)
        {
            if (!_categories.ContainsKey(name))
            {
                _categories[name] = new TransactionCategory { Name = name };
            }

            return _categories[name];
        }
    }
}