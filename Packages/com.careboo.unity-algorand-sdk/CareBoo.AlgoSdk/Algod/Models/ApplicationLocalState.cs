using System;
using AlgoSdk.MsgPack;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace AlgoSdk
{
    public struct ApplicationLocalState
        : IMessagePackObject
        , IEquatable<ApplicationLocalState>
        , INativeDisposable
    {
        public ulong Id;
        public UnsafeList<TealKeyValue> KeyValues;
        public ApplicationStateSchema Schema;

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return KeyValues.Dispose(inputDeps);
        }

        public void Dispose()
        {
            KeyValues.Dispose();
        }

        public bool Equals(ApplicationLocalState other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<ApplicationLocalState>.Map applicationLocalStateFields =
            new Field<ApplicationLocalState>.Map()
                .Assign("id", (ref ApplicationLocalState x) => ref x.Id)
                .Assign("key-value", (ref ApplicationLocalState x) => ref x.KeyValues, default(UnsafeListComparer<TealKeyValue>))
                .Assign("schema", (ref ApplicationLocalState x) => ref x.Schema)
                ;
    }
}
