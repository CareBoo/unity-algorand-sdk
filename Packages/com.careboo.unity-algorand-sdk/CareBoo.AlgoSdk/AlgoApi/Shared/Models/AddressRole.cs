using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(AddressRoleFormatter))]
    public enum AddressRole : byte
    {
        None,
        Sender,
        Receiver,
        FreezeTarget
    }

    public static class AddressRoleExtensions
    {
        public static readonly FixedString32Bytes[] TypeToString = new FixedString32Bytes[]
        {
            default,
            "sender",
            "receiver",
            "freeze-target"
        };

        public static FixedString32Bytes ToFixedString(this AddressRole addrRole)
        {
            return TypeToString[(int)addrRole];
        }

        public static Optional<FixedString32Bytes> ToOptionalFixedString(this AddressRole addressRole)
        {
            return addressRole == AddressRole.None
                ? default(Optional<FixedString32Bytes>)
                : addressRole.ToFixedString()
                ;
        }
    }
}
