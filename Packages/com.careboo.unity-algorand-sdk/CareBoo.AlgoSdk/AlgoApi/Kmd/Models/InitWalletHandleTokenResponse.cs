using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct InitWalletHandleTokenResponse
        : IEquatable<InitWalletHandleTokenResponse>
    {
        [AlgoApiField("wallet_handle_token", null)]
        public string WalletHandleToken;

        [AlgoApiField("error", null)]
        public Optional<bool> Error;

        [AlgoApiField("message", null)]
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
