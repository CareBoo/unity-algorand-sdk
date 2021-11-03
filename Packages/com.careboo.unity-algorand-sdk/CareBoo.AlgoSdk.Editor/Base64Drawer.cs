using System.Collections.Generic;
using System.Linq;
using AlgoSdk;
using AlgoSdk.Crypto;
using UnityEditor;

namespace CareBoo.AlgoSdk.Editor
{
    [CustomPropertyDrawer(typeof(Base64Attribute))]
    [CustomPropertyDrawer(typeof(GenesisHash))]
    [CustomPropertyDrawer(typeof(Sha512_256_Hash))]
    [CustomPropertyDrawer(typeof(Ed25519.PublicKey))]
    [CustomPropertyDrawer(typeof(Sig))]
    [CustomPropertyDrawer(typeof(TealBytes))]
    [CustomPropertyDrawer(typeof(VrfPubKey))]
    public class Base64Drawer : BytesTextDrawer
    {
        protected override List<byte> GetBytes(string s)
        {
            return System.Convert.FromBase64String(s).ToList();
        }

        protected override string GetString(List<byte> bytes)
        {
            return System.Convert.ToBase64String(bytes.ToArray());
        }
    }
}
