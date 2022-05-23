using Unity.Collections;

namespace AlgoSdk.Abi
{
    public readonly struct SingleArg<T>
        : IArgEnumerator<SingleArg<T>>
        where T : IAbiType
    {
        readonly T arg;

        public SingleArg(T arg)
        {
            this.arg = arg;
        }

        public bool TryNext(out SingleArg<T> next)
        {
            next = this;
            return false;
        }

        public bool TryPrev(out SingleArg<T> prev)
        {
            prev = this;
            return false;
        }

        public NativeArray<byte> Encode(Method.Arg argDefinition, Allocator allocator)
        {
            return arg.Encode(argDefinition, allocator);
        }

        public int Length(Method.Arg argDefinition)
        {
            return arg.Length(argDefinition);
        }

        public bool IsStatic => arg.IsStatic;

        public string AbiTypeName => arg.AbiTypeName;
    }
}
