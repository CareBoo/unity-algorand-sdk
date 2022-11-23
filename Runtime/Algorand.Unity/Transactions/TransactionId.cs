using System;
using System.Runtime.InteropServices;
using Algorand.Unity.Crypto;
using Algorand.Unity.Formatters;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// A SHA512 256-truncated hash of a transaction's bytes.
    /// This is usually Base32 encoded with the padding chars trimmed.
    /// </summary>
    [AlgoApiFormatter(typeof(TransactionIdFormatter))]
    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    [Serializable]
    public partial struct TransactionId
        : IByteArray
        , IEquatable<TransactionId>
    {
        public const int SizeBytes = Sha512_256_Hash.SizeBytes;

        [FieldOffset(0), SerializeField] private Sha512_256_Hash hash;

        public byte this[int index] { get => hash[index]; set => hash[index] = value; }

        public int Length => hash.Length;

        public unsafe void* GetUnsafePtr()
        {
            return hash.GetUnsafePtr();
        }

        public FixedString64Bytes ToFixedString()
        {
            FixedString64Bytes result = default;
            Base32Encoding.ToString(this, ref result);
            Base32Encoding.TrimPadding(ref result);
            return result;
        }

        public override string ToString() => ToFixedString().ToString();

        public static TransactionId FromString<T>(T fs)
            where T : unmanaged, INativeList<byte>, IUTF8Bytes
        {
            TransactionId result = default;
            var b64BytesSize = Base64Encoding.BytesRequiredForBase64Encoding(result.Length);
            if (fs.Length == b64BytesSize)
                result.CopyFromBase64(fs);
            else
                Base32Encoding.ToBytes(fs, ref result);
            return result;
        }

        public static TransactionId FromString(string s)
        {
            return FromString(new FixedString64Bytes(s));
        }

        public bool Equals(TransactionId other)
        {
            return hash.Equals(other.hash);
        }

        public static implicit operator string(TransactionId txId)
        {
            return txId.ToString();
        }

        public static implicit operator TransactionId(string s)
        {
            return TransactionId.FromString(s);
        }

        public static implicit operator TransactionId(Sha512_256_Hash hash) => new TransactionId { hash = hash };
        public static implicit operator Sha512_256_Hash(TransactionId txid) => txid.hash;
    }
}
