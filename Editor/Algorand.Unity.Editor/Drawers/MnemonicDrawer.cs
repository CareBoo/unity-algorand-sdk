using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;

namespace Algorand.Unity.Editor
{
    [CustomPropertyDrawer(typeof(Mnemonic))]
    public class MnemonicDrawer : FixedBytesTextDrawer
    {
        protected unsafe override List<byte> GetBytes(string s)
        {
            var mnemonic = Mnemonic.FromString(s);
            var result = new List<byte>();
            for (var i = 0; i < Mnemonic.SizeBytes; i++)
                result.Add(UnsafeUtility.ReadArrayElement<byte>(mnemonic.GetUnsafePtr(), i));
            return result;
        }

        protected unsafe override string GetString(List<byte> bytes)
        {
            Mnemonic mnemonic = default;
            for (var i = 0; i < Mnemonic.SizeBytes; i++)
                UnsafeUtility.WriteArrayElement<byte>(mnemonic.GetUnsafePtr(), i, bytes[i]);
            return mnemonic.ToString();
        }
    }
}
