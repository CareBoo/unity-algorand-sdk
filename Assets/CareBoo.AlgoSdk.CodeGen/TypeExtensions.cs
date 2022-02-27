using System;
using System.Linq;
using UnityEngine;

namespace AlgoSdk.Editor.CodeGen
{
    public static class TypeExtensions
    {
        public static string FullNameExpression(this Type type)
        {
            {
                try
                {
                    string name;
                    if (type.IsGenericType)
                    {
                        string genericArguments = type.GetGenericArguments()
                                            .Select(FullNameExpression)
                                            .Aggregate((x1, x2) => $"{x1}, {x2}");
                        name = $"{type.FullName.Substring(0, type.FullName.IndexOf("`"))}<{genericArguments}>";
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
                        name = type.FullName;
                    return name.Replace('+', '.');
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Got exception while formatting type {type}: {ex}");
                    return null;
                }
            }
        }

        public static string NameExpression(this Type type)
        {
            return type.FullNameExpression().Split(".").Last();
        }
    }
}
