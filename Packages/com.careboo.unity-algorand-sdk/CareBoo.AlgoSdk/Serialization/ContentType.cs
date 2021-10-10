using UnityEngine.Networking;

namespace AlgoSdk
{
    public enum ContentType : byte
    {
        None,
        Json,
        MessagePack,
        PlainText
    }

    public static class ContentTypeExtensions
    {
        public static string ToHeaderValue(this ContentType contentType)
        {
            return contentType switch
            {
                ContentType.Json => "application/json",
                ContentType.MessagePack => "application/msgpack",
                ContentType.PlainText => "text/plain",
                _ => null
            };
        }

        public static ContentType ParseContentType(this UnityWebRequest uwr)
        {
            var headerValue = uwr.GetResponseHeader("Content-Type");
            return PruneParametersFromContentType(headerValue) switch
            {
                "application/json" => ContentType.Json,
                "application/msgpack" => ContentType.MessagePack,
                "text/plain" => ContentType.PlainText,
                _ => ContentType.None
            };
        }


        private static string PruneParametersFromContentType(string fullType)
        {
            if (fullType == null) return fullType;
            for (var i = 0; i < fullType.Length; i++)
                if (fullType[i] == ';')
                    return fullType.Substring(0, i);
            return fullType;
        }
    }
}
