

using System;
using System.Collections.Generic;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct RawSignedTransaction
        : IMessagePackObject
        , IEquatable<RawSignedTransaction>
    {
        public RawTransaction Transaction;
        public Signature Sig;
        public MultiSig MultiSig;
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
        private static readonly SortedDictionary<FixedString32, Field<RawSignedTransaction>> rawSignedTransactionFields = new SortedDictionary<FixedString32, Field<RawSignedTransaction>>()
        {
            {"txn", Field<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.Transaction)},
            {"sig", Field<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.Sig)},
            {"msig", Field<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.LogicSig)},
            {"lsig", Field<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.MultiSig)},
        };
    }
}
