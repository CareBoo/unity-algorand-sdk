using Algorand.Unity;
using Algorand.Unity.Crypto;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using Random = Algorand.Unity.Crypto.Random;

public class MnemonicTest
{
    private string RandomMnemonicString()
    {
        var key = Random.Bytes<PrivateKey>();
        var mnemonic = key.ToMnemonic();
        return mnemonic.ToString();
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
    public void ShortWordShouldEqualWord()
    {
        var word = (Mnemonic.Word)System.Enum.Parse(typeof(Mnemonic.ShortWord), "aban");
        Assert.AreEqual(Mnemonic.Word.abandon, word);
    }

    [Test]
    public void MnemonicToKeyShouldGenerateValidKey()
    {
        var expected = Random.Bytes<PrivateKey>();
        var mnemonic = expected.ToMnemonic();
        var actual = mnemonic.ToPrivateKey();
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void PrintMaxWordLength()
    {
        var maxWordLength = 0;
        for (var i = 0; i < (int)Mnemonic.Word.LENGTH; i++)
        {
            var wordLen = ((Mnemonic.Word)i).ToString().Length;
            maxWordLength = math.max(maxWordLength, wordLen);
        }
        Debug.Log($"Max mnemonic word length: {maxWordLength}");
    }
}
