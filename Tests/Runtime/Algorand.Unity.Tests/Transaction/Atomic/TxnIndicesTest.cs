using Algorand.Unity;
using NUnit.Framework;
using static Algorand.Unity.TxnFlags;

[TestFixture]
public class TxnIndicesTest
{
    [Test]
    public void AllButShouldReturnAllTxnIndicesButTheGivenOnes()
    {
        TxnIndices expected = Txn0 | Txn1 | Txn2 | Txn3 | Txn4 | Txn5 | Txn6 | Txn7;
        var actual = TxnIndices.AllBut(8, 9, 10, 11, 12, 13, 14, 15);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void SelectShouldReturnAllTxnIndicesGiven()
    {
        TxnIndices expected = Txn0 | Txn1 | Txn2 | Txn3 | Txn4 | Txn5 | Txn6 | Txn7;
        TxnIndices actual = TxnIndices.Select(0, 1, 2, 3, 4, 5, 6, 7);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CountShouldReturnCountOfFlaggedIndices()
    {
        int expected = 8;
        int actual = TxnIndices.Select(0, 1, 3, 6, 9, 11, 12, 14).Count();
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void EnumeratorShouldEnumerateThroughGivenIndicesInOrder()
    {
        int[] indices = new[] { 1, 3, 5, 7, 9, 11, 13, 15 };
        TxnIndices txnIndices = TxnIndices.Select(1, 3, 5, 7, 9, 11, 13, 15);
        var i = 0;
        var indexEnum = txnIndices.GetEnumerator();
        while (indexEnum.MoveNext())
        {
            var expected = indices[i];
            var actual = indexEnum.Current;
            Assert.AreEqual(expected, actual);
            i++;
        }
        Assert.AreEqual(indices.Length, i);
    }

    [Test]
    public void ContainsIndexShouldReturnIfIndexInIndices()
    {
        var testCases = new[]
        {
            (index: 1, expected: true),
            (index: 2, expected: false)
        };
        var indices = TxnIndices.Select(1, 3, 4, 5, 6, 7, 9);

        foreach (var (index, expected) in testCases)
        {
            var actual = indices.ContainsIndex(index);
            Assert.AreEqual(expected, actual);
        }
    }
}
