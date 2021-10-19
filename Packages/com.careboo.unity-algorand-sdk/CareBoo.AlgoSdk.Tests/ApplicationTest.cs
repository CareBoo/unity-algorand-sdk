using AlgoSdk;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ApplicationTest
{
    [Test]
    public void ApplicationGetAddressFromIdShouldBeValid()
    {
        Address expected = "WCS6TVPJRBSARHLN2326LRU5BYVJZUKI2VJ53CAWKYYHDE455ZGKANWMGM";
        var actual = AlgoSdk.Application.ComputeAddressFromId(1);
        Assert.AreEqual(expected, actual);
        Debug.Log(actual.ToString());
    }
}
