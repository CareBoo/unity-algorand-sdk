using Unity.Collections;

namespace Algorand.Unity
{
    public enum HashType : byte
    {
        None,
        Sumhash,
        Sha512_256
    }

    public static class HashTypeExtensions
    {
        public static readonly string[] TypeToString = new string[]
        {
            string.Empty,
            "sumhash",
            "sha512_256"
        };

        public static FixedString32Bytes ToFixedString(this HashType hashType)
        {
            return TypeToString[(int)hashType];
        }

        public static Optional<FixedString32Bytes> ToOptionalFixedString(this HashType hashType)
        {
            return hashType == HashType.None
                ? default(Optional<FixedString32Bytes>)
                : (Optional<FixedString32Bytes>)hashType.ToFixedString()
                ;
        }
    }
}
