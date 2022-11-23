using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// Represents the action on the value
    /// </summary>
    public enum DeltaAction : byte
    {
        None = 0,
        SetUInt = 1,
        SetBytes = 2,
        Delete = 3
    }
}
