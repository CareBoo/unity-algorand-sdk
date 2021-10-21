using System;
using System.Diagnostics;

namespace AlgoSdk
{
    [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    internal sealed class AlgoApiObjectAttribute : Attribute
    {
    }
}
