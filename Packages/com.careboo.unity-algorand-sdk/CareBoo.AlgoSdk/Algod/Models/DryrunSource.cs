using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct DryrunSource
        : IMessagePackObject
        , IEquatable<DryrunSource>
    {
        public ulong AppIndex;
        public FixedString32Bytes FieldName;
        public string Source;
        public ulong TransactionIndex;

        public bool Equals(DryrunSource other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<DryrunSource>.Map dryrunSourceFields =
            new Field<DryrunSource>.Map()
                .Assign("app-index", (ref DryrunSource x) => ref x.AppIndex)
                .Assign("field-name", (ref DryrunSource x) => ref x.FieldName)
                .Assign("source", (ref DryrunSource x) => ref x.Source, StringComparer.Instance)
                .Assign("txn-index", (ref DryrunSource x) => ref x.TransactionIndex)
                ;
    }
}