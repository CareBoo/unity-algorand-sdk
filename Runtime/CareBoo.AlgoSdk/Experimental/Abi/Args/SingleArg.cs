using Unity.Collections;

namespace AlgoSdk.Experimental.Abi
{
    public readonly struct SingleArg<T>
        : IArgEnumerator<SingleArg<T>>
        where T : IAbiValue
    {
        readonly T arg;

        public SingleArg(T arg)
        {
            this.arg = arg;
        }

        /// <inheritdoc />
        public int Count => 1;

        /// <inheritdoc />
        public bool TryNext(out SingleArg<T> next)
        {
            next = this;
            return false;
        }

        /// <inheritdoc />
        public bool TryPrev(out SingleArg<T> prev)
        {
            prev = this;
            return false;
        }

        /// <inheritdoc />
        public EncodedAbiArg EncodeCurrent(IAbiType type, AbiReferences references, Allocator allocator)
        {
            return arg.Encode(type, references, allocator);
        }

        /// <inheritdoc />
        public int LengthOfCurrent(IAbiType type)
        {
            return arg.Length(type);
        }
    }
}
