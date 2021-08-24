using System;
using AlgoSdk.MsgPack;
using Unity.Collections;
using Unity.Jobs;

namespace AlgoSdk
{
    public struct Application
        : IMessagePackObject
        , INativeDisposable
        , IEquatable<Application>
    {
        public ulong Id;
        public ApplicationParams Params;

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return Params.Dispose(inputDeps);
        }

        public void Dispose()
        {
            Params.Dispose();
        }

        public bool Equals(Application other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal readonly static Field<Application>.Map applicationFields =
            new Field<Application>.Map()
                .Assign("id", (ref Application x) => ref x.Id)
                .Assign("params", (ref Application x) => ref x.Params)
                ;
    }
}
