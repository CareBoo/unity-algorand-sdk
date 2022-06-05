using AlgoSdk.Json;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;

public class JsonReaderTest
{
    const string validJson =
@"{
    ""string"": ""world\"""",
    ""int"": -100,
    ""float"": 1.345,
    ""arr"": [
        true,
        false
    ],
    ""null"": null
}";

    [Test]
    public void ValidJsonShouldBeParsedWithoutErrs()
    {
        using var jsonText = new NativeText(
            validJson,
            Allocator.Persistent);
        var reader = new JsonReader(jsonText);
        Assert.AreEqual(JsonToken.ObjectBegin, reader.Read());
        Assert.AreEqual(JsonToken.String, reader.Peek());
        AssertReadStringEqual(ref reader, "string");
        Assert.AreEqual(JsonToken.String, reader.Peek());
        AssertReadStringEqual(ref reader, "world\"");
        Assert.AreEqual(JsonToken.String, reader.Peek());
        AssertReadStringEqual(ref reader, "int");
        Assert.AreEqual(JsonToken.Number, reader.Peek());
        reader.ReadNumber(out int intVal);
        Assert.AreEqual(-100, intVal);
        Assert.AreEqual(JsonToken.String, reader.Peek());
        AssertReadStringEqual(ref reader, "float");
        Assert.AreEqual(JsonToken.Number, reader.Peek());
        reader.ReadNumber(out float floatVal);
        Assert.AreEqual(1.345f, floatVal);
        Assert.AreEqual(JsonToken.String, reader.Peek());
        AssertReadStringEqual(ref reader, "arr");
        Assert.AreEqual(JsonToken.ArrayBegin, reader.Read());
        Assert.AreEqual(JsonToken.Bool, reader.Peek());
        reader.ReadBool(out bool boolVal);
        Assert.AreEqual(true, boolVal);
        Assert.AreEqual(JsonToken.Bool, reader.Peek());
        reader.ReadBool(out boolVal);
        Assert.AreEqual(boolVal, false);
        Assert.AreEqual(JsonToken.ArrayEnd, reader.Read());
        Assert.AreEqual(JsonToken.String, reader.Peek());
        AssertReadStringEqual(ref reader, "null");
        Assert.AreEqual(JsonToken.Null, reader.Peek());
        reader.ReadNull();
        Assert.AreEqual(JsonToken.ObjectEnd, reader.Read());
        Assert.AreEqual(JsonToken.None, reader.Read());
    }

    [Test]
    public void CheckValue()
    {
        var expected = 3;
        Assert.AreEqual(expected, expected++);
        Debug.Log(expected);
    }

    void AssertReadStringEqual(ref JsonReader reader, FixedString32Bytes expected)
    {
        var actual = new FixedString32Bytes();
        reader.ReadString(ref actual);
        Assert.AreEqual(expected, actual);
    }
}
