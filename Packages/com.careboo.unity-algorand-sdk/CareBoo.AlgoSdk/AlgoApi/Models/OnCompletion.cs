using AlgoSdk.Formatters;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(OnCompletionFormatter))]
    public enum OnCompletion : byte
    {
        None,
        NoOp,
        OptIn,
        CloseOut,
        Clear,
        Update,
        Delete,
        Count
    }
}
