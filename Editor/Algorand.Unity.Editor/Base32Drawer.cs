using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEditor;

namespace Algorand.Unity.Editor
{
    [CustomPropertyDrawer(typeof(TransactionId))]
    public class Base32Drawer : FixedBytesTextDrawer
    {
        protected override List<byte> GetBytes(string s)
        {
            return Base32Encoding.ToBytes(s).ToList();
        }

        protected override string GetString(List<byte> bytes)
        {
            var t = new NativeText(Base32Encoding.ToString(bytes.ToArray()), Allocator.Persistent);
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
