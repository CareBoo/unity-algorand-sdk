using AlgoSdk;
using NUnit.Framework;

public class MultisigAddressTest
{
    static readonly MultisigSig testMsig = new MultisigSig
    {
        Threshold = 3,
        Version = 1,
        Subsigs = new[]
        {
            new MultisigSig.Subsig { PublicKey = (Address)"SLFVTLXHW3I2HGFRL2XJ5J3YAC3MSPNT6CX5G2KK6IKPW4SNEKXCZ6UXYQ" },
            new MultisigSig.Subsig { PublicKey = (Address)"HN5XPGE25UPX62DC2COJFAFAFRBYDFZG7AJ2KIZXFE3W73VVFEYTY6Q5JQ" },
            new MultisigSig.Subsig { PublicKey = (Address)"PI23UTQHCTMHAMO2AUYDGIPEAHRK25MUVV7PDH3QOHMSKGVPEWSJIIESAQ" },
            new MultisigSig.Subsig { PublicKey = (Address)"VCDUTRSZEO7DWU4Z2OTZHLRZ6PEDKZVISI7EDFCB5EUZR5776Y44JBQSRI" }
        }
    };

    static readonly Address testMsigAddress = "ZYTZSDHYAT5G6SSSGM5ZCRWSARGGYCOREKTR3OVK4RWMBC6EIQCY5OVY7I";

    [Test]
    public void MultisigGetAddressShouldReturnValidAddress()
    {
        var expected = testMsigAddress;
        var actual = testMsig.GetAddress();
        Assert.AreEqual(expected, actual);
    }
}
