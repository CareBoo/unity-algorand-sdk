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
        var bytes = new byte[] { 0x30, 0x30, 0x30, 0x30 };
        bytes = AlgoSdk.Crypto.Sha512.Hash(bytes);
        Assert.AreEqual(32, bytes.Length);
    }

    [Test]
    public void GeneratedRandomBytesIsSameLengthAsSize()
    {
        uint size = 32;
        var bytes = Sha512.RandomBytes(size);
        foreach (var b in bytes)
            Assert.AreNotEqual(0, b);
        Assert.AreEqual(size, bytes.Length);
    }
}
