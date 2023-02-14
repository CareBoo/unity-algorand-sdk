using System;
using Algorand.Unity.Collections;
using Algorand.Unity.Crypto;
using Algorand.Unity.LowLevel;
using NUnit.Framework;
using Unity.Collections;

public class Sha512Test
{
    private static readonly (string, string)[] ValidHashes = new (string, string)[]
    {
        ("hello", "e30d87cfa2a75db545eac4d61baf970366a8357c7f72fa95b52d0accb698f13a"),
        ("world", "b8007fc640bef3e2f10ea7ad9681f6fdbd132887406960f365452ba0a15e65e2"),
        ("something for nothing", "0a9bd59712f41292d3d161cbb4bed989f317559890a1ccf21098df2ab8be399c"),
        ("", "c672b8d1ef56ed28ab87c3622c5114069bdd3ad7b8f9737498d0c01ecef0967a"),
        ("1234567890", "89845297b53545520e05ec446aa8c7dc6a9df1171d54d182aec7ca346e44df0d")
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
            using var text = new NativeText(seed, Allocator.Temp);
            var arr = text.AsArray();
            var bytes = new NativeByteArray(arr);
            var hash = Sha512.Hash256Truncated(bytes);
            var actual = BitConverter.ToString(hash.ToArray()).Replace("-", "").ToLower();
            Assert.AreEqual(expected, actual);
        }
    }
}
