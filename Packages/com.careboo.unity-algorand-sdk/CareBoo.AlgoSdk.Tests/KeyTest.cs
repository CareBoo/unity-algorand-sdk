using AlgoSdk;
using NUnit.Framework;

public class KeyTest
{
    [Test]
    public void PrivateKeyToMnemonicShouldBeValid()
    {
        var privateKey = Key.FromString("Q8gxsV68hkC8Qt+nebHRqFNLW2xPjtYr+eCMhfzuu8O9R8CHfo8aki6Q6mZSrRnVnsppS7czm0bvODeBxacOaA==");
        var expected = Mnemonic.FromString("anchor shrimp flat shine aspect awake bicycle tent crunch shine hand dentist note bitter kiwi mixture kiss tool idea flee weekend wing build absorb clerk");
        var actual = privateKey.ToMnemonic();
        Assert.AreEqual(expected, actual);
    }
}
