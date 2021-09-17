using Unity.Collections;

namespace AlgoSdk.Formatters
{
    public sealed class AddressFormatter : ByteArrayFormatter<Address>
    {
        protected override void BytesToString<T>(Address value, ref T fs)
        {
            fs.CopyFrom(value.ToFixedString());
        }

        protected override Address BytesFromString<T>(in T fs)
        {
            return Address.FromString<T>(in fs);
        }
    }
}
