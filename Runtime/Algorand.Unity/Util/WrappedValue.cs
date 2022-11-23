namespace Algorand.Unity
{
    public interface IWrappedValue<T>
    {
        T WrappedValue { get; set; }
    }
}
