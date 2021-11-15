using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct WalletTransaction
        : IEquatable<WalletTransaction>
    {
        [AlgoApiField("txn", null)]
        public byte[] MessagePackTxn;

        [AlgoApiField("authAddr", null)]
        public Address AuthAddr;

        [AlgoApiField("msig", null)]
        public MultisigMetadata Msig;

        [AlgoApiField("signers", null)]
        public Address[] Signers;

        [AlgoApiField("message", null)]
        public string Message;

        public bool Equals(WalletTransaction other)
        {
            return ArrayComparer.Equals(MessagePackTxn, other.MessagePackTxn)
                && AuthAddr.Equals(other.AuthAddr)
                && Msig.Equals(other.Msig)
                && ArrayComparer.Equals(Signers, other.Signers)
                && StringComparer.Equals(Message, other.Message)
                ;
        }
    }
}
