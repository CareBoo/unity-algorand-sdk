using System;
using System.IO;

namespace AlgoSdk.MessagePack
{
    public ref partial struct MessagePackReader
    {
        NotSupportedException InvalidCode(int code) => new NotSupportedException($"invalid code: {code}");

        EndOfStreamException InsufficientBuffer() => new EndOfStreamException("insufficient buffer...");
    }
}
