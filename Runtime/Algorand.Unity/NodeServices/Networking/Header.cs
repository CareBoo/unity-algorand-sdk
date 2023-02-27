using System;

namespace Algorand.Unity
{
    /// <summary>
    /// A serializable key-value pair that's used in http request headers.
    /// </summary>
    [Serializable]
    public struct Header
    {
        public string Key;

        public string Value;

        public Header(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public void Deconstruct(out string key, out string value)
        {
            key = Key;
            value = Value;
        }

        public override string ToString()
        {
            return $"{Key}:{Value}";
        }

        public static implicit operator Header((string, string) tuple)
        {
            var (key, value) = tuple;
            return new Header(key, value);
        }

        public static implicit operator (string, string)(Header header)
        {
            return (header.Key, header.Value);
        }
    }
}
