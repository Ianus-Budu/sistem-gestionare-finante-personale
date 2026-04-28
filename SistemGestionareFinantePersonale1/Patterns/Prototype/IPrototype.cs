namespace SistemGestionareFinantePersonale1.Patterns.Prototype
{
    public interface IPrototype<T>
    {
        T Clone();
    }
}