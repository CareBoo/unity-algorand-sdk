using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algorand.Crypto;
using System.Text;
using NUnit.Framework;

public class Sha512Test : MonoBehaviour
{
    [Test]
    public void CanCallSha512Hash()
    {
        var bytes = new byte[] { 0x30, 0x30, 0x30, 0x30 };
        Debug.Log(Encoding.UTF8.GetString(Algorand.Crypto.SHA512.Hash(bytes)));
    }
}
