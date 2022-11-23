namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// Utility methods used to build <see cref="IArgEnumerator{T}"/>
    /// </summary>
    public static partial class Args
    {
        public static EmptyArgs Empty => default;

        public static SingleArg<T> Add<T>(T arg)
            where T : IAbiValue
        {
            return new SingleArg<T>(arg);
        }

        public static ArgsList<THead, TTail> Add<THead, TTail>(
            this TTail tail,
            THead head
        )
            where THead : IAbiValue
            where TTail : struct, IArgEnumerator<TTail>
        {
            return new ArgsList<THead, TTail>(head, tail);
        }

        public static ArgsArray Of(params IAbiValue[] values)
        {
            return new ArgsArray(values);
        }
    }
}
