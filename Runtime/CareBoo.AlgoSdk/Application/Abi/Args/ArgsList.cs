using Unity.Collections;

namespace AlgoSdk.Abi
{
    public readonly struct ArgsList<THead, TTail>
        : IArgEnumerator<ArgsList<THead, TTail>>
        where THead : IAbiType
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

        public NativeArray<byte> Encode(Method.Arg argDefinition, Allocator allocator)
        {
            return isHead
                ? head.Encode(argDefinition, allocator)
                : tail.Encode(argDefinition, allocator)
                ;
        }

        public int Length(Method.Arg argDefinition)
        {
            return isHead
                ? head.Length(argDefinition)
                : tail.Length(argDefinition)
                ;
        }

        public bool IsStatic => isHead
            ? head.IsStatic
            : tail.IsStatic
            ;

        public string AbiTypeName => isHead
            ? head.AbiTypeName
            : tail.AbiTypeName
            ;
    }
}
