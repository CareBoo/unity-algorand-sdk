using UnityEditor;

namespace AlgoSdk.Editor
{
    [CustomPropertyDrawer(typeof(Address))]
    public class AddressDrawer : BytesTextDrawer<Address>
    {
        protected override Address GetByteArray(string s)
        {
            return Address.FromString(s);
        }

        protected override string GetString(Address bytes)
        {
            return bytes.ToString();
        }
    }
}
