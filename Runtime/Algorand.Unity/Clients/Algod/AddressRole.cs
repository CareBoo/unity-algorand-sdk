using Unity.Collections;

namespace Algorand.Unity
{
    /// <summary>
    /// Role of an address for a transaction.
    /// </summary>
    public enum AddressRole : byte
    {
        None,
        Sender,
        Receiver,
        FreezeTarget
    }

    public static class AddressRoleExtensions
    {
        public static readonly string[] TypeToString = new string[]
        {
            string.Empty,
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
                : (Optional<FixedString32Bytes>)addressRole.ToFixedString()
                ;
        }
    }
}
