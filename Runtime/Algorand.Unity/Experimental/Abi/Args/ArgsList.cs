using Unity.Collections;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// Represents a linked-list of <see cref="IAbiValue"/> args given to call an ABI method.
    /// </summary>
    /// <typeparam name="THead">The type of the head argument.</typeparam>
    /// <typeparam name="TTail">The type of the tail enumerator of arguments.</typeparam>
    public readonly struct ArgsList<THead, TTail>
        : IArgEnumerator<ArgsList<THead, TTail>>
        where THead : IAbiValue
        where TTail : struct, IArgEnumerator<TTail>
    {
        private readonly THead head;
        private readonly TTail tail;
        private readonly bool isHead;

        public ArgsList(THead head, TTail tail)
        {
            this.head = head;
            this.tail = tail;
            this.isHead = false;
        }

        private ArgsList(THead head, TTail tail, bool isHead)
        {
            this.head = head;
            this.tail = tail;
            this.isHead = isHead;
        }

        /// <inheritdoc />
        public int Count => 1 + tail.Count;

        ///<inheritdoc />
        public bool TryNext(out ArgsList<THead, TTail> next)
        {
            if (isHead)
            {
                next = this;
                return false;
            }
            if (tail.TryNext(out var nextTail))
            {
                next = new ArgsList<THead, TTail>(head, nextTail);
                return true;
            }
            next = new ArgsList<THead, TTail>(head, nextTail, true);
            return true;
        }

        ///<inheritdoc />
        public bool TryPrev(out ArgsList<THead, TTail> prev)
        {
            if (isHead)
            {
                prev = new ArgsList<THead, TTail>(head, tail);
                return true;
            }
            if (tail.TryPrev(out var prevTail))
            {
                prev = new ArgsList<THead, TTail>(head, prevTail);
                return true;
            }
            prev = this;
            return false;
        }

        ///<inheritdoc />
        public EncodedAbiArg EncodeCurrent(IAbiType type, AbiReferences references, Allocator allocator)
        {
            return isHead
                ? head.Encode(type, references, allocator)
                : tail.EncodeCurrent(type, references, allocator)
                ;
        }

        ///<inheritdoc />
        public int LengthOfCurrent(IAbiType type)
        {
            return isHead
                ? head.Length(type)
                : tail.LengthOfCurrent(type)
                ;
        }
    }
}
