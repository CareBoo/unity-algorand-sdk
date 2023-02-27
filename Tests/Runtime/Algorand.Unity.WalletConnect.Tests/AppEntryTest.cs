using System;
using Algorand.Unity;
using Algorand.Unity.WalletConnect;
using NUnit.Framework;

[TestFixture]
public class AppEntryTest
{
    [Test]
    public void PeraWalletAppUrlShouldBeCorrect()
    {
        var expected = "algorand-wc://wc?uri=wc%3A4015f93f-b88d-48fc-8bfe-8b063cc325b6%401%3Fbridge%3Dhttps%253A%252F%252F9.bridge.walletconnect.org%26key%3Db0576e0880e17f8400bfff92d4caaf2158cccc0f493dcf455ba76d448c9b5655%26algorand%3Dtrue";
        var handshake = new HandshakeUrl(
            "4015f93f-b88d-48fc-8bfe-8b063cc325b6",
            "1",
            "https://9.bridge.walletconnect.org",
            Hex.FromString("b0576e0880e17f8400bfff92d4caaf2158cccc0f493dcf455ba76d448c9b5655")
            );
        var app = WalletRegistry.PeraWallet;
        var actual = app.FormatUrlForDeepLinkIos(handshake.Url);
        StringAssert.AreEqualIgnoringCase(expected, actual);
    }
}
