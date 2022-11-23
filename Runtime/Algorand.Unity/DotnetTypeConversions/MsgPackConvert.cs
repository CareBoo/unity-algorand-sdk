using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algorand.Unity
{
    public static class ConversionExtensions
    {
        public static MsgPackConvert<T> Convert<T>(this T from)
        {
            return new MsgPackConvert<T>(from);
        }
    }

    public readonly struct MsgPackConvert<T>
    {
        private readonly T from;

        public MsgPackConvert(T from)
        {
            this.from = from;
        }

        public U ToDotnet<U>()
        {
            var msgPack = AlgoApiSerializer.SerializeMessagePack(from);
            return Algorand.Utils.Encoder.DecodeFromMsgPack<U>(msgPack);
        }

        public U ToUnity<U>()
        {
            var msgPack = Algorand.Utils.Encoder.EncodeToMsgPackNoOrder(from);
            return AlgoApiSerializer.DeserializeMessagePack<U>(msgPack);
        }
    }
}
