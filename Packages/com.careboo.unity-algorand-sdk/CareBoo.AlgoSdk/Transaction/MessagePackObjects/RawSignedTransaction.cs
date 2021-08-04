

using System;
using System.Collections.Generic;
using System.Linq;
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

        private static readonly Dictionary<FixedString32, Field<RawSignedTransaction>> fields;

        private static readonly FixedString32[] orderedFields;

        static RawSignedTransaction()
        {
            fields = new Dictionary<FixedString32, Field<RawSignedTransaction>>()
            {
                {"txn", Field<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.Transaction)},
                {"sig", Field<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.Sig)},
                {"msig", Field<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.LogicSig)},
                {"lsig", Field<RawSignedTransaction>.Assign((ref RawSignedTransaction r) => ref r.MultiSig)},
            };
            orderedFields = fields.Keys.ToArray();
            Array.Sort(orderedFields);
        }

        public Dictionary<FixedString32, Field<RawSignedTransaction>> MessagePackFields => fields;

        public bool Equals(RawSignedTransaction other)
        {
            for (var i = 0; i < orderedFields.Length; i++)
                if (!fields[orderedFields[i]].FieldsEqual(ref this, ref other))
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is RawSignedTransaction raw)
                return Equals(raw);
            return false;
        }
    }
}
