using System;
using AlgoSdk.MsgPack;


namespace AlgoSdk
{
    public struct Block
        : IMessagePackObject
        , IEquatable<Block>
    {
        public Header HeaderData;

        public bool Equals(Block other)
        {
            return this.Equals(ref other);
        }

        public struct Header
            : IEquatable<Header>
        {
            public bool Equals(Header other)
            {
                throw new NotImplementedException();
            }
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<Block>.Map blockFields =
            new Field<Block>.Map()
                .Assign("block", (ref Block x) => ref x.HeaderData)
                ;
    }
}
