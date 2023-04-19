using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Algorand.Unity
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    [Conditional("UNITY_EDITOR")]
    public class ProvideSourceInfoAttribute : Attribute
    {
        private readonly string filePath;
        private string assetPath;

        public string Member { get; }
        public int LineNumber { get; }

        public string AssetPath
        {
            get
            {
                if (assetPath == null)
                {
                    assetPath = Path.GetFullPath(filePath)
                        .Replace('\\', '/')
                        .Substring(LengthOfPathToProject);
                }

                return assetPath;
            }
        }

        public string FilePath => filePath;

        private static int LengthOfPathToProject
        {
            get
            {
                var dataPath = UnityEngine.Application.dataPath;
                if (dataPath.EndsWith("Assets"))
                {
                    return dataPath.Length - "Assets".Length;
                }

                return dataPath.Length;
            }
        }

        public ProvideSourceInfoAttribute(
            [CallerMemberName] string member = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Member = member;
            this.filePath = filePath;
            LineNumber = lineNumber;
        }
    }
}