using UnityEngine;

namespace Algorand.Unity
{
    public abstract class BaseAccountObject
        : ScriptableObject
        , IAccount
    {
        public abstract Address Address { get; }
    }
}
