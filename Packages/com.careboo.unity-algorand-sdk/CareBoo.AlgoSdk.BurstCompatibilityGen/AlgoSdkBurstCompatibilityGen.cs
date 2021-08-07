using Unity.Collections.Tests;

#if UNITY_EDITOR_WIN
public class AlgoSdkBurstCompatibilityGen : BurstCompatibilityTests
{
    public AlgoSdkBurstCompatibilityGen() : base(
        "CareBoo.AlgoSdk",
        "Packages/com.careboo.unity-algorand-sdk/CareBoo.AlgoSdk.Tests/CompatGenerated.cs",
        "CareBoo.AlgoSdk.Tests"
    )
    { }
}
#endif
