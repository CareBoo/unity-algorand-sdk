using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ImportMultisigRequest
        : IEquatable<ImportMultisigRequest>
    {
        [AlgoApiField("multisig_version", null)]
        public Optional<byte> Version;

        [AlgoApiField("pks", null)]
        public Ed25519.PublicKey[] PublicKeys;

        [AlgoApiField("threshold", null)]
        public Optional<byte> Threshold;

        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        public bool Equals(ImportMultisigRequest other)
        {
            return Version.Equals(other.Version)
                && ArrayComparer.Equals(PublicKeys, other.PublicKeys)
                && Threshold.Equals(other.Threshold)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                ;
        }
    }
}
