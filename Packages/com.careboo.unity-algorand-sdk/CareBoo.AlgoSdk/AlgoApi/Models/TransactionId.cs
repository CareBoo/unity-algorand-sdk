using System;
using AlgoSdk.Formatters;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(TransactionIdFormatter))]
    public struct TransactionId
        : IEquatable<TransactionId>
    {
        public FixedString64Bytes Text;

        public bool Equals(TransactionId other)
        {
            return this.Text.Equals(other.Text);
        }

        public override string ToString()
        {
            return Text.ToString();
        }

        public static implicit operator string(TransactionId txid)
        {
            return txid.ToString();
        }

        public static implicit operator TransactionId(string text)
        {
            return new TransactionId { Text = text };
        }

        public static implicit operator TransactionId(FixedString64Bytes text)
        {
            return new TransactionId { Text = text };
        }

        public static implicit operator FixedString64Bytes(TransactionId txid)
        {
            return txid.Text;
        }
    }
}
