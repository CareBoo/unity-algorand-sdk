using System.Text;
using Algorand.Unity.LowLevel;
using NUnit.Framework;
using Unity.Collections;
using static Algorand.Unity.Crypto.Ed25519;

namespace Algorand.Unity.Crypto.Tests
{
    public class Ed25519Test
    {
        private const string Message = "Hello world!";

        public static readonly (string, string)[] PublicKeyCases =
        {
            ("zYAQn56H4S2lM84Xf7FTAqYaWF5fBv07STyIJLGeNBg=", "rmlA38Ox+Sv/2msRguPKyeigspgEXtUvkg5+SBYMRXE="),
            ("8t6Mg4DHpur6Hz5yAdTfMgmIU97PgYO4TIdTQp2Djko=", "chRu5AcnydUjV8EX/4VpdmFsoTs7KTcubbRXlaePIz8="),
            ("RpZOyo0ld7eVcR3//nICnCSOdKFyFOUzw6inmrF2h8c=", "BNpaFXoT5vd4Iac8zhvbC+OwAZbyzzNDBWCgmrnVdsE="),
            ("PjeHUXSgVP3ZYdxcouyQzWkEjW9b+oXHd0F5D6bA55o=", "4jBcNpsDXR0ThAGZVBP0yAJ+e/UyPtWRgheENxd0xFY=")
        };

        public static readonly (string, string)[] SigCases =
        {
            ("zYAQn56H4S2lM84Xf7FTAqYaWF5fBv07STyIJLGeNBg=",
                "QGJp3MZGbLayvxaXKC6PJVIuXZl5Z9p4bgjjZccI6kHT0GuFBZ/tpHiZzJnOOvPi0/9UMsqxVeIdph2UWQngCQ=="),
            ("8t6Mg4DHpur6Hz5yAdTfMgmIU97PgYO4TIdTQp2Djko=",
                "2LNMn5rlBqYa/9mO9bXfR0QBOCRav+K9jCht3GEVKpMxvnzwXhG7XAkTkbpUi1cRHQXdQ5GvQRgirqevWlEPDg=="),
            ("RpZOyo0ld7eVcR3//nICnCSOdKFyFOUzw6inmrF2h8c=",
                "MIW9GbRgtJfB/eC+Xsm5R/74GC4gZTDIRk/HEguABwWg0EU5tKL92LJGHsE+OVoU/kY7GTXbYl8R4ez1lUWQDg=="),
            ("PjeHUXSgVP3ZYdxcouyQzWkEjW9b+oXHd0F5D6bA55o=",
                "hm1npBvti6KcPh2ARows1b8oLBHRIMz6Y+Vl87OYCcr43ZmnlbIEW7jWTh3GV+kb79uV8bHTjKaXFBNPjk8ABA==")
        };

        private NativeByteArray GetMessageBytes(string msg, Allocator allocator)
        {
            var msgArr = Encoding.UTF8.GetBytes(msg);
            return new NativeByteArray(msgArr, allocator);
        }

        [Test]
        public void GenerateKeyPairGeneratesValidPublicKeys(
            [ValueSource(nameof(PublicKeyCases))] (string seedStr, string expected) @params)
        {
            var (seedStr, expected) = @params;
            using var msg = GetMessageBytes(Message, Allocator.Temp);
            var seed = ByteArray.FromBase64<Seed>(seedStr);
            using var kp = seed.ToKeyPair();
            var publicKey = kp.PublicKey;
            var actual = publicKey.ToBase64();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GenerateKeyPairGeneratesPrivateKeyWithProperSignatures(
            [ValueSource(nameof(SigCases))] (string seedStr, string expected) @params)
        {
            var (seedStr, expected) = @params;
            using var msg = GetMessageBytes(Message, Allocator.Temp);
            var seed = ByteArray.FromBase64<Seed>(seedStr);
            using var kp = seed.ToKeyPair();
            Assert.IsTrue(kp.SecretKey.IsCreated);
            var sig = kp.SecretKey.Sign(msg);
            var actual = sig.ToBase64();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SigningMessageThenVerifyingReturnsTrue()
        {
            using var msg = GetMessageBytes($"some_random_message{UnityEngine.Random.Range(0, int.MaxValue)}",
                Allocator.Persistent);
            var seed = Random.Bytes<Seed>();
            using var kp = seed.ToKeyPair();
            var sig = kp.SecretKey.Sign(msg);
            Assert.IsTrue(sig.Verify(msg, kp.PublicKey));
        }
    }
}