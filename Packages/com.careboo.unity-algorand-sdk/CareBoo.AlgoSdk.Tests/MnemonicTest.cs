using System;
using AlgoSdk;
using NUnit.Framework;

public class MnemonicTest
{
    [Test]
    public void CanGenerateMnemonicFromString()
    {
        var expected = "any spoon travel skin conduct release road desk relief attend noodle error clock limit agree brave bright industry eternal stuff despair virus kitten abandon faith";
        var mnemonic = Mnemonic.FromString(expected);
        Assert.AreEqual(expected, mnemonic.ToString());
    }

    [Test]
    public void MnemonicToKeyShouldGenerateValidKey()
    {
        var word = (Mnemonic.Word)Enum.Parse(typeof(Mnemonic.ShortWord), "aban");
        Assert.AreEqual(Mnemonic.Word.abandon, word);
    }
}
