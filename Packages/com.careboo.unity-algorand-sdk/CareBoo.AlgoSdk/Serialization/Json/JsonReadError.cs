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
        Overflow,
    }

    public class JsonReadException : Exception
    {
        public JsonReadException(JsonReadError error, char c, int pos)
            : base($"{error} on char '{c}' at pos: {pos}")
        {
        }
    }

    public static class JsonReadErrorExtensions
    {
        public static void ThrowIfError(this JsonReadError err, char c, int pos)
        {
            switch (err)
            {
                case JsonReadError.None: return;
                default:
                    throw new JsonReadException(err, c, pos);
            }
        }
    }
}
