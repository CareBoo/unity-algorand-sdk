using System;
using AlgoSdk;
using NUnit.Framework;

public class LogicSigTest
{
    static readonly LogicSig testLogicSig = new LogicSig
    {
        Program = Convert.FromBase64String("ASABACI=")
    };

    static readonly Address testLogicSigAddress = "KI4DJG2OOFJGUERJGSWCYGFZWDNEU2KWTU56VRJHITP62PLJ5VYMBFDBFE";

    [Test]
    public void GetAddressShouldReturnValidAddress()
    {
        var expected = testLogicSigAddress;
        var actual = testLogicSig.GetAddress();
        Assert.AreEqual(expected, actual);
    }
}
