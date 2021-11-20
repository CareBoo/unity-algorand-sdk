using AlgoSdk.Json;
using AlgoSdk.MessagePack;

namespace AlgoSdk
{
    public class SignTxnParamsFormatter : IAlgoApiFormatter<SignTxnParams>
    {
        public SignTxnParams Deserialize(ref JsonReader reader)
        {
            throw new System.NotSupportedException();
        }

        public SignTxnParams Deserialize(ref MessagePackReader reader)
        {
            throw new System.NotSupportedException();
        }

        public void Serialize(ref JsonWriter writer, SignTxnParams value)
        {
            writer.BeginArray();

            AlgoApiFormatterCache<WalletTransaction[]>.Formatter.Serialize(ref writer, value.Transactions);

            if (!value.Options.Equals(default))
            {
                writer.BeginNextItem();
                AlgoApiFormatterCache<SignTxnOpts>.Formatter.Serialize(ref writer, value.Options);
            }

            writer.EndArray();
        }

        public void Serialize(ref MessagePackWriter writer, SignTxnParams value)
        {
            throw new System.NotSupportedException();
        }
    }
}
