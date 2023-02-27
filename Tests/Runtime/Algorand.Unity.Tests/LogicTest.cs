using System;
using System.Text;
using Algorand.Unity;
using Algorand.Unity.LowLevel;
using NUnit.Framework;

public class LogicTest
{
    public static readonly byte[] SigningMessageCase = Encoding.UTF8.GetBytes("this message will be encoded!");
    public const string PrivateKeyCase = "1aRWa1SKHOE4slQGNjgTUQN00c3fsyE1b9vd60qn0OcBZVNAyr+HPARJy+N0CsDJTyklBEZYhsfSO9lcmzDMlQ==";

    [Test]
    public void TealSignShouldReturnValidSignature()
    {
        var pk = PrivateKey.FromString(PrivateKeyCase);
        var program = TealCodeCases.AtomicSwap.SrcBytes;
        var expectedAddress = "7H6MJMO5YS2LSS456BLJDGMS5YM6F2ELOVHINLX62N42JXLQDU2KJD3ENA";
        var address = Logic.GetAddress(program);
        Assert.AreEqual(expectedAddress, address.ToString());

        using var keypair = pk.ToKeyPair();
        var (sk, _) = keypair;
        var tealSign = Logic.TealSign(sk, SigningMessageCase, address);
        var expectedTealSign = "o2oe2vcWrd7JzR3j7J3mQsBP2fT9DJd+gAM+W0OECJdSn9MkBSTp+EYxo8R41TVMF7pcdbGAIodykQsYQlOWBg==";
        Assert.AreEqual(expectedTealSign, System.Convert.ToBase64String(tealSign.ToArray()));
    }
}
