using Unity.Collections;

namespace AlgoSdk
{
    public struct TransactionId
    {
        FixedString128Bytes content;

        public static implicit operator string(TransactionId txid)
        {
            return txid.ToString();
        }

        public static implicit operator TransactionId(string content)
        {
            return new TransactionId { content = content };
        }

        public static implicit operator TransactionId(FixedString128Bytes content)
        {
            return new TransactionId { content = content };
        }

        public static implicit operator FixedString128Bytes(TransactionId txid)
        {
            return txid.content;
        }
    }
}
