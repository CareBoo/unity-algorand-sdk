using System;
using Algorand.Unity.LowLevel;
using Algorand.Unity.Crypto;
using Algorand.Unity.Collections;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.WalletConnect.Core
{
    /// <summary>
    /// Sha 256 hash of the message.
    /// </summary>
    [Serializable]
    public struct MessageId
    {
        public FixedBytes32 bytes;

        public MessageId(string message)
        {
            var utf8 = new NativeText(message, Allocator.Temp);
            var hash = Sha512.Hash256Truncated(utf8.AsArray().AsReadOnly());
            bytes = UnsafeUtility.As<Sha512_256_Hash, FixedBytes32>(ref hash);
        }

        public static MessageId FromMessage(string message)
        {
            return new MessageId(message);
        }
    }
}
