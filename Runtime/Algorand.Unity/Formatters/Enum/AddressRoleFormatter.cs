namespace Algorand.Unity.Formatters
{
    public class AddressRoleFormatter : KeywordByteEnumFormatter<AddressRole>
    {
        public AddressRoleFormatter() : base(AddressRoleExtensions.TypeToString) { }
    }
}
