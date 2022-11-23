using Algorand.Unity;
using UnityEngine;

[CreateAssetMenu]
public class AlgodClientObject
    : ScriptableObject
{
    public AlgodClient Client;
    public AlgorandNetwork Network;
}
