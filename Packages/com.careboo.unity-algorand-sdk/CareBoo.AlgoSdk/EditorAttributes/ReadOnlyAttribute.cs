using System.Diagnostics;
using UnityEngine;

namespace AlgoSdk
{
    [Conditional("UNITY_EDITOR")]
    public class ReadOnlyAttribute : PropertyAttribute
    {
    }
}
