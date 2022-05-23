using Unity.Collections;

namespace AlgoSdk.Abi
{
    public interface IAbiType
    {
        bool IsStatic { get; }
        string AbiTypeName { get; }
        NativeArray<byte> Encode(Method.Arg definition, Allocator allocator);
        int Length(Method.Arg definition);
    }
}
