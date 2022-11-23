using System;
using System.Diagnostics;

namespace Algorand.Unity
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public sealed class AlgoApiFieldAttribute : Attribute
    {
        private readonly string name;

        public AlgoApiFieldAttribute(string name)
        {
            this.name = name;
        }

        public string Name => name;
    }
}
