using System;
using System.Collections.Generic;

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        private static readonly Dictionary<Type, object> lookup;

        static FieldMaps()
        {
            lookup = new Dictionary<Type, object>()
            {
                {typeof(RawTransaction), rawTransactionFields},
                {typeof(RawSignedTransaction), rawSignedTransactionFields},
                {typeof(Account), accountFields},
                {typeof(ErrorResponse), errorResponseFields},
                {typeof(AccountParticipation), accountParticipationFields},
                {typeof(AccountStateDelta), accountStateDeltaFields},
                {typeof(Application), applicationFields},
                {typeof(ApplicationLocalState), applicationLocalStateFields},
                {typeof(ApplicationParams), applicationParamsFields},
                {typeof(ApplicationStateSchema), applicationStateSchemaFields},
                {typeof(Asset), assetFields},
                {typeof(AssetHolding), assetHoldingFields},
                {typeof(AssetParams), assetParamsFields},
                {typeof(BuildVersion), buildVersionFields},
                {typeof(CatchupMessage), catchupMessageFields},
                {typeof(DryrunRequest), dryrunRequestFields},
                {typeof(DryrunSource), dryrunSourceFields},
                {typeof(DryrunResults), dryrunResultsFields},
                {typeof(DryrunTxnResult), dryrunTxnResultFields},
                {typeof(DryrunState), dryrunStateFields},
                {typeof(PendingTransactions), pendingTransactionsFields},
                {typeof(PendingTransaction), pendingTransactionFields},
                {typeof(Block), blockFields},
                {typeof(Block.Header), block_headerFields},
                {typeof(BlockTransaction), blockTransactionFields},
                {typeof(MerkleProof), merkleProofFields},
                {typeof(LedgerSupply), ledgerSupplyFields},
                {typeof(Status), statusFields},
                {typeof(Version), versionFields},
                {typeof(TransactionParams), transactionParamsFields},
                {typeof(EvalDeltaKeyValue), evalDeltaKeyValueFields},
                {typeof(EvalDelta), evalDeltaFields}
            };
        }

        internal static Field<T>.Map GetFieldMap<T>()
            where T : struct
        {
            if (lookup.TryGetValue(typeof(T), out var map))
                return (Field<T>.Map)map;
            return null;
        }
    }
}
