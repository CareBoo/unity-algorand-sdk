using System;

namespace AlgoSdk.Json
{
    public enum JsonReadError
    {
        None,
        UnknownError,
        ParseError,
        IncorrectFormat,
        IncorrectType,
    }

    public class JsonReadException : Exception
    {
        public JsonReadException(JsonReadError error) : base(error.ToString())
        {
        }
    }

    public static class JsonReadErrorExtensions
    {
        public static void ThrowIfError(this JsonReadError err)
        {
            switch (err)
            {
                case JsonReadError.None: return;
                default:
                    throw new JsonReadException(err);
            }
        }
    }
}
