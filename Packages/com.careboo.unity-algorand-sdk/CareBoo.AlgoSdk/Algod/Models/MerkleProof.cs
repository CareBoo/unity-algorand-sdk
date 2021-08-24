using Unity.Collections;
using Unity.Jobs;

namespace AlgoSdk
{
    public struct MerkleProof
        : INativeDisposable
    {
        public JobHandle Dispose(JobHandle inputDeps)
        {
            return inputDeps;
        }

        public void Dispose()
        {
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
    }
}
