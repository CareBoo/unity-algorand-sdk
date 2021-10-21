using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AlgoSdk
{
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class | AttributeTargets.Enum, Inherited = true, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    internal sealed class AlgoApiFormatterAttribute : Attribute
    {
        readonly Type formatterType;

        public AlgoApiFormatterAttribute(
            Type formatterType,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (!formatterType.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IAlgoApiFormatter<>)))
                UnityEngine.Debug.LogError($"`{nameof(formatterType)}` given for `{typeof(AlgoApiFormatterAttribute)}` at {filePath} line {lineNumber} doesn't implement `{typeof(IAlgoApiFormatter<>)}`");

            this.formatterType = formatterType;
        }

        public Type FormatterType => formatterType;
    }
}
