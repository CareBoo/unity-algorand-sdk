using System;
using System.IO;

namespace Algorand.Unity.MessagePack
{
    public ref partial struct MessagePackReader
    {
        private NotSupportedException InvalidCode(int code) => new NotSupportedException(
            $"invalid code: {code} at position {offset}\nbase64 msgpack: \"{System.Convert.ToBase64String(data.ToArray())}\"");

        private EndOfStreamException InsufficientBuffer() => new EndOfStreamException("insufficient buffer...");
    }
}
