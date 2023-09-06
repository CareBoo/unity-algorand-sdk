using System;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.WalletConnect.Core
{
    /// <summary>
    /// <br/>
    /// https://github.com/ethereum/EIPs/blob/master/EIPS/eip-1328.md
    /// <br/>
    /// https://specs.walletconnect.com/2.0/specs/clients/core/pairing/pairing-uri
    /// <br/>
    /// The WalletConnect pairing URL. This was a handshake url in v1.
    /// </summary>
    [Serializable]
    public struct PairingUri
    {
        public Topic topic;
        public byte version;
        public PairingMethods methods;
        public SymKey symKey;
        public ProtocolOptions relay;

        public PairingUri(PairingMethods methods)
        {
            version = 2;
            this.methods = methods;
            symKey = SymKey.Random();
            var sha256Hash = Crypto.Sha256.Hash(symKey.AsReadOnlySpan());
            topic = UnsafeUtility.As<Crypto.Sha256, Topic>(ref sha256Hash);
            relay = new ProtocolOptions { Protocol = "irn" };
        }

        public override string ToString()
        {
            var s = new NativeText(Allocator.Temp);
            using var deferredDispose = s;
            s.Append("wc:");
            s.Append(topic.ToFixedString());
            s.Append('@');
            s.Append(version);
            s.Append('?');
            s.Append("symKey=");
            s.Append(symKey.ToFixedString());
            if (!methods.Equals(default))
            {
                s.Append('&');
                s.Append("methods=");
                methods.AppendToString(ref s);
            }
            s.Append('&');
            s.Append("relay-protocol=");
            s.Append(relay.Protocol);
            if (!string.IsNullOrEmpty(relay.Data))
            {
                s.Append('&');
                s.Append("relay-data=");
                s.Append(relay.Data);
            }
            return s.ToString();
        }
    }
}
