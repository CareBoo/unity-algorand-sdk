using AlgoSdk.Json;
using AlgoSdk.MessagePack;

namespace AlgoSdk
{
    public interface IAlgoApiFormatter<T>
    {
        T Deserialize(ref JsonReader reader);
        T Deserialize(ref MessagePackReader reader);
        void Serialize(ref JsonWriter writer, T value);
        void Serialize(ref MessagePackWriter writer, T value);
    }
}
