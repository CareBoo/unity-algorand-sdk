using System;
using System.Globalization;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk
{
    public class DateTimeFormatter : IAlgoApiFormatter<DateTime>
    {
        public const string Format = "yyyy-MM-dd'T'HH:mm:ss.fffzzz";

        public DateTime Deserialize(ref JsonReader reader)
        {
            var s = AlgoApiFormatterCache<FixedString64Bytes>.Formatter.Deserialize(ref reader);
            return DateTime.ParseExact(s.ToString(), Format, DateTimeFormatInfo.InvariantInfo);
        }

        public DateTime Deserialize(ref MessagePackReader reader)
        {
            var s = AlgoApiFormatterCache<FixedString64Bytes>.Formatter.Deserialize(ref reader);
            return DateTime.ParseExact(s.ToString(), Format, DateTimeFormatInfo.InvariantInfo);
        }

        public void Serialize(ref JsonWriter writer, DateTime value)
        {
            var s = value.ToString(Format, DateTimeFormatInfo.InvariantInfo);
            AlgoApiFormatterCache<FixedString64Bytes>.Formatter.Serialize(ref writer, s);
        }

        public void Serialize(ref MessagePackWriter writer, DateTime value)
        {
            var s = value.ToString(Format, DateTimeFormatInfo.InvariantInfo);
            AlgoApiFormatterCache<FixedString64Bytes>.Formatter.Serialize(ref writer, s);
        }
    }
}
