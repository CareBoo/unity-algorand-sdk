using Unity.Collections;

namespace Algorand.Unity
{
    public enum SignatureType : byte
    {
        None,
        Sig,
        Multisig,
        LogicSig
    }

    public static class SignatureTypeExtensions
    {
        public static readonly string[] TypeToString = new string[]
        {
            string.Empty,
            "sig",
            "msig",
            "lsig"
        };

        public static FixedString32Bytes ToFixedString(this SignatureType sigType)
        {
            return TypeToString[(int)sigType];
        }

        public static Optional<FixedString32Bytes> ToOptionalFixedString(this SignatureType sigType)
        {
            return sigType == SignatureType.None
                ? default(Optional<FixedString32Bytes>)
                : (Optional<FixedString32Bytes>)sigType.ToFixedString()
                ;
        }
    }
}
