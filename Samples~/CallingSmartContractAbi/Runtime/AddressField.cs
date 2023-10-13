using Algorand.Unity;
using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.CallingSmartContractAbi
{
    public class AddressField : BaseField<Address>
    {
        public Address address
        {
            get => value;
            set => this.value = value;
        }

        public AddressField(string label) : base(label, new TextField())
        {
        }
    }
}
