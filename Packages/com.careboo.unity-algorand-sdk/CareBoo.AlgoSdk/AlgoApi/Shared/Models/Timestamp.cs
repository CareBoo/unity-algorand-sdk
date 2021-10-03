using System;
using System.Globalization;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(TimestampFormatter))]
    public struct Timestamp
        : IEquatable<Timestamp>
        , IEquatable<DateTime>
    {
        public const string Format = "yyyy-MM-dd'T'HH:mm:ss.fffzzz";

        DateTime dateTime;

        public bool Equals(Timestamp other)
        {
            return dateTime.Equals(other.dateTime);
        }

        public bool Equals(DateTime other)
        {
            return dateTime.Equals(other);
        }

        public static implicit operator DateTime(Timestamp ts)
        {
            return ts.dateTime;
        }

        public static implicit operator Timestamp(DateTime dt)
        {
            return new Timestamp { dateTime = dt };
        }

        public override string ToString()
        {
            return dateTime.ToString(Format, DateTimeFormatInfo.InvariantInfo);
        }

        public static Timestamp FromString(string s)
        {
            var dateTime = DateTime.ParseExact(s, Format, DateTimeFormatInfo.InvariantInfo);
            return new Timestamp { dateTime = dateTime };
        }
    }
}
