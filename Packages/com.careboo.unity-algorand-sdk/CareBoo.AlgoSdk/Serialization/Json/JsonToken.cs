using Unity.Collections;

namespace AlgoSdk.Json
{
    public enum JsonToken
    {
        None = -1,
        ObjectBegin,
        ObjectEnd,
        ArrayBegin,
        ArrayEnd,
        String,
        Bool,
        Null,
        Number,
    }

    public static class JsonTokenExtensions
    {
        public static JsonToken ToJsonToken(this Unicode.Rune rune)
        {
            var c = rune.ToChar();
            return c switch
            {
                '{' => JsonToken.ObjectBegin,
                '}' => JsonToken.ObjectEnd,
                '[' => JsonToken.ArrayBegin,
                ']' => JsonToken.ArrayEnd,
                '"' => JsonToken.String,
                't' => JsonToken.Bool,
                'f' => JsonToken.Bool,
                'n' => JsonToken.Null,
                '0' => JsonToken.Number,
                '1' => JsonToken.Number,
                '2' => JsonToken.Number,
                '3' => JsonToken.Number,
                '4' => JsonToken.Number,
                '5' => JsonToken.Number,
                '6' => JsonToken.Number,
                '7' => JsonToken.Number,
                '8' => JsonToken.Number,
                '9' => JsonToken.Number,
                '-' => JsonToken.Number,
                _ => JsonToken.None
            };
        }
    }
}
