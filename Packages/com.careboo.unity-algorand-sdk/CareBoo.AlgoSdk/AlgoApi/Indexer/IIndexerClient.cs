using System;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IIndexerClient : IApiClient
    {
        UniTask<AlgoApiResponse<HealthCheck>> GetHealth();
        UniTask<AlgoApiResponse<AccountsResponse>> GetAccounts(
            Optional<ulong> applicationId = default,
            Optional<ulong> assetId = default,
            Address authAddr = default,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            Optional<FixedString128Bytes> next = default,
            Optional<ulong> round = default
        );
        UniTask<AlgoApiResponse<AccountResponse>> GetAccount(
            Address accountAddress,
            Optional<bool> includeAll = default,
            Optional<ulong> round = default
        );
        UniTask<AlgoApiResponse<TransactionsResponse>> GetAccountTransactions(
            Address accountAddress,
            DateTime afterTime = default,
            Optional<ulong> assetId = default,
            DateTime beforeTime = default,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<ulong> limit = default,
            Optional<ulong> maxRound = default,
            Optional<ulong> minRound = default,
            Optional<FixedString128Bytes> next = default,
            string notePrefix = default,
            Optional<bool> rekeyTo = default,
            Optional<ulong> round = default,
            SignatureType sigType = default,
            TransactionType txType = default,
            Optional<FixedString64Bytes> txid = default
        );
        UniTask<AlgoApiResponse<ApplicationsResponse>> GetApplications(
            Optional<ulong> applicationId = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            Optional<FixedString128Bytes> next = default
        );
        UniTask<AlgoApiResponse<ApplicationResponse>> GetApplication(
            ulong applicationId,
            Optional<bool> includeAll = default
        );
        UniTask<AlgoApiResponse<AssetsResponse>> GetAssets(
            Optional<ulong> assetId = default,
            Address creator = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            string name = default,
            Optional<FixedString128Bytes> next = default,
            string unit = default
        );
        UniTask<AlgoApiResponse<AssetResponse>> GetAsset(
            ulong assetId,
            Optional<bool> includeAll = default
        );
        UniTask<AlgoApiResponse<BalancesResponse>> GetAssetBalances(
            ulong assetId,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            Optional<FixedString128Bytes> next = default,
            Optional<ulong> round = default
        );
        UniTask<AlgoApiResponse<TransactionsResponse>> GetAssetTransactions(
            ulong assetId,
            Address address = default,
            AddressRole addressRole = default,
            DateTime afterTime = default,
            DateTime beforeTime = default,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<bool> excludeCloseTo = default,
            Optional<ulong> limit = default,
            Optional<ulong> maxRound = default,
            Optional<ulong> minRound = default,
            Optional<FixedString128Bytes> next = default,
            string notePrefix = default,
            Optional<bool> rekeyTo = default,
            Optional<ulong> round = default,
            SignatureType sigType = default,
            TransactionType txType = default,
            Optional<FixedString64Bytes> txid = default
        );
        UniTask<AlgoApiResponse<Block>> GetBlock(ulong round);
        UniTask<AlgoApiResponse<TransactionsResponse>> GetTransactions(
            Address address = default,
            AddressRole addressRole = default,
            DateTime afterTime = default,
            Optional<ulong> applicationId = default,
            Optional<ulong> assetId = default,
            DateTime beforeTime = default,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<bool> excludeCloseTo = default,
            Optional<ulong> limit = default,
            Optional<ulong> maxRound = default,
            Optional<ulong> minRound = default,
            Optional<FixedString128Bytes> next = default,
            string notePrefix = default,
            Optional<bool> rekeyTo = default,
            Optional<ulong> round = default,
            SignatureType sigType = default,
            TransactionType txType = default,
            Optional<FixedString64Bytes> txid = default
        );
        UniTask<AlgoApiResponse<TransactionResponse>> GetTransaction(FixedString64Bytes txid);
    }
}
