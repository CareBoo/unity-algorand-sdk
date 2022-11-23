namespace Algorand.Unity.Json
{
    public readonly struct JsonNumber
    {
        private const ulong MaxUlongBefore10 = (ulong.MaxValue / 10);
        private const long MaxLongBefore10 = (long.MaxValue / 10);

        private readonly bool sign;
        private readonly ulong integer;
        private readonly ushort fractionDigits;
        private readonly ulong fraction;
        private readonly bool exponentSign;
        private readonly ushort exponent;

        public JsonNumber(
            bool sign,
            ulong integer,
            ushort fractionDigits,
            ulong fraction,
            bool exponentSign,
            ushort exponent
        )
        {
            this.sign = sign;
            this.integer = integer;
            this.fractionDigits = fractionDigits;
            this.fraction = fraction;
            this.exponentSign = exponentSign;
            this.exponent = exponent;
        }

        public bool Sign => sign;

        public ulong Integer => integer;

        public ushort FractionDigits => fractionDigits;

        public ulong Fraction => fraction;

        public bool ExponentSign => exponentSign;

        public ushort Exponent => exponent;

        public bool TryCastUnsigned(out ulong value)
        {
            value = default;
            if (exponentSign || exponent < fractionDigits)
                return false;

            var x = integer;
            for (var i = 0; i < exponent; i++)
            {
                if (x > MaxUlongBefore10)
                {
                    return false;
                }
                x *= 10;
            }
            value += x;

            x = fraction;
            for (var i = 0; i < (exponent - fractionDigits); i++)
            {
                if (x > MaxUlongBefore10)
                {
                    return false;
                }
                x *= 10;
            }
            if (ulong.MaxValue - value < x)
            {
                return false;
            }
            value += x;

            return true;
        }

        public bool TryCast(out ulong value)
        {
            value = default;
            if (sign)
                return false;
            return TryCastUnsigned(out value);
        }

        public bool TryCast(out uint value)
        {
            value = default;
            if (!TryCast(out ulong u) || u > uint.MaxValue)
            {
                return false;
            }
            value = (uint)u;
            return true;
        }

        public bool TryCast(out ushort value)
        {
            value = default;
            if (!TryCast(out ulong u) || u > ushort.MaxValue)
            {
                return false;
            }
            value = (ushort)u;
            return true;
        }

        public bool TryCast(out byte value)
        {
            value = default;
            if (!TryCast(out ulong u) || u > byte.MaxValue)
            {
                return false;
            }
            value = (byte)u;
            return true;
        }

        public bool TryCast(out long value)
        {
            value = default;
            if (!TryCastUnsigned(out ulong u))
                return false;
            if (u > long.MaxValue)
                return false;
            value = (long)u * (sign ? -1 : 1);
            return true;
        }

        public bool TryCast(out int value)
        {
            value = default;
            if (!TryCast(out long l))
                return false;
            if (l > int.MaxValue || l < int.MinValue)
                return false;
            value = (int)l;
            return true;
        }

        public bool TryCast(out short value)
        {
            value = default;
            if (!TryCast(out long l))
                return false;
            if (l > short.MaxValue || l < short.MinValue)
                return false;
            value = (short)l;
            return true;
        }

        public bool TryCast(out sbyte value)
        {
            value = default;
            if (!TryCast(out long l))
                return false;
            if (l > sbyte.MaxValue || l < sbyte.MinValue)
                return false;
            value = (sbyte)l;
            return true;
        }
    }
}
