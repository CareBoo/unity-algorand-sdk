using AlgoSdk.MsgPack.Resolvers;
using MessagePack;
using UnityEngine;

namespace AlgoSdk.MsgPack
{
    public static class Config
    {
        static MessagePackSerializerOptions options = null;

        public static MessagePackSerializerOptions Options => options;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            if (options != null)
                return;

            options = MessagePackSerializerOptions.Standard.WithResolver(GeneratedResolver.Instance);
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
