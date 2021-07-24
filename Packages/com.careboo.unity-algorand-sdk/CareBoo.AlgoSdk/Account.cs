namespace AlgoSdk
{
    public readonly struct Account
    {
        readonly public PrivateKey PrivateKey;
        readonly public Address Address;

        public Account(in PrivateKey privateKey, in Address address)
        {
            PrivateKey = privateKey;
            Address = address;
        }

        public Account(in PrivateKey privateKey) : this(in privateKey, privateKey.ToAddress()) { }

        public static Account Generate()
        {
            var privateKey = Crypto.Random.Bytes<PrivateKey>();
            return new Account(in privateKey);
        }
    }
}
