using System;
using AlgoSdk.WalletConnect.Formatters;
using UnityEngine;

namespace AlgoSdk.WalletConnect
{
    [Serializable]
    [AlgoApiFormatter(typeof(HexFormatter))]
    public partial struct Hex : IEquatable<Hex>
    {
        [SerializeField]
        byte[] data;

        public byte[] Data => data;

        public override string ToString()
        {
            if (data == null)
                return null;
            return BitConverter.ToString(data)
                .Replace("-", "")
                ;
        }

        public static implicit operator byte[](Hex hex)
        {
            return hex.data;
        }

        public static implicit operator Hex(byte[] data)
        {
            return new Hex { data = data };
        }

        public static Hex FromString(string s)
        {
            var data = new byte[s.Length / 2];
            for (var i = 0; i < s.Length; i += 2)
                data[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);
            return data;
        }

        public bool Equals(Hex other)
        {
            return ArrayComparer.Equals(data, other.data);
        }
    }
}
