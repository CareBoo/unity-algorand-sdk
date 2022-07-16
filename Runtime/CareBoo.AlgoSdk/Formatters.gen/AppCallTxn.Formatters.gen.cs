//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlgoSdk
{
    
    
    public partial struct AppCallTxn
    {
        
        private static bool @__generated__IsValid = AppCallTxn.@__generated__InitializeAlgoApiFormatters();
        
        private static bool @__generated__InitializeAlgoApiFormatters()
        {
            AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.AppCallTxn>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.AppCallTxn>(false).Assign("fee", (AlgoSdk.AppCallTxn x) => x.Fee, (ref AlgoSdk.AppCallTxn x, AlgoSdk.MicroAlgos value) => x.Fee = value).Assign("fv", (AlgoSdk.AppCallTxn x) => x.FirstValidRound, (ref AlgoSdk.AppCallTxn x, System.UInt64 value) => x.FirstValidRound = value).Assign("gh", (AlgoSdk.AppCallTxn x) => x.GenesisHash, (ref AlgoSdk.AppCallTxn x, AlgoSdk.GenesisHash value) => x.GenesisHash = value).Assign("lv", (AlgoSdk.AppCallTxn x) => x.LastValidRound, (ref AlgoSdk.AppCallTxn x, System.UInt64 value) => x.LastValidRound = value).Assign("snd", (AlgoSdk.AppCallTxn x) => x.Sender, (ref AlgoSdk.AppCallTxn x, AlgoSdk.Address value) => x.Sender = value).Assign("type", (AlgoSdk.AppCallTxn x) => x.TransactionType, (ref AlgoSdk.AppCallTxn x, AlgoSdk.TransactionType value) => x.TransactionType = value, AlgoSdk.ByteEnumComparer<AlgoSdk.TransactionType>.Instance).Assign("gen", (AlgoSdk.AppCallTxn x) => x.GenesisId, (ref AlgoSdk.AppCallTxn x, Unity.Collections.FixedString32Bytes value) => x.GenesisId = value).Assign("grp", (AlgoSdk.AppCallTxn x) => x.Group, (ref AlgoSdk.AppCallTxn x, AlgoSdk.TransactionId value) => x.Group = value).Assign("lx", (AlgoSdk.AppCallTxn x) => x.Lease, (ref AlgoSdk.AppCallTxn x, AlgoSdk.TransactionId value) => x.Lease = value).Assign("note", (AlgoSdk.AppCallTxn x) => x.Note, (ref AlgoSdk.AppCallTxn x, System.Byte[] value) => x.Note = value, AlgoSdk.ArrayComparer<System.Byte>.Instance).Assign("rekey", (AlgoSdk.AppCallTxn x) => x.RekeyTo, (ref AlgoSdk.AppCallTxn x, AlgoSdk.Address value) => x.RekeyTo = value).Assign("apid", (AlgoSdk.AppCallTxn x) => x.ApplicationId, (ref AlgoSdk.AppCallTxn x, AlgoSdk.AppIndex value) => x.ApplicationId = value).Assign("apan", (AlgoSdk.AppCallTxn x) => x.OnComplete, (ref AlgoSdk.AppCallTxn x, AlgoSdk.OnCompletion value) => x.OnComplete = value, AlgoSdk.ByteEnumComparer<AlgoSdk.OnCompletion>.Instance).Assign("apap", (AlgoSdk.AppCallTxn x) => x.ApprovalProgram, (ref AlgoSdk.AppCallTxn x, AlgoSdk.CompiledTeal value) => x.ApprovalProgram = value).Assign("apsu", (AlgoSdk.AppCallTxn x) => x.ClearStateProgram, (ref AlgoSdk.AppCallTxn x, AlgoSdk.CompiledTeal value) => x.ClearStateProgram = value).Assign("apaa", (AlgoSdk.AppCallTxn x) => x.AppArguments, (ref AlgoSdk.AppCallTxn x, AlgoSdk.CompiledTeal[] value) => x.AppArguments = value, AlgoSdk.ArrayComparer<AlgoSdk.CompiledTeal>.Instance).Assign("apat", (AlgoSdk.AppCallTxn x) => x.Accounts, (ref AlgoSdk.AppCallTxn x, AlgoSdk.Address[] value) => x.Accounts = value, AlgoSdk.ArrayComparer<AlgoSdk.Address>.Instance).Assign("apfa", (AlgoSdk.AppCallTxn x) => x.ForeignApps, (ref AlgoSdk.AppCallTxn x, System.UInt64[] value) => x.ForeignApps = value, AlgoSdk.ArrayComparer<System.UInt64>.Instance).Assign("apas", (AlgoSdk.AppCallTxn x) => x.ForeignAssets, (ref AlgoSdk.AppCallTxn x, System.UInt64[] value) => x.ForeignAssets = value, AlgoSdk.ArrayComparer<System.UInt64>.Instance).Assign("apgs", (AlgoSdk.AppCallTxn x) => x.GlobalStateSchema, (ref AlgoSdk.AppCallTxn x, AlgoSdk.StateSchema value) => x.GlobalStateSchema = value).Assign("apls", (AlgoSdk.AppCallTxn x) => x.LocalStateSchema, (ref AlgoSdk.AppCallTxn x, AlgoSdk.StateSchema value) => x.LocalStateSchema = value).Assign("apep", (AlgoSdk.AppCallTxn x) => x.ExtraProgramPages, (ref AlgoSdk.AppCallTxn x, System.UInt64 value) => x.ExtraProgramPages = value));
            return true;
        }
        
        public partial struct Params
        {
            
            private static bool @__generated__IsValid = Params.@__generated__InitializeAlgoApiFormatters();
            
            private static bool @__generated__InitializeAlgoApiFormatters()
            {
                AlgoSdk.AlgoApiFormatterLookup.Add<AlgoSdk.AppCallTxn.Params>(new AlgoSdk.AlgoApiObjectFormatter<AlgoSdk.AppCallTxn.Params>(false).Assign("apid", (AlgoSdk.AppCallTxn.Params x) => x.ApplicationId, (ref AlgoSdk.AppCallTxn.Params x, AlgoSdk.AppIndex value) => x.ApplicationId = value).Assign("apan", (AlgoSdk.AppCallTxn.Params x) => x.OnComplete, (ref AlgoSdk.AppCallTxn.Params x, AlgoSdk.OnCompletion value) => x.OnComplete = value, AlgoSdk.ByteEnumComparer<AlgoSdk.OnCompletion>.Instance).Assign("apap", (AlgoSdk.AppCallTxn.Params x) => x.ApprovalProgram, (ref AlgoSdk.AppCallTxn.Params x, AlgoSdk.CompiledTeal value) => x.ApprovalProgram = value).Assign("apsu", (AlgoSdk.AppCallTxn.Params x) => x.ClearStateProgram, (ref AlgoSdk.AppCallTxn.Params x, AlgoSdk.CompiledTeal value) => x.ClearStateProgram = value).Assign("apaa", (AlgoSdk.AppCallTxn.Params x) => x.AppArguments, (ref AlgoSdk.AppCallTxn.Params x, AlgoSdk.CompiledTeal[] value) => x.AppArguments = value, AlgoSdk.ArrayComparer<AlgoSdk.CompiledTeal>.Instance).Assign("apat", (AlgoSdk.AppCallTxn.Params x) => x.Accounts, (ref AlgoSdk.AppCallTxn.Params x, AlgoSdk.Address[] value) => x.Accounts = value, AlgoSdk.ArrayComparer<AlgoSdk.Address>.Instance).Assign("apfa", (AlgoSdk.AppCallTxn.Params x) => x.ForeignApps, (ref AlgoSdk.AppCallTxn.Params x, System.UInt64[] value) => x.ForeignApps = value, AlgoSdk.ArrayComparer<System.UInt64>.Instance).Assign("apas", (AlgoSdk.AppCallTxn.Params x) => x.ForeignAssets, (ref AlgoSdk.AppCallTxn.Params x, System.UInt64[] value) => x.ForeignAssets = value, AlgoSdk.ArrayComparer<System.UInt64>.Instance).Assign("global-state-schema", (AlgoSdk.AppCallTxn.Params x) => x.GlobalStateSchema, (ref AlgoSdk.AppCallTxn.Params x, AlgoSdk.StateSchema value) => x.GlobalStateSchema = value).Assign("local-state-schema", (AlgoSdk.AppCallTxn.Params x) => x.LocalStateSchema, (ref AlgoSdk.AppCallTxn.Params x, AlgoSdk.StateSchema value) => x.LocalStateSchema = value).Assign("epp", (AlgoSdk.AppCallTxn.Params x) => x.ExtraProgramPages, (ref AlgoSdk.AppCallTxn.Params x, System.UInt64 value) => x.ExtraProgramPages = value));
                return true;
            }
        }
    }
}