using System;
using System.Runtime.InteropServices;
using Algorand.Unity.Json;
using Algorand.Unity.LowLevel;
using Algorand.Unity.MessagePack;
using Unity.Collections;

namespace Algorand.Unity.WalletConnect.Core
{
    /// <summary>
    /// 32 byte random id for a walletconnect topic.
    /// </summary>
    [AlgoApiFormatter(typeof(TopicFormatter))]
    [StructLayout(LayoutKind.Sequential, Size = 32)]
    public unsafe partial struct Topic
        : IEquatable<Topic>
        , IByteArray
    {
        public fixed byte bytes[32];

        public int Length => 32;

        public byte this[int index] { get => this.GetByteAt(index); set => this.SetByteAt(index, value); }

        public override string ToString()
        {
            Span<char> output = stackalloc char[64];
            fixed (byte* ptr = bytes)
            {
                var byteSpan = new ReadOnlySpan<byte>(ptr, 32);
                HexConverter.ToHex(byteSpan, output, out _);
            }
            return output.ToString();
        }

        public FixedString128Bytes ToFixedString()
        {
            var output = default(FixedString128Bytes);

            fixed (byte* ptr = bytes)
            {
                var byteSpan = new ReadOnlySpan<byte>(ptr, 32);
                var error = HexConverter.ToHex(byteSpan, ref output);
                if (error > 0)
                {
                    throw new FormatException(error.ToString());
                }
            }

            return output;
        }

        public static Topic Random()
        {
            return Crypto.Random.Bytes<Topic>();
        }

        public unsafe static Topic FromString<TFixedString>(TFixedString fixedString)
            where TFixedString : struct, INativeList<byte>, IUTF8Bytes
        {
            var result = default(Topic);
            var resultSpan = new Span<byte>(result.bytes, 32);
            var error = HexConverter.FromHex(fixedString, resultSpan, out _);
            if (error > 0)
            {
                throw new FormatException($"Invalid hex string: {fixedString}");
            }
            return result;
        }

        public bool Equals(Topic other)
        {
            return ByteArray.Equals(this, other);
        }

        public void* GetUnsafePtr()
        {
            fixed (byte* ptr = bytes) return ptr;
        }

        public class TopicFormatter : IAlgoApiFormatter<Topic>
        {
            public Topic Deserialize(ref JsonReader reader)
            {
                var s = AlgoApiFormatterCache<FixedString128Bytes>.Formatter.Deserialize(ref reader);
                return FromString(s);
            }

            public Topic Deserialize(ref MessagePackReader reader)
            {
                var s = AlgoApiFormatterCache<FixedString128Bytes>.Formatter.Deserialize(ref reader);
                return FromString(s);
            }

            public void Serialize(ref JsonWriter writer, Topic value)
            {
                var s = value.ToFixedString();
                AlgoApiFormatterCache<FixedString128Bytes>.Formatter.Serialize(ref writer, s);
            }

            public void Serialize(ref MessagePackWriter writer, Topic value)
            {
                var s = value.ToFixedString();
                AlgoApiFormatterCache<FixedString128Bytes>.Formatter.Serialize(ref writer, s);
            }
        }
    }
}
