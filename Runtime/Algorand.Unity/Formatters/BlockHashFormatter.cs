using Algorand.Unity.Crypto;
using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;
using Unity.Collections;

namespace Algorand.Unity.Formatters
{
    public sealed class BlockHashFormatter : IAlgoApiFormatter<BlockHash>
    {
        private static readonly FixedString32Bytes blockTag = "blk-";

        public BlockHash Deserialize(ref JsonReader reader)
        {
            var s = new FixedString64Bytes();
            reader.ReadString(ref s)
                .ThrowIfError(reader);
            BlockHash result = default;
            if (s.IndexOf(blockTag) == 0)
            {
                var tmp = new FixedString64Bytes();
                tmp.Length = s.Length - blockTag.Length;
                for (var i = 0; i < tmp.Length; i++)
                    tmp.ElementAt(i) = s.ElementAt(i + blockTag.Length);
                UnityEngine.Debug.Log($"tmp: {tmp}");
                UnityEngine.Debug.Log($"s: {s}");
                s = tmp;

            }
            if (s.Length == Base64Encoding.BytesRequiredForBase64Encoding(result.Length))
                result.CopyFromBase64(s);
            else
                Base32Encoding.ToBytes(s, ref result);
            return result;
        }

        public BlockHash Deserialize(ref MessagePackReader reader)
        {
            return AlgoApiFormatterCache<Sha512_256_Hash>.Formatter.Deserialize(ref reader);
        }

        public void Serialize(ref JsonWriter writer, BlockHash value)
        {
            var fs = new FixedString64Bytes();
            value.CopyToBase64(ref fs);
            writer.WriteString(fs);
        }

        public void Serialize(ref MessagePackWriter writer, BlockHash value)
        {
            AlgoApiFormatterCache<Sha512_256_Hash>.Formatter.Serialize(ref writer, value);
        }
    }
}
