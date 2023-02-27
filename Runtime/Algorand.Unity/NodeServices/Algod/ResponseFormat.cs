using Unity.Collections;

namespace Algorand.Unity
{
    public enum ResponseFormat : byte
    {
        None,
        Json,
        MessagePack
    }

    public static class ResponseFormatExtensions
    {
        public const string JsonString = "json";

        public const string MessagePackString = "msgpack";

        public static readonly FixedString32Bytes JsonFixedString = JsonString;

        public static readonly FixedString32Bytes MessagePackFixedString = MessagePackString;

        public static readonly string[] TypeToString = new string[]
        {
            string.Empty,
            JsonString,
            MessagePackString
        };

        public static FixedString32Bytes ToFixedString(this ResponseFormat responseFormat)
        {
            switch (responseFormat)
            {
                case ResponseFormat.Json:
                    return JsonFixedString;
                case ResponseFormat.MessagePack:
                    return MessagePackFixedString;
            }
            return default;
        }

        public static Optional<FixedString32Bytes> ToOptionalFixedString(this ResponseFormat responseFormat)
        {
            if (responseFormat == ResponseFormat.None)
                return default;
            return responseFormat.ToFixedString();
        }
    }
}
