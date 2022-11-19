using System;
using Unity.Mathematics;

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
        private const int surroundingSize = 50;

        public JsonReadException(JsonReadError error, char c, int pos)
            : base($"{error} on char '{c}' at pos: {pos}")
        { }

        public JsonReadException(JsonReadError error, JsonReader context)
            : base($"{error} on char '{context.Char}' at position: {context.Position} in JSON text:"
                + $"\nsurrounding text: {GetSurroundingText(context)}"
                + $"\n{context.Text}")
        { }

        private static string GetSurroundingText(JsonReader context)
        {
            var start = math.max(0, context.Position - surroundingSize);
            var length = math.min(surroundingSize * 2, context.Text.Length - start);
            return context.Text.Substring(start, length);
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

        public static void ThrowIfError(this JsonReadError err, JsonReader context)
        {
            switch (err)
            {
                case JsonReadError.None: return;
                default:
                    throw new JsonReadException(err, context);
            }
        }
    }
}
