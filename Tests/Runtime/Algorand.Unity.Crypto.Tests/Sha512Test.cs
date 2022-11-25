using Algorand.Unity.Crypto;
using Algorand.Unity.LowLevel;
using NUnit.Framework;
using Org.BouncyCastle.Crypto.Digests;

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

    [Test]
    public void Sha512_256BouncyCastleGeneratesValidHash()
    {
        foreach (var (seed, expected) in ValidHashes)
        {
            var digester = new Sha512tDigest(256);
            digester.BlockUpdate(seed.ToArray(), 0, seed.Length);
            var output = new byte[32];
            digester.DoFinal(output, 0);
            Assert.AreEqual(expected, System.Convert.ToBase64String(output));
        }
    }
}
