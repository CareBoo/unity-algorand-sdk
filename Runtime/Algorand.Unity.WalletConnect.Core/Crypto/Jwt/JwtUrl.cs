using System;
using Algorand.Unity.Crypto;
using Algorand.Unity.LowLevel;
using Algorand.Unity.Collections;
using Unity.Collections;
using Random = Algorand.Unity.Crypto.Random;

namespace Algorand.Unity.WalletConnect.Core
{
    public static class JwtUrl
    {
        public static string JsonEncode<T>(T data)
        {
            using var json = AlgoApiSerializer.SerializeJson(data, Allocator.Temp);
            var utf8Bytes = json.AsArray();
            return Base64Url.Encode(utf8Bytes.AsReadOnlySpan());
        }

        public static string SignJwt(string audience, Ed25519.Seed seed)
        {
            var issuedAt = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
            return SignJwt(audience, seed, issuedAt);
        }

        public static string SignJwt(string audience, Ed25519.Seed seed, long issuedAt)
        {
            Span<byte> subjectBytes = stackalloc byte[32];
            Random.Randomize(subjectBytes);

            Span<char> subjectHex = stackalloc char[64];
            HexConverter.ToHex(subjectBytes, subjectHex, out _);
            return SignJwt(audience, seed, issuedAt, subjectHex.ToString());
        }

        public static string SignJwt(string audience, Ed25519.Seed seed, long issuedAt, string subject)
        {
            using var keyPair = seed.ToKeyPair();
            const int timeToLive = 24 * 60 * 60;
            var issuer = new JwtIssuer { PublicKey = keyPair.PublicKey };

            var header = new JwtHeader
            {
                SigningAlgorithm = "EdDSA",
                TokenType = "JWT"
            };

            var payload = new JwtPayload
            {
                Issuer = issuer.Encode(),
                Subject = subject,
                Audience = audience,
                IssuedAt = issuedAt,
                Expiration = issuedAt + timeToLive
            };

            return SignJwt(header, payload, keyPair.SecretKey);
        }

        public static string SignJwt(JwtHeader header, JwtPayload payload, Ed25519.SecretKeyHandle signer)
        {
            var encodedHeader = JsonEncode(header);
            var encodedPayload = JsonEncode(payload);

            var jwt = new NativeText(Allocator.Temp);
            using var defer = jwt;

            jwt.Append(encodedHeader);
            jwt.Append('.');
            jwt.Append(encodedPayload);

            var signature = signer.Sign(jwt.AsArray().AsSpan());
            var encodedSignature = Base64Url.Encode(signature.AsReadOnlySpan());

            jwt.Append('.');
            jwt.Append(encodedSignature);

            return jwt.ToString();
        }
    }
}
