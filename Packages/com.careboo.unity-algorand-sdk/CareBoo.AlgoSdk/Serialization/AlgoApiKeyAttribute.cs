using System;
using System.Diagnostics;

namespace AlgoSdk
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public sealed class AlgoApiKeyAttribute : Attribute
    {
        readonly string keyName;

        public AlgoApiKeyAttribute(string keyName)
        {
            this.keyName = keyName;
        }

        public string KeyName => keyName;

        public Type EqualityComparer { get; set; }
    }
}
