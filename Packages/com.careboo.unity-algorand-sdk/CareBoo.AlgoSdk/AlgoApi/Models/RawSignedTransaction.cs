

using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct RawSignedTransaction
        : IMessagePackObject
        , IEquatable<RawSignedTransaction>
    {
        [AlgoApiKey("txn")]
        public RawTransaction Transaction;
        [AlgoApiKey("sig")]
        public Signature Sig;
        [AlgoApiKey("msig")]
        public MultiSig MultiSig;
        [AlgoApiKey("lsig")]
        public LogicSig LogicSig;

        public bool Equals(RawSignedTransaction other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        private static readonly Field<RawSignedTransaction>.Map rawSignedTransactionFields =
            new Field<RawSignedTransaction>.Map()
                .Assign("txn", (ref RawSignedTransaction r) => ref r.Transaction)
                .Assign("sig", (ref RawSignedTransaction r) => ref r.Sig)
                .Assign("msig", (ref RawSignedTransaction r) => ref r.LogicSig)
                .Assign("lsig", (ref RawSignedTransaction r) => ref r.MultiSig)
                ;
    }
}
