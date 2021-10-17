using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ExportMasterKeyRequest
        : IEquatable<ExportMasterKeyRequest>
    {

        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        [AlgoApiField("wallet_password", null)]
        public FixedString128Bytes WalletPassword;

        public bool Equals(ExportMasterKeyRequest other)
        {
            return WalletHandleToken.Equals(other.WalletHandleToken)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
