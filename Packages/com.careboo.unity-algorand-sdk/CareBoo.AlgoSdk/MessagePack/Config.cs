using AlgoSdk.MsgPack.Resolvers;
using MessagePack;
using UnityEngine;

namespace AlgoSdk.MsgPack
{
    public static class AlgoSdkMessagePackConfig
    {
        static MessagePackSerializerOptions serializerOptions = null;

        public static MessagePackSerializerOptions SerializerOptions => serializerOptions;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            if (serializerOptions != null)
                return;

            serializerOptions = MessagePackSerializerOptions.Standard.WithResolver(AlgoSdkMessagePackResolver.Instance);
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
