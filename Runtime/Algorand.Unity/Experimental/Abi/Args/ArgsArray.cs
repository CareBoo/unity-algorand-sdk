using Unity.Collections;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// Represents an array of <see cref="IAbiValue"/> args given via params to an ABI method.
    /// </summary>
    public readonly struct ArgsArray
        : IArgEnumerator<ArgsArray>
    {
        private readonly IAbiValue[] values;

        private readonly int current;

        /// <inheritdoc />
        public int Count => values?.Length ?? 0;

        public IAbiValue[] Values => values;

        public ArgsArray(IAbiValue[] values, int current)
        {
            this.values = values;
            this.current = current;
        }

        public ArgsArray(IAbiValue[] values) : this(values, 0)
        {
        }

        /// <inheritdoc />
        public EncodedAbiArg EncodeCurrent(IAbiType type, AbiReferences references, Allocator allocator)
        {
            if (Count == 0)
                throw new System.NotSupportedException("ArgsArray is empty");

            return values[current].Encode(type, references, allocator);
        }

        /// <inheritdoc />
        public int LengthOfCurrent(IAbiType type)
        {
            if (Count == 0)
                throw new System.NotSupportedException("ArgsArray is empty");

            return values[current].Length(type);
        }

        /// <inheritdoc />
        public bool TryNext(out ArgsArray next)
        {
            var nextIndex = current + 1;
            if (nextIndex >= Count)
            {
                next = this;
                return false;
            }

            next = new ArgsArray(values, nextIndex);
            return true;
        }

        /// <inheritdoc />
        public bool TryPrev(out ArgsArray prev)
        {
            var prevIndex = current - 1;
            if (prevIndex < 0)
            {
                prev = this;
                return false;
            }

            prev = new ArgsArray(values, prevIndex);
            return true;
        }
    }
}
