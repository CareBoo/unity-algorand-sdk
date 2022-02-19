using AlgoSdk.Json;
using AlgoSdk.MessagePack;

namespace AlgoSdk.WalletConnect
{
    public class SignTxnsParamsFormatter : IAlgoApiFormatter<SignTxnsParams>
    {
        public SignTxnsParams Deserialize(ref JsonReader reader)
        {
            throw new System.NotSupportedException();
        }

        public SignTxnsParams Deserialize(ref MessagePackReader reader)
        {
            throw new System.NotSupportedException();
        }

        public void Serialize(ref JsonWriter writer, SignTxnsParams value)
        {
            writer.BeginArray();

            AlgoApiFormatterCache<WalletTransaction[]>.Formatter.Serialize(ref writer, value.Transactions);

            if (!value.Options.Equals(default))
            {
                writer.BeginNextItem();
                AlgoApiFormatterCache<SignTxnsOpts>.Formatter.Serialize(ref writer, value.Options);
            }

            writer.EndArray();
        }

        public void Serialize(ref MessagePackWriter writer, SignTxnsParams value)
        {
            throw new System.NotSupportedException();
        }
    }
}
