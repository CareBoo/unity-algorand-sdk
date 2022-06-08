using UnityEngine;

namespace AlgoSdk
{
    public abstract class AccountObject
        : ScriptableObject
        , IAccount
    {
        public abstract Address Address { get; }
    }
}
