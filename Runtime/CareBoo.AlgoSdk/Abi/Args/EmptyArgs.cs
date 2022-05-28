using Unity.Collections;

namespace AlgoSdk.Abi
{
    public struct EmptyArgs
        : IArgEnumerator<EmptyArgs>
    {
        public EncodedAbiArg EncodeCurrent(AbiType type, AbiReferences references, Allocator allocator)
        {
            throw new System.NotSupportedException();
        }

        public int LengthOfCurrent(AbiType type)
        {
            throw new System.NotSupportedException();
        }

        public bool TryNext(out EmptyArgs next)
        {
            next = this;
            return false;
        }

        public bool TryPrev(out EmptyArgs prev)
        {
            prev = this;
            return false;
        }
    }
}
