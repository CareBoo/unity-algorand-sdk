namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(AddressRoleFormatter))]
    public enum AddressRole : byte
    {
        None,
        Sender,
        Receiver,
        FreezeTarget,
        Count
    }
}
