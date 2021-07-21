using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlgoSdk.Crypto;
using System.Text;
using NUnit.Framework;

public class Sha512Test
{
    [Test]
    public void Sha512IsTruncatedTo32Bytes()
    {
        var seed = AlgoSdk.Crypto.Random.RandomBytes<Ed25519.Seed>();
        var hash = AlgoSdk.Crypto.Sha512.Hash256Truncated(seed);
        Assert.AreEqual(32, hash.Length);
    }
}
