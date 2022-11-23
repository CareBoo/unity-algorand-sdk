using System;

namespace Algorand.Unity.WalletConnect
{
    /// <summary>
    /// A struct representing the serialized data for a WalletConnect Transaction
    /// 
    /// see https://developer.algorand.org/docs/get-details/walletconnect/walletconnect-schema/
    /// </summary>
    [AlgoApiObject]
    public partial struct WalletTransaction
        : IEquatable<WalletTransaction>
    {
        /// <summary>
        /// A transaction in canonical messagepack format.
        /// </summary>
        /// <remarks>
        /// Does not contain the txn prefix that is used when signing a transaction.
        /// See <see cref="AlgoApiSerializer.SerializeMessagePack"/> for serializing the transaction.
        /// </remarks>
        [AlgoApiField("txn")]
        public byte[] Txn;

        /// <summary>
        /// Optional message explaining the reason of the transaction.
        /// </summary>
        [AlgoApiField("message")]
        public string Message;

        /// <summary>
        /// Optional <see cref="Address"/> used to sign the transaction when
        /// the account is rekeyed. Also called the signor/sgnr.
        /// </summary>
        [AlgoApiField("authAddr")]
        public Address AuthAddr;

        /// <summary>
        /// Optional multisig metadata used to sign the transaction.
        /// </summary>
        [AlgoApiField("msig")]
        public MultisigMetadata Msig;

        /// <summary>
        /// Optional list of addresses that must sign the transactions.
        /// </summary>
        [AlgoApiField("signers")]
        public Address[] Signers;

        /// <summary>
        /// Serialize a transaction and prepare it for WalletConnect.
        /// </summary>
        /// <param name="txn">The transaction to prepare.</param>
        /// <param name="message">Optional message explaining the reason of the transaction.</param>
        /// <param name="authAddr">Optional <see cref="Address"/> used to sign the transaction when the account is rekeyed. Also called the signor/sgnr.</param>
        /// <param name="msig">Optional multisig metadata used to sign the transaction.</param>
        /// <param name="signers">Optional list of addresses that must sign the transactions.</param>
        /// <typeparam name="T">The transaction type.</typeparam>
        /// <returns>A transaction struct usable with WalletConnect. See <see cref="AlgorandWalletConnectSession.SignTransactions"/></returns>
        public static WalletTransaction New<T>(
            T txn,
            string message = default,
            Address authAddr = default,
            MultisigMetadata msig = default,
            Address[] signers = default
            )
            where T : ITransaction
        {
            return new WalletTransaction
            {
                Txn = AlgoApiSerializer.SerializeMessagePack(txn),
                Message = message,
                AuthAddr = authAddr,
                Msig = msig,
                Signers = signers
            };
        }

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
