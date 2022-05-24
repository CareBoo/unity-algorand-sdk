using Unity.Collections;

namespace AlgoSdk.Abi
{
    public readonly struct ArgsList<THead, TTail>
        : IArgEnumerator<ArgsList<THead, TTail>>
        where THead : IAbiValue
        where TTail : struct, IArgEnumerator<TTail>
    {
        readonly THead head;
        readonly TTail tail;
        readonly bool isHead;

        public ArgsList(THead head, TTail tail)
        {
            this.head = head;
            this.tail = tail;
            this.isHead = false;
        }

        ArgsList(THead head, TTail tail, bool isHead)
        {
            this.head = head;
            this.tail = tail;
            this.isHead = isHead;
        }

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

        public NativeArray<byte> Encode(AbiType type, Allocator allocator)
        {
            return isHead
                ? head.Encode(type, allocator)
                : tail.Encode(type, allocator)
                ;
        }

        public int Length(AbiType type)
        {
            return isHead
                ? head.Length(type)
                : tail.Length(type)
                ;
        }
    }
}
