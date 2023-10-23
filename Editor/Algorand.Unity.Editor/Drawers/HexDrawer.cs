using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Algorand.Unity.Editor
{
    [CustomPropertyDrawer(typeof(Hex))]
    public class HexDrawer : BytesTextDrawer
    {
        protected override List<byte> GetBytes(string s)
        {
            return Hex.FromString(s).Data.ToList();
        }

        protected override SerializedBytes GetSerializedBytes(SerializedProperty property)
        {
            return new SerializedVariableBytes(property, "data");
        }

        protected override string GetString(List<byte> bytes)
        {
            return new Hex(bytes.ToArray()).ToString();
        }
    }
}
