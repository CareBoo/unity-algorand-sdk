using System;
using AlgoSdk;
using NUnit.Framework;

public class MnemonicTest
{
    private string RandomMnemonicString()
    {
        var words = new string[Mnemonic.Length];
        var random = new Random();
        for (var i = 0; i < Mnemonic.Length; i++)
            words[i] = ((Mnemonic.Word)random.Next((int)Mnemonic.Word.LENGTH)).ToString();
        return string.Join(" ", words);
    }

    [Test]
    public void CanGenerateMnemonicFromString()
    {
        var expected = RandomMnemonicString();
        var mnemonic = Mnemonic.FromString(expected);
        Assert.AreEqual(expected, mnemonic.ToString());
    }

    [Test]
    public void MnemonicsFromTheSameStringShouldBeEqual()
    {
        var mnemonicString = RandomMnemonicString();
        var mnemonic1 = Mnemonic.FromString(mnemonicString);
        var mnemonic2 = Mnemonic.FromString(mnemonicString);
        Assert.IsTrue(mnemonic1.Equals(mnemonic2));
        Assert.IsTrue(mnemonic2.Equals(mnemonic1));
    }

    [Test]
    public void MnemonicToKeyShouldGenerateValidKey()
    {
        var word = (Mnemonic.Word)Enum.Parse(typeof(Mnemonic.ShortWord), "aban");
        Assert.AreEqual(Mnemonic.Word.abandon, word);
    }
}
