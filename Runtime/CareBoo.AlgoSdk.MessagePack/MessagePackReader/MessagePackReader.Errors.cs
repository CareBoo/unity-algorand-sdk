using System;
using System.IO;

namespace AlgoSdk.MessagePack
{
    public ref partial struct MessagePackReader
    {
        NotSupportedException InvalidCode(int code) => new NotSupportedException(
            $"invalid code: {code} at position {offset}\nbase64 msgpack: \"{System.Convert.ToBase64String(data.ToArray())}\"");

        EndOfStreamException InsufficientBuffer() => new EndOfStreamException("insufficient buffer...");
    }
}
