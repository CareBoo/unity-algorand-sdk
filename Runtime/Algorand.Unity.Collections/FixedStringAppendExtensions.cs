using System;
using Unity.Collections;

namespace Algorand.Unity.Collections
{
    public static class FixedStringAppendExtensions
    {
        public unsafe static FormatError Append<T>(ref this T fs, ReadOnlySpan<char> s)
                    where T : unmanaged, INativeList<byte>, IUTF8Bytes
        {
            // we don't know how big the expansion from UTF16 to UTF8 will be, so we account for worst case.
            int worstCaseCapacity = s.Length * 4;
            byte* utf8Bytes = stackalloc byte[worstCaseCapacity];
            int utf8Len;

            fixed (char* chars = s)
            {
                var err = UTF8ArrayUnsafeUtility.Copy(utf8Bytes, out utf8Len, worstCaseCapacity, chars, s.Length);
                if (err != CopyError.None)
                {
                    return FormatError.Overflow;
                }
            }

            return fs.Append(utf8Bytes, utf8Len);
        }
    }
}
