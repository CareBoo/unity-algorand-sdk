using AlgoSdk;
using NUnit.Framework;

public class PrivateKeyTest
{
    [Test]
    public void PrivateKeyToMnemonicShouldBeValid()
    {
        var privateKey = PrivateKey.FromString("Q8gxsV68hkC8Qt+nebHRqFNLW2xPjtYr+eCMhfzuu8O9R8CHfo8aki6Q6mZSrRnVnsppS7czm0bvODeBxacOaA==");
        var expected = Mnemonic.FromString("anchor shrimp flat shine aspect awake bicycle tent crunch shine hand dentist note bitter kiwi mixture kiss tool idea flee weekend wing build absorb clerk");
        var actual = privateKey.ToMnemonic();
        for (var i = 0; i < Mnemonic.Length - 1; i++)
            Assert.AreEqual(expected[i], actual[i], $"Key mnemonic differs at index {i}, was expected {expected[i]}, but got {actual[i]}");
    }

    [Test]
    public void PrivateKeyToMnemonicCheckSumShouldBeValid()
    {
        var privateKey = PrivateKey.FromString("Q8gxsV68hkC8Qt+nebHRqFNLW2xPjtYr+eCMhfzuu8O9R8CHfo8aki6Q6mZSrRnVnsppS7czm0bvODeBxacOaA==");
        var expected = Mnemonic.FromString("anchor shrimp flat shine aspect awake bicycle tent crunch shine hand dentist note bitter kiwi mixture kiss tool idea flee weekend wing build absorb clerk")[Mnemonic.ChecksumIndex];
        var actual = privateKey.ToMnemonic()[Mnemonic.ChecksumIndex];
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void PrivateKeyToStringShouldBeValid()
    {
        var expected = "Q8gxsV68hkC8Qt+nebHRqFNLW2xPjtYr+eCMhfzuu8O9R8CHfo8aki6Q6mZSrRnVnsppS7czm0bvODeBxacOaA==";
        var actualBytes = System.Convert.FromBase64String("Q8gxsV68hkC8Qt+nebHRqFNLW2xPjtYr+eCMhfzuu8O9R8CHfo8aki6Q6mZSrRnVnsppS7czm0bvODeBxacOaA==");
        var actualPrivateKey = new PrivateKey();
        for (var i = 0; i < actualPrivateKey.Length; i++)
            actualPrivateKey[i] = actualBytes[i];
        var actual = actualPrivateKey.ToString();
        Assert.AreEqual(expected, actual);
    }
}
