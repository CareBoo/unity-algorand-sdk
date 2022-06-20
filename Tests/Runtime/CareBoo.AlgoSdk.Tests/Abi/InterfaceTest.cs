using AlgoSdk;
using AlgoSdk.Experimental.Abi;
using NUnit.Framework;

[TestFixture]
public class InterfaceTest
{
    const string ExpectedJson =
@"{
  ""name"": ""Calculator"",
  ""desc"": ""Interface for a basic calculator supporting additions and multiplications"",
  ""methods"": [
    {
      ""name"": ""add"",
      ""desc"": ""Calculate the sum of two 64-bit integers"",
      ""args"": [
        { ""type"": ""uint64"", ""name"": ""a"", ""desc"": ""The first term to add"" },
        { ""type"": ""uint64"", ""name"": ""b"", ""desc"": ""The second term to add"" }
      ],
      ""returns"": { ""type"": ""uint128"", ""desc"": ""The sum of a and b"" }
    },
    {
    ""name"": ""multiply"",
      ""desc"": ""Calculate the product of two 64-bit integers"",
      ""args"": [
        { ""type"": ""uint64"", ""name"": ""a"", ""desc"": ""The first factor to multiply"" },
        { ""type"": ""uint64"", ""name"": ""b"", ""desc"": ""The second factor to multiply"" }
      ],
      ""returns"": { ""type"": ""uint128"", ""desc"": ""The product of a and b"" }
}
  ]
}";

    static readonly Interface ExpectedInterface = new Interface
    {
        Name = "Calculator",
        Description = "Interface for a basic calculator supporting additions and multiplications",
        Methods = new[]
        {
            new Method
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
            },
            new Method
            {
                Name = "multiply",
                Description = "Calculate the product of two 64-bit integers",
                Arguments = new[]
                {
                    new Method.Arg
                    {
                        Type = AbiType.Parse("uint64"),
                        Name = "a",
                        Description = "The first factor to multiply"
                    },
                    new Method.Arg
                    {
                        Type = AbiType.Parse("uint64"),
                        Name = "b",
                        Description = "The second factor to multiply"
                    }
                },
                Returns = new Method.Return
                {
                    Type = AbiType.Parse("uint128"),
                    Description = "The product of a and b"
                }
            }
        }
    };

    [Test]
    public void SerializingExpectedInterfaceShouldProduceExpectedJson()
    {
        var deserialized = AlgoApiSerializer.DeserializeJson<Interface>(ExpectedJson);
        var expected = AlgoApiSerializer.SerializeJson(deserialized);
        var actual = AlgoApiSerializer.SerializeJson(ExpectedInterface);
        Assert.AreEqual(expected, actual);
    }
}
