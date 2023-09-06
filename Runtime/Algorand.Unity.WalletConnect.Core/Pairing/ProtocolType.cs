using Unity.Collections;

namespace Algorand.Unity.WalletConnect.Core
{
    public enum ProtocolType
    {
        Sign,
        Auth,
        Chat,
        Push,
        Count
    }

    public static class ProtocolTypeExtensions
    {
        public static readonly FixedString32Bytes[] ProtocolTypeStrings = new FixedString32Bytes[(int)ProtocolType.Count]
        {
            "sign",
            "auth",
            "chat",
            "push"
        };

        public static FixedString32Bytes ToFixedString(this ProtocolType protocolType)
        {
            return ProtocolTypeStrings[(int)protocolType];
        }
    }
}
