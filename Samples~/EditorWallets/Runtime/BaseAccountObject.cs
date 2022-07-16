using UnityEngine;

namespace AlgoSdk
{
    public abstract class BaseAccountObject
        : ScriptableObject
        , IAccount
    {
        public abstract Address Address { get; }
    }
}
