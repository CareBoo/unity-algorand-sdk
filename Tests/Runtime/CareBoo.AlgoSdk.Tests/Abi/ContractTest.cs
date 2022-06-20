using AlgoSdk;
using AlgoSdk.Experimental.Abi;
using NUnit.Framework;

[TestFixture]
public class ContractTest
{
    const string ExpectedJson =
@"{
  ""name"": ""Calculator"",
  ""desc"": ""Contract of a basic calculator supporting additions and multiplications. Implements the Calculator interface."",
  ""networks"": {
    ""wGHE2Pwdvd7S12BL5FaOP20EGYesN73ktiC1qzkkit8="": { ""appID"": 1234 },
    ""SGO1GKSzyE7IEPItTxCByw9x8FmnrCDexi9/cOUJOiI="": { ""appID"": 5678 },
  },
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

    static readonly Contract ExpectedContract = new Contract
    {
        Name = "Calculator",
        Description = "Contract of a basic calculator supporting additions and multiplications. Implements the Calculator interface.",
        Networks = new[]
        {
            new Contract.Deployment
            {
                Network = "wGHE2Pwdvd7S12BL5FaOP20EGYesN73ktiC1qzkkit8=",
                AppId = 1234
            },
            new Contract.Deployment
            {
                Network = "SGO1GKSzyE7IEPItTxCByw9x8FmnrCDexi9/cOUJOiI=",
                AppId = 5678
            }
        },
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
    public void DeserializingExpectedContractJsonShouldProduceExpectedContract()
    {
        var expected = ExpectedContract;
        var actual = AlgoApiSerializer.DeserializeJson<Contract>(ExpectedJson);
        Assert.True(expected.Equals(actual));
    }

    [Test]
    public void SerializingExpectedContractShouldProduceExpectedJson()
    {
        var deserialized = AlgoApiSerializer.DeserializeJson<Contract>(ExpectedJson);
        var expected = AlgoApiSerializer.SerializeJson(deserialized);
        var actual = AlgoApiSerializer.SerializeJson(ExpectedContract);
        Assert.AreEqual(expected, actual);
    }
}
