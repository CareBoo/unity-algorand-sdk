using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algorand.Crypto;
using System.Text;
using NUnit.Framework;

public class Sha512Test : MonoBehaviour
{
    [Test]
    public void Sha512IsTruncatedTo32Bytes()
    {
        var bytes = new byte[] { 0x30, 0x30, 0x30, 0x30 };
        bytes = Algorand.Crypto.SHA512.Hash(bytes);
        Assert.AreEqual(32, bytes.Length);
    }
}
