using Algorand.Unity;
using UnityEngine;

[CreateAssetMenu]
public class AssetObject : ScriptableObject
{
    public AssetIndex Index;
    public AlgorandNetwork Network;
    public AssetParams Params;
}
