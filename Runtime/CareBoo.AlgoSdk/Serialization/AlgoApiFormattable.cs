namespace AlgoSdk
{
    public interface IAlgoApiFormattable<T>
    {
        IAlgoApiFormatter<T> GetAlgoApiFormatter();
    }
}
