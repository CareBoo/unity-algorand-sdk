using System;
using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity
{
    /// <summary>
    /// Contains functions for serializing and deserializing Algorand.Unity types
    /// </summary>
    public static class AlgoApiSerializer
    {
        /// <summary>
        /// Deserializes raw data based on its <see cref="ContentType"/>
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="bytes">The raw data to deserialize</param>
        /// <param name="contentType">The content type of the raw data (Json | MessagePack)</param>
        /// <returns>The data deserialized as T</returns>
        public static T Deserialize<T>(byte[] bytes, ContentType contentType)
        {
            if (bytes == null)
                return default;
            using var nativeBytes = new NativeArray<byte>(bytes, Allocator.Temp);
            return Deserialize<T>(nativeBytes, contentType);
        }

        /// <summary>
        /// Deserializes raw data based on its <see cref="ContentType"/>
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="bytes">The raw data to deserialize</param>
        /// <param name="contentType">The content type of the raw data (Json | MessagePack)</param>
        /// <returns>The data deserialized as T</returns>
        public static T Deserialize<T>(NativeArray<byte> bytes, ContentType contentType)
        {
            return contentType switch
            {
                ContentType.Json => DeserializeJson<T>(bytes),
                ContentType.MessagePack => DeserializeMessagePack<T>(bytes),
                _ when bytes.Length == 0 => default,
                _ => throw new NotSupportedException($"Cannot decode {bytes.Length} bytes of content type {contentType}")
            };
        }

        /// <summary>
        /// Deserialize JSON encoded as UTF8 bytes
        /// </summary>
        /// <typeparam name="T">The type to deserialize the data into</typeparam>
        /// <param name="bytes">UTF8 Bytes that can be decoded into JSON</param>
        /// <returns>The data deserialized as T</returns>
        public static T DeserializeJson<T>(NativeArray<byte> bytes)
        {
            var text = new NativeText(bytes.Length, Allocator.Temp);
            try
            {
                unsafe
                {
                    UnsafeUtility.MemCpy(text.GetUnsafePtr(), bytes.GetUnsafeReadOnlyPtr(), bytes.Length);
                    text.Length = bytes.Length;
                }
                return DeserializeJson<T>(text);
            }
            finally
            {
                text.Dispose();
            }
        }

        /// <summary>
        /// Deserialize JSON encoded as UTF8 bytes
        /// </summary>
        /// <param name="bytes">UTF8 Bytes that can be decoded into JSON</param>
        /// <typeparam name="T">The type to deserialize the data into</typeparam>
        /// <returns>The data deserialized as T</returns>
        public static T DeserializeJson<T>(byte[] bytes)
        {
            using var nativeByteArray = new NativeArray<byte>(bytes, Allocator.Temp);
            return DeserializeJson<T>(nativeByteArray);
        }

        /// <summary>
        /// Deserialize JSON text into an object
        /// </summary>
        /// <typeparam name="T">The type of the object that will be deserialized into</typeparam>
        /// <param name="text">The JSON text</param>
        /// <returns>The data deserialized as T</returns>
        public static T DeserializeJson<T>(NativeText text)
        {
            var reader = new JsonReader(text);
            return AlgoApiFormatterCache<T>.Formatter.Deserialize(ref reader);
        }

        /// <summary>
        /// Deserialize JSON text into an object
        /// </summary>
        /// <typeparam name="T">The type of the object that will be deserialized into</typeparam>
        /// <param name="text">The JSON text</param>
        /// <returns>The data deserialized as T</returns>
        public static T DeserializeJson<T>(string text)
        {
            using var nativeText = new NativeText(text, Allocator.Temp);
            return DeserializeJson<T>(nativeText);
        }

        /// <summary>
        /// Deserialize messagepack bytes into an object
        /// </summary>
        /// <typeparam name="T">The type of the object that will be deserialized into</typeparam>
        /// <param name="bytes">The messagepack bytes</param>
        /// <returns>The data deserialized as T</returns>
        public static T DeserializeMessagePack<T>(NativeArray<byte> bytes)
        {
            var reader = new MessagePackReader(bytes);
            return AlgoApiFormatterCache<T>.Formatter.Deserialize(ref reader);
        }

        /// <summary>
        /// Deserialize messagepack bytes into an object
        /// </summary>
        /// <typeparam name="T">The type of the object that will be deserialized into</typeparam>
        /// <param name="bytes">The messagepack bytes</param>
        /// <returns>The data deserialized as T</returns>        
        public static T DeserializeMessagePack<T>(byte[] bytes)
        {
            using var nativeBytes = new NativeArray<byte>(bytes, Allocator.Temp);
            return DeserializeMessagePack<T>(nativeBytes);
        }

        /// <summary>
        /// Serialize an object into messagepack bytes
        /// </summary>
        /// <typeparam name="T">The type of the object that is serialized</typeparam>
        /// <param name="obj">The object to serialize</param>
        /// <param name="allocator">The allocator to use for the created list of message pack bytes</param>
        /// <returns>A <see cref="NativeList{byte}"/> allocated using the given allocator</returns>
        public static NativeList<byte> SerializeMessagePack<T>(T obj, Allocator allocator)
        {
            var writer = new MessagePackWriter(allocator);
            var formatter = AlgoApiFormatterCache<T>.Formatter;
            formatter.Serialize(ref writer, obj);
            return writer.Data;
        }

        /// <summary>
        /// Serialize an object into messagepack bytes
        /// </summary>
        /// <typeparam name="T">The type of the object that is serialized</typeparam>
        /// <param name="obj">The object to serialize</param>
        /// <returns>The message pack message as an array of bytes</returns>
        public static byte[] SerializeMessagePack<T>(T obj)
        {
            using var listBytes = SerializeMessagePack<T>(obj, Allocator.Temp);
            return listBytes.AsArray().ToArray();
        }

        /// <summary>
        /// Serialize an object into json text
        /// </summary>
        /// <typeparam name="T">The type of the object that is serialized</typeparam>
        /// <param name="obj">The object to serialize</param>
        /// <param name="allocator">The allocator to use for the created text of json</param>
        /// <returns>A <see cref="NativeText"/> allocated from the given allocator</returns>
        public static NativeText SerializeJson<T>(T obj, Allocator allocator)
        {
            var writer = new JsonWriter(allocator);
            var formatter = AlgoApiFormatterCache<T>.Formatter;
            formatter.Serialize(ref writer, obj);
            return writer.Text;
        }

        /// <summary>
        /// Serialize an object into json text
        /// </summary>
        /// <typeparam name="T">The type of the object that is serialized</typeparam>
        /// <param name="obj">The object to serialize</param>
        /// <returns>json text as a string</returns>
        public static string SerializeJson<T>(T obj)
        {
            using var text = SerializeJson<T>(obj, Allocator.Temp);
            return text.ToString();
        }
    }
}
