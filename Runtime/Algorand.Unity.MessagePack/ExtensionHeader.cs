using System;

namespace Algorand.Unity.MessagePack
{
    public struct ExtensionHeader : IEquatable<ExtensionHeader>
    {
        public readonly sbyte TypeCode;

        public readonly uint Length;

        public ExtensionHeader(sbyte typeCode, uint length)
        {
            this.TypeCode = typeCode;
            this.Length = length;
        }

        public ExtensionHeader(sbyte typeCode, int length)
        {
            this.TypeCode = typeCode;
            this.Length = (uint)length;
        }

        public bool Equals(ExtensionHeader other) => this.TypeCode == other.TypeCode && this.Length == other.Length;
    }
}
