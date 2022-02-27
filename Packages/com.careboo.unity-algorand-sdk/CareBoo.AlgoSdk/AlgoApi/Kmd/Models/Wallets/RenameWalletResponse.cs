using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct RenameWalletResponse
        : IEquatable<RenameWalletResponse>
    {
        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        [AlgoApiField("wallet", null)]
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
