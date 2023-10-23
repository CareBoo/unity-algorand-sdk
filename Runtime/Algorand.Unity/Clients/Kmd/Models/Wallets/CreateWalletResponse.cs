using System;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct CreateWalletResponse
        : IEquatable<CreateWalletResponse>
    {
        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
        public string Message;

        [AlgoApiField("wallet")]
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
