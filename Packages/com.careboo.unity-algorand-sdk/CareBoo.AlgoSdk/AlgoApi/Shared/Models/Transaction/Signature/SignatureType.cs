using AlgoSdk.Formatters;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(SignatureTypeFormatter))]
    public enum SignatureType : byte
    {
        None,
        Sig,
        MultiSig,
        LogicSig
    }
}
