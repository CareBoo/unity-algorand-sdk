using Algorand.Unity.Experimental.Abi;
using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.CallingSmartContractAbi
{
    public partial class ArgField
    {
        public sealed class UInt64 : ArgField
        {
            private readonly TextField textField;

            public override IAbiValue Value => new Algorand.Unity.Experimental.Abi.UInt64(
                ulong.TryParse(textField.text, out var x)
                    ? x
                    : 0
            );

            public UInt64(string label)
            {
                textField = new TextField(label) { value = "0" };
                textField.isDelayed = true;
                textField.RegisterValueChangedCallback(evt =>
                {
                    if (ulong.TryParse(evt.newValue, out var _))
                    {
                        return;
                    }

                    textField.SetValueWithoutNotify(evt.previousValue);
                });
                Add(textField);
            }
        }
    }
}