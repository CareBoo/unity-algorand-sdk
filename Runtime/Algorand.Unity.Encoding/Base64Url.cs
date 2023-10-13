using System;

namespace Algorand.Unity
{
    public static class Base64Url
    {
        public static string Encode(ReadOnlySpan<byte> data)
        {
            var result = Convert.ToBase64String(data);
            return result.Replace("+", "-").Replace("/", "_").TrimEnd('=');
        }

        public static byte[] Decode(ReadOnlySpan<char> base64Url)
        {
            var base64 = base64Url.ToString().Replace("-", "+").Replace("_", "/");
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}