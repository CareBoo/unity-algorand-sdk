using System;
using System.Diagnostics;

namespace AlgoSdk
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public sealed class AlgoApiKeyAttribute : Attribute
    {
        readonly string jsonKeyName;
        readonly string msgPackKeyName;

        public AlgoApiKeyAttribute(string jsonKeyName, string msgPackKeyName)
        {
            this.jsonKeyName = jsonKeyName;
            this.msgPackKeyName = msgPackKeyName;
        }

        public string JsonKeyName => jsonKeyName;

        public string MessagePackKeyName => msgPackKeyName;
    }
}
