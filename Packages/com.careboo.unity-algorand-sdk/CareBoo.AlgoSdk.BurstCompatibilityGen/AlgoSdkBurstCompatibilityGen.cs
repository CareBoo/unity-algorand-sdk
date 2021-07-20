using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections.Tests;
using UnityEditor;
using NUnit.Framework;
using Unity.Collections;

public class AlgoSdkBurstCompatibilityGen : BurstCompatibilityTests
{
    public AlgoSdkBurstCompatibilityGen() : base(
        "CareBoo.AlgoSdk",
        "Packages/com.careboo.unity-algorand-sdk/CareBoo.AlgoSdk.Tests/CompatGenerated.cs",
        "CareBoo.AlgoSdk.Tests"
    )
    { }
}
