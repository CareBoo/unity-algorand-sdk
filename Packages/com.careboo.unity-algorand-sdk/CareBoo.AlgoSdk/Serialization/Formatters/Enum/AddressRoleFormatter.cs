using AlgoSdk.Formatters;
using Unity.Collections;

namespace AlgoSdk
{
    public class AddressRoleFormatter : ByteEnumFormatter<AddressRole>
    {
        private static readonly FixedString32Bytes[] typeToString = new FixedString32Bytes[]
        {
            default,
            "sender",
            "receiver",
            "freeze-target"
        };

        public AddressRoleFormatter() : base(typeToString) { }
    }
}
