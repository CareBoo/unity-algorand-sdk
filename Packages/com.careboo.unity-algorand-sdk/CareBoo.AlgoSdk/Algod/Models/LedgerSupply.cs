using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public struct LedgerSupply
        : IMessagePackObject
    {
        public ulong Round;
        public ulong OnlineMoney;
        public ulong TotalMoney;
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<LedgerSupply>.Map ledgerSupplyFields =
            new Field<LedgerSupply>.Map()
                .Assign("current_round", (ref LedgerSupply x) => ref x.Round)
                .Assign("online-money", (ref LedgerSupply x) => ref x.OnlineMoney)
                .Assign("total-money", (ref LedgerSupply x) => ref x.TotalMoney)
                ;
    }
}
