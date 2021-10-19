using AlgoSdk.Formatters;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(OnCompletionFormatter))]
    public enum OnCompletion : byte
    {
        NoOp,
        OptIn,
        CloseOut,
        Clear,
        Update,
        Delete
    }
}
