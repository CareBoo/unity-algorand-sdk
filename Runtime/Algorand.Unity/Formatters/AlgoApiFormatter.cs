using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;

namespace Algorand.Unity
{
    public interface IAlgoApiFormatter<T>
    {
        T Deserialize(ref JsonReader reader);
        T Deserialize(ref MessagePackReader reader);
        void Serialize(ref JsonWriter writer, T value);
        void Serialize(ref MessagePackWriter writer, T value);
    }
}
