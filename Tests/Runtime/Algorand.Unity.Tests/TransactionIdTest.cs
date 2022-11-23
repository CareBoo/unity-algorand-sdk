using Algorand.Unity;
using NUnit.Framework;
using Unity.Collections;

[TestFixture]
public class TransactionIdTest
{
    [Test]
    public void TestTransactionIdCanBeDeserializedFromString()
    {
        FixedString64Bytes expected = "QASHMKXOAL2P7IN6K2LZESLW3IFC673GKG2B4FWVUFRHLXNL3JPA";
        var actual = TransactionId.FromString(expected).ToFixedString();
        Assert.AreEqual(expected, actual);
    }
}
