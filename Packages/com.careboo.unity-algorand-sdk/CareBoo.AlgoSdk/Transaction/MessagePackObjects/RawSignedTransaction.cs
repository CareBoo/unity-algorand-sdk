

using System;
using System.Collections.Generic;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct RawSignedTransaction
        : IMessagePackObject
    {
        public Field<RawTransaction> Transaction;
        public Field<Signature> Sig;
        public Field<MultiSig> MultiSig;
        public Field<LogicSig> LogicSig;

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
        private static readonly SortedDictionary<FixedString32, FieldFor<RawSignedTransaction>> rawSignedTransactionFields = new SortedDictionary<FixedString32, FieldFor<RawSignedTransaction>>()
        {
            {"txn", FieldFor<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.Transaction)},
            {"sig", FieldFor<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.Sig)},
            {"msig", FieldFor<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.LogicSig)},
            {"lsig", FieldFor<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.MultiSig)},
        };
    }
}
