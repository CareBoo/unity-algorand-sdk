using System;
using AlgoSdk.Crypto;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TealCompilationResult
        : IMessagePackObject
        , IEquatable<TealCompilationResult>
    {
        [AlgoApiKey("hash")]
        public Sha512_256_Hash Hash;
        [AlgoApiKey("result")]
        public string BytesBase64;

        public bool Equals(TealCompilationResult other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<TealCompilationResult>.Map tealCompilationResultFields =
            new Field<TealCompilationResult>.Map()
                .Assign("hash", (ref TealCompilationResult x) => ref x.Hash)
                .Assign("result", (ref TealCompilationResult x) => ref x.BytesBase64, StringComparer.Instance)
                ;
    }
}
