using Unity.Collections;

namespace Algorand.Unity.LowLevel
{
    public static class NativeArrayExtensions
    {
        public static NativeText AsUtf8Text(this NativeArray<byte>.ReadOnly rawBytes, Allocator allocator)
        {
            var text = new NativeText(allocator) { Length = rawBytes.Length };
            for (var i = 0; i < rawBytes.Length; i++)
                text[i] = rawBytes[i];
            return text;
        }

        public static NativeText AsUtf8Text(this NativeArray<byte> rawBytes, Allocator allocator)
        {
            return rawBytes.AsReadOnly().AsUtf8Text(allocator);
        }
    }
}
