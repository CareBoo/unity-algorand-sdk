using System.Collections.Generic;
using System.Text.RegularExpressions;
using AlgoSdk.LowLevel;
using UnityEditor;

namespace CareBoo.AlgoSdk.Editor
{
    public class SerializedBytes
    {
        static readonly Regex byteRegex = new Regex(@"byte\d\d\d\d", RegexOptions.Compiled);
        SerializedObject serializedObject;
        string rootPath;

        public SerializedBytes(SerializedProperty root)
        {
            serializedObject = root.serializedObject;
            rootPath = root.propertyPath;
        }

        public T ToByteArray<T>() where T : struct, IByteArray
        {
            T byteArray = default;
            var bytes = GetBytes();
            for (var i = 0; i < bytes.Count; i++)
                byteArray[i] = bytes[i];
            return byteArray;
        }

        public void SetBytes<T>(T byteArray) where T : IByteArray
        {
            var bytes = new List<byte>();
            for (var i = 0; i < byteArray.Length; i++)
                bytes.Add(byteArray[i]);
            SetBytes(bytes);
        }

        public List<byte> GetBytes()
        {
            var bytes = new List<byte>();
            var root = serializedObject.FindProperty(rootPath);
            foreach (SerializedProperty child in root)
                if (byteRegex.IsMatch(child.name) && child.propertyType == SerializedPropertyType.Integer)
                    bytes.Add((byte)child.intValue);
            return bytes;
        }

        public void SetBytes(List<byte> bytes)
        {
            var i = 0;
            var root = serializedObject.FindProperty(rootPath);
            foreach (SerializedProperty child in root)
                if (byteRegex.IsMatch(child.name) && child.propertyType == SerializedPropertyType.Integer)
                    child.intValue = (int)bytes[i++];
        }
    }
}
