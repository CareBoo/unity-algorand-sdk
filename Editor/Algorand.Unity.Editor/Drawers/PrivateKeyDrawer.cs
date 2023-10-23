using UnityEditor;

namespace Algorand.Unity.Editor
{
    [CustomPropertyDrawer(typeof(PrivateKey))]
    public class PrivateKeyDrawer : FixedBytesTextDrawer<PrivateKey>
    {
        protected override PrivateKey GetByteArray(string s)
        {
            return PrivateKey.FromString(s);
        }

        protected override string GetString(PrivateKey bytes)
        {
            return bytes.ToString();
        }
    }
}
