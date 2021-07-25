using AlgoSdk.Crypto;
using NUnit.Framework;
using System.Runtime.InteropServices;

public class RandomTest
{
    [StructLayout(LayoutKind.Explicit, Size = 5)]
    internal struct TestStruct
    {
        [FieldOffset(0)] internal byte byte0000;
        [FieldOffset(1)] internal byte byte0001;
        [FieldOffset(2)] internal byte byte0002;
        [FieldOffset(3)] internal byte byte0003;
        [FieldOffset(4)] internal byte byte0004;
    }

    [Test]
    public void CanGenerateRandomBytesForType()
    {
        var test = Random.Bytes<TestStruct>();
        UnityEngine.Debug.Log($"{test.byte0000},{test.byte0001},{test.byte0002},{test.byte0003},{test.byte0004}");
    }
}
