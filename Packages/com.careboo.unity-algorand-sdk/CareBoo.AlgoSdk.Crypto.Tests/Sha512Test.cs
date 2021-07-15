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
        bytes = AlgoSdk.Crypto.SHA512.Hash(bytes);
        Assert.AreEqual(32, bytes.Length);
    }
}
