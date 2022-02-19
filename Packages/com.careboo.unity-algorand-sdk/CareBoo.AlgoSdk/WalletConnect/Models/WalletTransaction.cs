using System;

namespace AlgoSdk.WalletConnect
{
    /// <summary>
    /// A struct representing the serialized data for a WalletConnect Transaction
    /// 
    /// see https://developer.algorand.org/docs/get-details/walletconnect/walletconnect-schema/
    /// </summary>
    [AlgoApiObject]
    public struct WalletTransaction
        : IEquatable<WalletTransaction>
    {
        /// <summary>
        /// A transaction in canonical messagepack format.
        /// </summary>
        [AlgoApiField("txn", null)]
        public byte[] Txn;

        /// <summary>
        /// Optional <see cref="Address"/> used to sign the transaction when
        /// the account is rekeyed. Also called the signor/sgnr.
        /// </summary>
        [AlgoApiField("authAddr", null)]
        public Address AuthAddr;

        /// <summary>
        /// Optional multisig metadata used to sign the transaction.
        /// </summary>
        [AlgoApiField("msig", null)]
        public MultisigMetadata Msig;

        /// <summary>
        /// Optional list of addresses that must sign the transactions.
        /// </summary>
        [AlgoApiField("signers", null)]
        public Address[] Signers;

        /// <summary>
        /// Optional message explaining the reason of the transaction.
        /// </summary>
        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(WalletTransaction other)
        {
            return ArrayComparer.Equals(Txn, other.Txn)
                && AuthAddr.Equals(other.AuthAddr)
                && Msig.Equals(other.Msig)
                && ArrayComparer.Equals(Signers, other.Signers)
                && StringComparer.Equals(Message, other.Message)
                ;
        }
    }
}
