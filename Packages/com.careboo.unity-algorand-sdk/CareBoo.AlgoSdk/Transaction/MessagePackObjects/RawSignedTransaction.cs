

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
        private static readonly Field<RawSignedTransaction>.Map rawSignedTransactionFields =
            new Field<RawSignedTransaction>.Map()
                .Assign("txn", (ref RawSignedTransaction r) => ref r.Transaction)
                .Assign("sig", (ref RawSignedTransaction r) => ref r.Sig)
                .Assign("msig", (ref RawSignedTransaction r) => ref r.LogicSig)
                .Assign("lsig", (ref RawSignedTransaction r) => ref r.MultiSig)
                ;
    }
}
