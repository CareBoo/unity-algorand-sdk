using System.Collections.Generic;
using UnityEditor;
using AlgoSdk;
using System.Linq;
using Unity.Collections;

namespace AlgoSdk.Editor
{
    [CustomPropertyDrawer(typeof(Base32Attribute))]
    [CustomPropertyDrawer(typeof(TransactionId))]
    public class Base32Drawer : BytesTextDrawer
    {
        protected override List<byte> GetBytes(string s)
        {
            return Base32Encoding.ToBytes(s).ToList();
        }

        protected override string GetString(List<byte> bytes)
        {
            var t = new NativeText(Base32Encoding.ToString(bytes.ToArray()), Allocator.Temp);
            try
            {
                Base32Encoding.TrimPadding(ref t);
                return t.ToString();
            }
            finally
            {
                t.Dispose();
            }
        }
    }
}
