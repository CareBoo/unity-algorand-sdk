using AlgoSdk.Experimental.Abi;


public partial class ArgField
{
    public sealed class Account : ArgField
    {
        private readonly AddressField address;

        public override IAbiValue Value => new AbiAddress(address.address);

        public Account(string label)
        {
            address = new AddressField(label);
            contentContainer.Add(address);
        }
    }
}
