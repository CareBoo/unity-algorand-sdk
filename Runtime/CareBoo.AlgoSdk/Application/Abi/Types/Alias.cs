namespace AlgoSdk
{
    public static class Alias
    {
        public static string UnderlyingType(string abiType)
        {
            return abiType switch
            {
                "application" => "uint8",
                "asset" => "uint8",
                "account" => "uint8",
                "byte" => "uint8",
                _ => abiType
            };
        }
    }
}
