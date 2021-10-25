using AlgoSdk;
using NUnit.Framework;
using UnityEngine;

public class AccountTest
{
    [Test]
    public void GenerateAccount()
    {
        var (privateKey, address) = Account.GenerateAccount();
        Debug.Log($"My address: {address}");
        Debug.Log($"My private key: {privateKey}");
        Debug.Log($"My passphrase: {privateKey.ToMnemonic()}");
    }
}
