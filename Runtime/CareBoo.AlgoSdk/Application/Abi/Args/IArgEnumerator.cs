using Unity.Collections;

namespace AlgoSdk.Abi
{
    public interface IArgEnumerator<T>
        where T : IArgEnumerator<T>
    {
        public bool TryNext(out T next);
        public bool TryPrev(out T prev);
        NativeArray<byte> EncodeCurrent(AbiType type, Allocator allocator);
        int LengthOfCurrent(AbiType type);
    }
}
