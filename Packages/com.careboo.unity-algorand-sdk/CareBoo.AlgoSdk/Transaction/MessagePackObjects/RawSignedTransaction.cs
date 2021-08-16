

using System;
using System.Collections.Generic;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct RawSignedTransaction
        : IMessagePackType<RawSignedTransaction>
    {
        public Prop<RawTransaction> Transaction;
        public Prop<Signature> Sig;
        public Prop<MultiSig> MultiSig;
        public Prop<LogicSig> LogicSig;

        private static readonly SortedDictionary<FixedString32, Field<RawSignedTransaction>> fields = new SortedDictionary<FixedString32, Field<RawSignedTransaction>>()
            {
                {"txn", Field<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.Transaction)},
                {"sig", Field<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.Sig)},
                {"msig", Field<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.LogicSig)},
                {"lsig", Field<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.MultiSig)},
            };

        public SortedDictionary<FixedString32, Field<RawSignedTransaction>> MessagePackFields => fields;

        public bool Equals(RawSignedTransaction other)
        {
            return this.Equals(ref other);
        }

        public override bool Equals(object obj)
        {
            if (obj is RawSignedTransaction raw)
                return Equals(raw);
            return false;
        }
    }
}
