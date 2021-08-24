using Unity.Collections;
using Unity.Jobs;

namespace AlgoSdk
{
    public struct PendingTransaction
        : INativeDisposable
    {
        public JobHandle Dispose(JobHandle inputDeps)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
    }
}
