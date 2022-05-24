using Unity.Collections;

namespace AlgoSdk.Abi
{
    public interface IAbiValue
    {
        NativeArray<byte> Encode(AbiType type, Allocator allocator);
        int Length(AbiType type);
    }
}
