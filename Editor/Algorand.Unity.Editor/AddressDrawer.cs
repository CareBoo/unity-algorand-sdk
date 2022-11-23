using System;
using UnityEditor;
using UnityEngine;

namespace Algorand.Unity.Editor
{
    [CustomPropertyDrawer(typeof(Address))]
    public class AddressDrawer : FixedBytesTextDrawer<Address>
    {
        public const string EmptyDisplay = "(" + nameof(Address.Empty) + ")";

        protected override Address GetByteArray(string s)
        {
            try
            {
                return string.IsNullOrEmpty(s) || s == EmptyDisplay
                    ? Address.Empty
                    : Address.FromString(s)
                    ;
            }
            catch (Exception ex)
            {
                Debug.LogException(new FormatException("Invalid Address Format", ex));
                return Address.Empty;
            }
        }

        protected override string GetString(Address bytes)
        {
            return bytes == Address.Empty
                ? EmptyDisplay
                : bytes.ToString()
                ;
        }
    }
}
