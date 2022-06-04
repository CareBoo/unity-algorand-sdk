using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;

namespace AlgoSdk.Editor
{
    public abstract class SerializedBytes
    {
        protected SerializedObject serializedObject;
        protected string rootPath;

        public SerializedBytes(SerializedProperty root)
        {
            serializedObject = root.serializedObject;
            rootPath = root.propertyPath;
        }

        public SerializedProperty Property => serializedObject.FindProperty(rootPath);

        public abstract IEnumerable<SerializedProperty> ByteProperties { get; }

        public List<byte> GetBytes() => ByteProperties
            .Select(prop => (byte)prop.intValue)
            .ToList()
            ;

        public abstract void SetBytes(List<byte> bytes);
    }

    public class SerializedFixedBytes : SerializedBytes
    {
        static readonly Regex byteRegex = new Regex(@"byte\d\d\d\d", RegexOptions.Compiled);

        public SerializedFixedBytes(SerializedProperty root) : base(root) { }

        public override IEnumerable<SerializedProperty> ByteProperties
        {
            get
            {
                foreach (SerializedProperty child in Property)
                {
                    if (byteRegex.IsMatch(child.name) && child.propertyType == SerializedPropertyType.Integer)
                    {
                        yield return child;
                    }
                }
            }
        }

        public override void SetBytes(List<byte> bytes)
        {
            var props = ByteProperties.ToList();
            for (var i = 0; i < bytes.Count; i++)
                props[i].intValue = bytes[i];
        }
    }

    public class SerializedVariableBytes : SerializedBytes
    {
        readonly string byteArrayName;
        public SerializedVariableBytes(SerializedProperty root, string byteArrayName) : base(root)
        {
            this.byteArrayName = byteArrayName;
        }

        public override IEnumerable<SerializedProperty> ByteProperties
        {
            get
            {
                var arrayProp = Property.FindPropertyRelative(byteArrayName);
                for (var i = 0; i < arrayProp.arraySize; i++)
                    yield return arrayProp.GetArrayElementAtIndex(i);
            }
        }

        public override void SetBytes(List<byte> bytes)
        {
            var arrayProp = Property.FindPropertyRelative(byteArrayName);
            arrayProp.arraySize = bytes.Count;
            for (var i = 0; i < bytes.Count; i++)
                arrayProp.GetArrayElementAtIndex(i).intValue = bytes[i];
        }
    }
}
