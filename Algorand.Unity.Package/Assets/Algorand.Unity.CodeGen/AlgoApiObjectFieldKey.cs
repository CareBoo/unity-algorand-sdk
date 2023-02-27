using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Algorand.Unity.Editor.CodeGen
{
    public struct AlgoApiObjectFieldKey
    {
        public static readonly BindingFlags BindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public AlgoApiFieldAttribute Attribute { get; private set; }
        public MemberInfo MemberInfo { get; private set; }
        public Type MemberType { get; private set; }

        public AlgoApiObjectFieldKey(FieldInfo fieldInfo)
        {
            MemberInfo = fieldInfo;
            MemberType = fieldInfo.FieldType;
            Attribute = fieldInfo.GetCustomAttribute<AlgoApiFieldAttribute>();
        }

        public AlgoApiObjectFieldKey(PropertyInfo propertyInfo)
        {
            MemberInfo = propertyInfo;
            MemberType = propertyInfo.PropertyType;
            Attribute = propertyInfo.GetCustomAttribute<AlgoApiFieldAttribute>();
        }

        public static IEnumerable<AlgoApiObjectFieldKey> GetFields(Type type)
        {
            var fields = type.GetFields(BindingFlags).Select(f => new AlgoApiObjectFieldKey(f));
            var properties = type.GetProperties(BindingFlags).Select(p => new AlgoApiObjectFieldKey(p));
            return fields.Concat(properties).Where(f => f.Attribute != null);
        }
    }
}
