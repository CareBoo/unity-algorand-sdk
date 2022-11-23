using System;
using Algorand.Unity;
using NUnit.Framework;

public class LogicSigTest
{
    private static readonly LogicSig testLogicSig = new LogicSig
    {
        Program = Convert.FromBase64String("ASABACI=")
    };

    private static readonly Address testLogicSigAddress = "KI4DJG2OOFJGUERJGSWCYGFZWDNEU2KWTU56VRJHITP62PLJ5VYMBFDBFE";

    [Test]
    public void GetAddressShouldReturnValidAddress()
    {
        var expected = testLogicSigAddress;
        var actual = testLogicSig.GetAddress();
        Assert.AreEqual(expected, actual);
    }
}
