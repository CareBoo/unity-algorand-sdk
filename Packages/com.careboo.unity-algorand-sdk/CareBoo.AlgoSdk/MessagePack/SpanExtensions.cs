using System;
using System.Buffers;
using Unity.Collections;

namespace AlgoSdk.MsgPack
{
    public static class SpanExtensions
    {
        public static void CopyTo<TString>(this in ReadOnlySpan<byte> readOnlySpan, ref TString s)
            where TString : struct, IUTF8Bytes, INativeList<byte>
        {
            s.Length = readOnlySpan.Length;
            for (var i = 0; i < readOnlySpan.Length; i++)
                s[i] = readOnlySpan[i];
        }

        public static void CopyTo<TString>(this in Span<byte> span, ref TString s)
            where TString : struct, IUTF8Bytes, INativeList<byte>
        {
            s.Length = span.Length;
            for (var i = 0; i < span.Length; i++)
                s[i] = span[i];
        }

        public static void CopyTo<TString>(this in ReadOnlySequence<byte> sequence, ref TString s)
            where TString : struct, IUTF8Bytes, INativeList<byte>
        {
            s.Length = (int)sequence.Length;
            sequence.CopyTo(s.AsSpan());
        }

        public static unsafe Span<byte> AsSpan<TString>(this ref TString s)
            where TString : struct, IUTF8Bytes, INativeList<byte>
        {
            return new Span<byte>(s.GetUnsafePtr(), s.Length);
        }

        public static unsafe ReadOnlySpan<byte> AsReadOnlySpan<TString>(this ref TString s)
            where TString : struct, IUTF8Bytes, INativeList<byte>
        {
            return new ReadOnlySpan<byte>(s.GetUnsafePtr(), s.Length);
        }
    }
}
