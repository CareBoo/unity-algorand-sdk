namespace AlgoSdk.Abi
{
    public static partial class Args
    {
        public static SingleArg<T> Add<T>(T arg)
            where T : IAbiType
        {
            return new SingleArg<T>(arg);
        }

        public static ArgsList<THead, TTail> Add<THead, TTail>(
            this TTail tail,
            THead head
        )
            where THead : IAbiType
            where TTail : struct, IArgEnumerator<TTail>
        {
            return new ArgsList<THead, TTail>(head, tail);
        }
    }
}
