using AlgoSdk.Formatters;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(SignatureTypeFormatter))]
    public enum SignatureType : byte
    {
        None,
        Sig,
        Multisig,
        LogicSig
    }

    public static class SignatureTypeExtensions
    {
        public static readonly FixedString32Bytes[] TypeToString = new FixedString32Bytes[]
        {
            default,
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
