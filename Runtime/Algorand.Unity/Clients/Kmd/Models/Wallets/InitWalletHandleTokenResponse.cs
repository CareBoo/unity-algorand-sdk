using System;
using Unity.Collections;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct InitWalletHandleTokenResponse
        : IEquatable<InitWalletHandleTokenResponse>
    {
        [AlgoApiField("wallet_handle_token")]
        public FixedString128Bytes WalletHandleToken;

        [AlgoApiField("error")]
        public Optional<bool> Error;

        [AlgoApiField("message")]
        public string Message;

        public bool Equals(InitWalletHandleTokenResponse other)
        {
            return StringComparer.Equals(WalletHandleToken, other.WalletHandleToken)
                && Error.Equals(other.Error)
                && Message.Equals(other.Message)
                ;
        }
    }
}
