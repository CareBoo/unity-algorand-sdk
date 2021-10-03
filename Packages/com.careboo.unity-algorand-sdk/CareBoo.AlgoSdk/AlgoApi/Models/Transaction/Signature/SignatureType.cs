using AlgoSdk.Formatters;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(EnumFormatter<SignatureType>))]
    public enum SignatureType : byte
    {
        Sig,
        MultiSig,
        LogicSig
    }
}
