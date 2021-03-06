using System;
using System.Diagnostics;

namespace AlgoSdk
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public sealed class AlgoApiFieldAttribute : Attribute
    {
        readonly string name;

        public AlgoApiFieldAttribute(string name)
        {
            this.name = name;
        }

        public string Name => name;
    }
}
