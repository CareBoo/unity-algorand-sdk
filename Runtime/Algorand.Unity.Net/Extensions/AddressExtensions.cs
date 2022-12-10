namespace Algorand.Unity.Net
{
    public static class AddressExtensions
    {
        public static Algorand.Address ToDotnet(this Address from)
        {
            return new Algorand.Address(from);
        }

        public static Address ToUnity(this Algorand.Address from)
        {
            return from.EncodeAsString();
        }
    }
}
