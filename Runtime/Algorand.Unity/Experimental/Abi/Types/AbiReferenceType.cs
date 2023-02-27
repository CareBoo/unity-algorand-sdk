namespace Algorand.Unity.Experimental.Abi
{
    public enum AbiReferenceType : byte
    {
        None,
        Asset,
        Application,
        Account
    }

    public static class AbiReferenceTypeExtensions
    {
        public static AbiReferenceType Parse(string type) =>
            type switch
            {
                "asset" => AbiReferenceType.Asset,
                "application" => AbiReferenceType.Application,
                "account" => AbiReferenceType.Account,
                _ => AbiReferenceType.None
            };
    }
}
