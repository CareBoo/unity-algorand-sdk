using System;
using Unity.Collections;

namespace Algorand.Unity.WalletConnect.Core
{
    [Serializable]
    public struct PairingMethods : IEquatable<PairingMethods>
    {
        public string[] signMethods;
        public string[] authMethods;
        public string[] chatMethods;
        public string[] pushMethods;

        public string[] this[int index]
        {
            get => this[(ProtocolType)index];
            set => this[(ProtocolType)index] = value;
        }

        public string[] this[ProtocolType protocolType]
        {
            get => protocolType switch
            {
                ProtocolType.Sign => signMethods,
                ProtocolType.Auth => authMethods,
                ProtocolType.Chat => chatMethods,
                ProtocolType.Push => pushMethods,
                _ => throw new ArgumentOutOfRangeException(nameof(protocolType), protocolType, null)
            };
            set
            {
                switch (protocolType)
                {
                    case ProtocolType.Sign:
                        signMethods = value;
                        break;
                    case ProtocolType.Auth:
                        authMethods = value;
                        break;
                    case ProtocolType.Chat:
                        chatMethods = value;
                        break;
                    case ProtocolType.Push:
                        pushMethods = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(protocolType), protocolType, null);
                }
            }
        }

        public FormatError AppendToString<TFixedString>(ref TFixedString text)
            where TFixedString : struct, IUTF8Bytes, INativeList<byte>
        {
            FormatError error = default;
            var startLength = text.Length;
            for (var i = 0; i < (int)ProtocolType.Count; i++)
            {
                var methods = this[i];
                if (methods == null || methods.Length == 0)
                    continue;
                if (text.Length > startLength)
                    error |= text.Append(',');
                error |= text.Append('[');
                error |= text.Append(methods[0]);
                for (var j = 1; j < methods.Length; j++)
                {
                    error |= text.Append(',');
                    error |= text.Append(methods[j]);
                }
                error |= text.Append(']');
            }

            return error;
        }

        public bool Equals(PairingMethods other)
        {
            return ArrayComparer.Equals(signMethods, other.signMethods) &&
                   ArrayComparer.Equals(authMethods, other.authMethods) &&
                   ArrayComparer.Equals(chatMethods, other.chatMethods) &&
                   ArrayComparer.Equals(pushMethods, other.pushMethods);
        }

        public override string ToString()
        {
            var s = new NativeText(Allocator.Temp);
            using var deferDispose = s;
            AppendToString(ref s);

            return s.ToString();
        }
    }
}
