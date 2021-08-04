

using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct RawSignedTransaction
    {
        public RawTransaction Transaction;
        public Signature Sig;
        public MultiSig MultiSig;
        public LogicSig LogicSig;

        private static readonly FixedString32[] OrderedFields = new FixedString32[]
        {

        };

        private delegate bool SerializePredicate(in RawSignedTransaction data);

        private static readonly SerializePredicate[] ShouldSerialize = new SerializePredicate[]
        {
            (in RawSignedTransaction data) => data.Transaction != default,
            (in RawSignedTransaction data) => data.Sig != default,
            (in RawSignedTransaction data) => data.MultiSig != default,
            (in RawSignedTransaction data) => data.LogicSig != default,
    };
    }
}
