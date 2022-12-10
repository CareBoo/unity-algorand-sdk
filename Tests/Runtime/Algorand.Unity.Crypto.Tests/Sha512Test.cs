using Algorand.Unity.Crypto;
using Algorand.Unity.LowLevel;
using NUnit.Framework;

public class Sha512Test
{
    private static readonly (Ed25519.Seed, string)[] ValidHashes = new (Ed25519.Seed, string)[]
    {
        (default, "rxPASJkSJKXkxmREa2iKr0j7VFbbNilgGwDsFgx05VQ=")
    };

    [Test]
    public void Sha512IsTruncatedTo32Bytes()
    {
        var seed = Algorand.Unity.Crypto.Random.Bytes<Ed25519.Seed>();
        var hash = Algorand.Unity.Crypto.Sha512.Hash256Truncated(seed);
        Assert.AreEqual(32, hash.Length);
    }

    [Test]
    public void Sha512_256GeneratesValidHash()
    {
        foreach (var (seed, expected) in ValidHashes)
        {
            var hash = Sha512.Hash256Truncated(seed);
            var actual = hash.ToBase64();
            Assert.AreEqual(expected, actual);
        }
    }
}
