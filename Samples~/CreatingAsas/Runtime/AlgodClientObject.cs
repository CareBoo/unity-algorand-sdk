using UnityEngine;

namespace Algorand.Unity.Samples.CreatingAsas
{
    [CreateAssetMenu]
    public class AlgodClientObject
        : ScriptableObject
    {
        public AlgodClient Client;
        public AlgorandNetwork Network;
    }
}
