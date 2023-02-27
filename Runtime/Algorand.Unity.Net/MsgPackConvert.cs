using Algorand.Utils;

namespace Algorand.Unity.Net
{
    public static class ConversionExtensions
    {
        public static MsgPackConvert<T> Convert<T>(this T from)
        {
            return new MsgPackConvert<T>(from);
        }
    }

    public readonly struct MsgPackConvert<TIn>
    {
        private readonly TIn from;

        public MsgPackConvert(TIn from)
        {
            this.from = from;
        }

        public TOut ToDotnet<TOut>()
        {
            var msgPack = AlgoApiSerializer.SerializeMessagePack(from);
            return Encoder.DecodeFromMsgPack<TOut>(msgPack);
        }

        public TOut ToUnity<TOut>()
        {
            var msgPack = Encoder.EncodeToMsgPackNoOrder(from);
            return AlgoApiSerializer.DeserializeMessagePack<TOut>(msgPack);
        }
    }
}
