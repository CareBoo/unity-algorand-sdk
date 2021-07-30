using System;
using System.Collections.Generic;
using AlgoSdk.MsgPack.Resolvers;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk.MsgPack
{
    public class Resolver : IFormatterResolver
    {
        public static Resolver Instance = new Resolver();

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.Formatter ?? GeneratedResolver.Instance.GetFormatter<T>();
        }

        private static class FormatterCache<T>
        {
            internal static readonly IMessagePackFormatter<T> Formatter;

            static FormatterCache()
            {
                var f = (IMessagePackFormatter<T>)ResolverHelper.GetFormatter(typeof(T));
            }
        }
    }

    internal static class ResolverHelper
    {
        private static readonly Dictionary<Type, int> lookup = new Dictionary<Type, int>()
        {
            {typeof(Address), 0},
            {typeof(NativeReference<>), 1}
        };

        internal static object GetFormatter(Type t)
        {
            UnityEngine.Debug.Log(typeof(NativeReference<>).GetGenericTypeDefinition());
            UnityEngine.Debug.Log(typeof(Address).GetGenericTypeDefinition());
            UnityEngine.Debug.Log(typeof(NativeReference<Address>).GetGenericTypeDefinition());
            if (!lookup.TryGetValue(t.GetGenericTypeDefinition(), out var key))
            {
                return null;
            }
            switch (key)
            {
                case 0: return new AddressFormatter();
                default: return null;
            }
        }
    }
}
