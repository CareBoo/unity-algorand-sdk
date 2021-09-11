using System;
using System.Diagnostics;

namespace AlgoSdk
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    sealed class AlgoApiObjectAttribute : Attribute
    {
    }
}
