using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.CallingSmartContractAbi
{
    public class PaymentTxnField : VisualElement
    {
        private readonly Label label;
        private readonly AddressField receiver;
        private readonly Slider amount;

        public Address Receiver => receiver.address;

        public MicroAlgos Amount => (ulong)(amount.value * MicroAlgos.PerAlgo);

        public PaymentTxnField(string labelText)
        {
            label = new Label(labelText);
            receiver = new AddressField("receiver");
            amount = new Slider(0.01f, 1f);

            Add(label);
            Add(receiver);
            Add(amount);
        }
    }
}