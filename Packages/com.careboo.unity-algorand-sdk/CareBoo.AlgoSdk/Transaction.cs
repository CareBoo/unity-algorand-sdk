namespace AlgoSdk
{
    public interface ITransaction
    {
        Transaction.Header.ReadOnly Header { get; }
    }

    public enum TransactionType : ushort
    {
        None,
        Payment,
        KeyRegistration,
        AssetTransfer,
        AssetFreeze,
        AssetConfiguration
    }

    public static partial class Transaction
    {

    }
}
