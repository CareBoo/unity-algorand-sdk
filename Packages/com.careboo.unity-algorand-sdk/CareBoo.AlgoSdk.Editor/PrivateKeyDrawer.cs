using UnityEditor;

namespace AlgoSdk.Editor
{
    [CustomPropertyDrawer(typeof(PrivateKey))]
    public class PrivateKeyDrawer : BytesTextDrawer<PrivateKey>
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
