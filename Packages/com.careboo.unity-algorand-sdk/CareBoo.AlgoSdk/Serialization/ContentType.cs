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

        public static ContentType ToContentType(this string headerValue)
        {
            return headerValue switch
            {
                "application/json" => ContentType.Json,
                "application/msgpack" => ContentType.MessagePack,
                "text/plain" => ContentType.PlainText,
                _ => ContentType.None
            };
        }
    }
}
