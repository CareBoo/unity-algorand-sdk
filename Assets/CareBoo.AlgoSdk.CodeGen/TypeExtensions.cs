using System;
using System.Linq;

namespace AlgoSdk.Editor.CodeGen
{
    public static class TypeExtensions
    {
        public static string FullNameExpression(this Type type)
        {
            {
                string name;
                if (type.IsGenericType)
                {
                    string genericArguments = type.GetGenericArguments()
                                        .Select(FullNameExpression)
                                        .Aggregate((x1, x2) => $"{x1}, {x2}");
                    name = $"{type.SafeFullName().Substring(0, type.SafeFullName().IndexOf("`"))}<{genericArguments}>";
                }
                else if (type.IsArray)
                {
                    string elementType = FullNameExpression(type.GetElementType());
                    name = $"{elementType}[]";
                }
                else if (type.IsGenericTypeParameter)
                {
                    name = type.Name;
                }
                else
                    name = type.SafeFullName();
                return name.Replace('+', '.');
            }
        }

        public static string NameExpression(this Type type)
        {
            {
                string name;
                if (type.IsGenericType)
                {
                    string genericArguments = type.GetGenericArguments()
                                        .Select(NameExpression)
                                        .Aggregate((x1, x2) => $"{x1}, {x2}");
                    name = $"{type.Name.Substring(0, type.Name.IndexOf("`"))}<{genericArguments}>";
                }
                else if (type.IsArray)
                {
                    string elementType = NameExpression(type.GetElementType());
                    name = $"{elementType}[]";
                }
                else
                    name = type.Name;
                return name.Replace('+', '.');
            }
        }

        public static string SafeFullName(this Type type)
        {
            return type.FullName ?? $"{type.Namespace}.{type.Name}";
        }
    }
}
