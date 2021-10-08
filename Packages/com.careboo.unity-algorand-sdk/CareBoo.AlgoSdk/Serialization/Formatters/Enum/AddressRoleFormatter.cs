using AlgoSdk.Formatters;

namespace AlgoSdk
{
    public class AddressRoleFormatter : KeywordByteEnumFormatter<AddressRole>
    {
        public AddressRoleFormatter() : base(AddressRoleExtensions.TypeToString) { }
    }
}
