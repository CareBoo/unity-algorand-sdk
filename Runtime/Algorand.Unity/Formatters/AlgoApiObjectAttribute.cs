using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Algorand.Unity
{
    [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public sealed class AlgoApiObjectAttribute : ProvideSourceInfoAttribute
    {
        /// <summary>
        /// Determines if unknown keys should throw an error.
        /// </summary>
        public bool IsStrict = false;

        public AlgoApiObjectAttribute(
            [CallerMemberName] string member = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0
        ) : base(member, filePath, lineNumber) { }
    }
}
