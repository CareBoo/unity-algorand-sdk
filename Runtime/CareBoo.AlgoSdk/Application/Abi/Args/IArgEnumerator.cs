namespace AlgoSdk.Abi
{
    public interface IArgEnumerator<T>
        : IAbiValue
        where T : IArgEnumerator<T>
    {
        public bool TryNext(out T next);
        public bool TryPrev(out T prev);
    }
}
