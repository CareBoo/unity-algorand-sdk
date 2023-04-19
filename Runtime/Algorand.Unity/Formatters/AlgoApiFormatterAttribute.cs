using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Algorand.Unity
{
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public sealed class AlgoApiFormatterAttribute : ProvideSourceInfoAttribute
    {
        private readonly Type formatterType;

        public AlgoApiFormatterAttribute(
            Type formatterType,
            [CallerMemberName] string member = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0
        ) : base(member, filePath, lineNumber)
        {
            if (!formatterType.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IAlgoApiFormatter<>)))
                UnityEngine.Debug.LogError($"`{nameof(formatterType)}` given for `{typeof(AlgoApiFormatterAttribute)}` at {base.FilePath} line {LineNumber} doesn't implement `{typeof(IAlgoApiFormatter<>)}`");

            this.formatterType = formatterType;
        }

        public Type FormatterType => formatterType;
    }
}
