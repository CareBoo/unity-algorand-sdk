using System;
using System.IO;

namespace AlgoSdk.MessagePack
{
    public ref partial struct MessagePackReader
    {
        NotSupportedException InvalidCode(int code) => new NotSupportedException($"invalid code: {code} at position {offset}");

        EndOfStreamException InsufficientBuffer() => new EndOfStreamException("insufficient buffer...");
    }
}
