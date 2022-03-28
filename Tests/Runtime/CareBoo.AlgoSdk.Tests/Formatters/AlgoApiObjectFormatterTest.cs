using AlgoSdk;
using NUnit.Framework;


[AlgoApiObject(IsStrict = true)]
public partial struct StrictTestObject
{
    [AlgoApiField("field1")]
    public int field;
}

public class AlgoApiObjectFormatterTest
{
    [Test]
    public void StrictAlgoApiObjectShouldThrowKeyErrorIfMissingFieldDefinition()
    {
        var json = "{\"field1\":2,\"field2\":3}";
        Assert.Throws<SerializationException>(() =>
        {
            AlgoApiSerializer.DeserializeJson<StrictTestObject>(json);
        });
    }
}
