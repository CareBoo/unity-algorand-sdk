using AlgoSdk;
using NUnit.Framework;

public class AddressTest
{
    [Test]
    public void AddressFromStringShouldBeValid()
    {
        var expected = new Address();
        var addressBytes = System.Convert.FromBase64String("GGLP4dv2Cb8V9MfEW9n2VwjOdUmhSaj8nkLMAVaJ+7o=");
        for (var i = 0; i < addressBytes.Length; i++)
            expected[i] = addressBytes[i];
        expected.GenerateCheckSum();
        var actual = Address.FromString("DBRM7YO36YE36FPUY7CFXWPWK4EM45KJUFE2R7E6ILGACVUJ7O5LSFLAFY");
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void AddressToStringShouldBeValid()
    {
        var expected = "DBRM7YO36YE36FPUY7CFXWPWK4EM45KJUFE2R7E6ILGACVUJ7O5LSFLAFY";
        var actualBytes = System.Convert.FromBase64String("GGLP4dv2Cb8V9MfEW9n2VwjOdUmhSaj8nkLMAVaJ+7o=");
        var actual = new Address();
        for (var i = 0; i < actualBytes.Length; i++)
            actual[i] = actualBytes[i];
        actual.GenerateCheckSum();
        Assert.AreEqual(expected, actual.ToString());
    }
}
