using Unity.Collections;

namespace AlgoSdk.Abi
{
    public struct EmptyArgs
        : IArgEnumerator<EmptyArgs>
    {
        /// <inheritdoc />
        public int Count => 0;

        /// <inheritdoc />
        public EncodedAbiArg EncodeCurrent(AbiType type, AbiReferences references, Allocator allocator)
        {
            throw new System.NotSupportedException();
        }

        /// <inheritdoc />
        public int LengthOfCurrent(AbiType type)
        {
            throw new System.NotSupportedException();
        }

        /// <inheritdoc />
        public bool TryNext(out EmptyArgs next)
        {
            next = this;
            return false;
        }

        /// <inheritdoc />
        public bool TryPrev(out EmptyArgs prev)
        {
            prev = this;
            return false;
        }
    }
}
