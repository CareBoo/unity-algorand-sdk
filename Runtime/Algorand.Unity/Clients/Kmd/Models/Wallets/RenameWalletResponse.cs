using System;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct RenameWalletResponse
        : IEquatable<RenameWalletResponse>
    {
        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
        public string Message;

        [AlgoApiField("wallet")]
        public Wallet Wallet;

        public bool Equals(RenameWalletResponse other)
        {
            return Error.Equals(other.Error)
                && Message.Equals(other.Message)
                && Wallet.Equals(other.Wallet)
                ;
        }
    }
}
