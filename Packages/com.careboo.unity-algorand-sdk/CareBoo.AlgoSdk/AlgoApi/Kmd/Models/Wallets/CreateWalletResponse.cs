using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct CreateWalletResponse
        : IEquatable<CreateWalletResponse>
    {
        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
        public string Message;

        [AlgoApiField("wallet", null)]
        public Wallet Wallet;

        public bool Equals(CreateWalletResponse other)
        {
            return Error.Equals(other.Error)
                && Message.Equals(other.Message)
                && Wallet.Equals(other.Wallet)
                ;
        }
    }
}
