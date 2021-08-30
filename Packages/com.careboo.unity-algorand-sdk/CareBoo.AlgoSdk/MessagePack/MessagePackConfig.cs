using MessagePack;
using UnityEngine;

namespace AlgoSdk.MsgPack
{
    public static class MessagePackConfig
    {
        static MessagePackSerializerOptions serializerOptionsMessagePack = null;
        static MessagePackSerializerOptions serializerOptionsJson = null;

        public static MessagePackSerializerOptions SerializerOptionsMessagePack => serializerOptionsMessagePack;

        public static MessagePackSerializerOptions SerializerOptionsJson => serializerOptionsJson;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            if (serializerOptionsMessagePack != null)
                return;

            serializerOptionsMessagePack = MessagePackSerializerOptions.Standard.WithResolver(Resolvers.FormatterResolver.Instance);
            serializerOptionsJson = serializerOptionsMessagePack.WithIsJsonTrue();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        static void EditorInitialize()
        {
            Initialize();
        }
#endif // UNITY_EDITOR
    }
}
