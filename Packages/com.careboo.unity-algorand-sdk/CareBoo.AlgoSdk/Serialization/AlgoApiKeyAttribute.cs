using System;
using System.Diagnostics;

namespace AlgoSdk
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public sealed class AlgoApiKeyAttribute : Attribute
    {
        public readonly string keyName;
        public readonly string msgPackKeyName;

        public AlgoApiKeyAttribute(string keyName)
        {
            this.keyName = keyName;
        }

        public AlgoApiKeyAttribute(string jsonKeyName, string msgPackKeyName)
        {
            this.keyName = jsonKeyName;
            this.msgPackKeyName = msgPackKeyName;
        }

        public bool HasMultipleKeys => msgPackKeyName != null;

        public string KeyName => keyName;

        public string JsonKeyName => keyName;

        public string MessagePackKeyName => msgPackKeyName ?? keyName;
    }
}
