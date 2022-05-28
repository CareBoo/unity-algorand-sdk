using AlgoSdk;
using AlgoSdk.Abi;
using NUnit.Framework;

[TestFixture]
public class MethodTest
{
    const string ExpectedJson =
@"{
  ""name"": ""add"",
  ""desc"": ""Calculate the sum of two 64-bit integers"",
  ""args"": [
    { ""type"": ""uint64"", ""name"": ""a"", ""desc"": ""The first term to add"" },
    { ""type"": ""uint64"", ""name"": ""b"", ""desc"": ""The second term to add"" }
  ],
  ""returns"": { ""type"": ""uint128"", ""desc"": ""The sum of a and b"" }
}";

    static readonly Method ExpectedMethod = new Method
    {
        Name = "add",
        Description = "Calculate the sum of two 64-bit integers",
        Arguments = new[]
        {
            new Method.Arg
            {
                Type = AbiType.Parse("uint64"),
                Name = "a",
                Description = "The first term to add"
            },
            new Method.Arg
            {
                Type = AbiType.Parse("uint64"),
                Name = "b",
                Description = "The second term to add"
            }
        },
        Returns = new Method.Return
        {
            Type = AbiType.Parse("uint128"),
            Description = "The sum of a and b"
        }
    };

    [Test]
    public void SerializingExpectedMethodShouldProduceExpectedJson()
    {
        var deserialized = AlgoApiSerializer.DeserializeJson<Method>(ExpectedJson);
        var expected = AlgoApiSerializer.SerializeJson(deserialized);
        var actual = AlgoApiSerializer.SerializeJson(ExpectedMethod);
        Assert.AreEqual(expected, actual);
    }
}
