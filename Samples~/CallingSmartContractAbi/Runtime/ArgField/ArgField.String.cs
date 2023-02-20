using System.Linq;
using System.Text;
using Algorand.Unity.Experimental.Abi;
using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.CallingSmartContractAbi
{
    public partial class ArgField
    {
        public sealed class String : ArgField
        {
            private readonly TextField textField;

            public override IAbiValue Value =>
                new Array<UInt8>(Encoding.UTF8.GetBytes(textField.text).Select(b => new UInt8(b)).ToArray());

            public String(string label)
            {
                textField = new TextField(label);
                Add(textField);
            }
        }
    }
}