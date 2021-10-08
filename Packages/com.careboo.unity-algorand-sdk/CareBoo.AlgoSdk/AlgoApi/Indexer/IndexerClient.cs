using System;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public struct IndexerClient : IIndexerClient
    {
        readonly string address;
        readonly string token;

        public IndexerClient(string address, string token)
        {
            this.address = address.TrimEnd('/');
            this.token = token;
        }

        public string Address => address;

        public string Token => token;

        public async UniTask<AlgoApiResponse<HealthCheck>> GetHealth()
        {
            return await this.GetAsync("/health");
        }

        public async UniTask<AlgoApiResponse<AccountsResponse>> GetAccounts(
            Optional<ulong> applicationId = default,
            Optional<ulong> assetId = default,
            Address authAddr = default,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            FixedString128Bytes next = default,
            Optional<ulong> round = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            queryBuilder
                .Add("application-id", applicationId)
                .Add("asset-id", assetId)
                .Add("auth-addr", authAddr)
                .Add("currency-greater-than", currencyGreaterThan)
                .Add("currency-less-than", currencyLessThan)
                .Add("include-all", includeAll)
                .Add("limit", limit)
                .AddFixedString("next", next)
                .Add("round", round)
                ;
            return await this.GetAsync("/v2/accounts" + queryBuilder.ToString());
        }

        public async UniTask<AlgoApiResponse<AccountResponse>> GetAccount(
            Address accountAddress,
            Optional<bool> includeAll = default,
            Optional<ulong> round = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            queryBuilder
                .Add("include-all", includeAll)
                .Add("round", round)
                ;
            return await this.GetAsync($"/v2/accounts/{accountAddress}{queryBuilder}");
        }

        public async UniTask<AlgoApiResponse<TransactionsResponse>> GetAccountTransactions(
            Address accountAddress,
            DateTime afterTime = default,
            Optional<ulong> assetId = default,
            DateTime beforeTime = default,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<ulong> limit = default,
            Optional<ulong> maxRound = default,
            Optional<ulong> minRound = default,
            FixedString128Bytes next = default,
            string notePrefix = default,
            Optional<bool> rekeyTo = default,
            Optional<ulong> round = default,
            SignatureType sigType = default,
            TransactionType txType = default,
            FixedString64Bytes txid = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            queryBuilder
                .Add("account-address", accountAddress)
                .Add("after-time", afterTime)
                .Add("asset-id", assetId)
                .Add("before-time", beforeTime)
                .Add("currency-greater-than", currencyGreaterThan)
                .Add("currency-less-than", currencyLessThan)
                .Add("limit", limit)
                .Add("max-round", maxRound)
                .Add("min-round", minRound)
                .AddFixedString("next", next)
                .Add("note-prefix", notePrefix)
                .Add("rekey-to", rekeyTo)
                .Add("round", round)
                .AddFixedString("sig-type", sigType.ToFixedString())
                .AddFixedString("tx-type", txType.ToFixedString())
                .AddFixedString("txid", txid)
                ;
            return await this.GetAsync($"/v2/accounts/{accountAddress}/transactions{queryBuilder}");
        }

        public async UniTask<AlgoApiResponse<ApplicationsResponse>> GetApplications(
            Optional<ulong> applicationId = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            FixedString128Bytes next = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            queryBuilder
                .Add("application-id", applicationId)
                .Add("include-all", includeAll)
                .Add("limit", limit)
                .AddFixedString("next", next)
                ;
            return await this.GetAsync("/v2/applications" + queryBuilder.ToString());
        }

        public async UniTask<AlgoApiResponse<ApplicationResponse>> GetApplication(
            ulong applicationId,
            Optional<bool> includeAll = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            queryBuilder
                .Add("include-all", includeAll)
                ;
            return await this.GetAsync($"/v2/applications/{applicationId}{queryBuilder}");
        }

        public async UniTask<AlgoApiResponse<AssetsResponse>> GetAssets(
            Optional<ulong> assetId = default,
            Address creator = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            string name = default,
            FixedString128Bytes next = default,
            string unit = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            queryBuilder.Add("asset-id", assetId)
                .Add("creator", creator)
                .Add("include-all", includeAll)
                .Add("limit", limit)
                .Add("name", name)
                .Add("next", next)
                .Add("unit", unit)
                ;
            return await this.GetAsync($"/v2/assets{queryBuilder}");
        }

        public async UniTask<AlgoApiResponse<AssetResponse>> GetAsset(
            ulong assetId,
            Optional<bool> includeAll = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            queryBuilder
                .Add("include-all", includeAll)
                ;
            return await this.GetAsync($"/v2/assets/{assetId}{queryBuilder}");
        }

        public async UniTask<AlgoApiResponse<BalancesResponse>> GetAssetBalances(
            ulong assetId,
            Optional<ulong> currencyGreaterThan = default,
            Optional<ulong> currencyLessThan = default,
            Optional<bool> includeAll = default,
            Optional<ulong> limit = default,
            FixedString128Bytes next = default,
            Optional<ulong> round = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            queryBuilder
                .Add("currency-greater-than", currencyGreaterThan)
                .Add("currency-less-than", currencyLessThan)
                .Add("include-all", includeAll)
                .Add("limit", limit)
                .AddFixedString("next", next)
                .Add("round", round)
                ;
            return await this.GetAsync($"/v2/assets/{assetId}/balances{queryBuilder}");
        }

        public async UniTask<AlgoApiResponse<TransactionsResponse>> GetAssetTransactions(
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
            FixedString128Bytes next = default,
            string notePrefix = default,
            Optional<bool> rekeyTo = default,
            Optional<ulong> round = default,
            SignatureType sigType = default,
            TransactionType txType = default,
            FixedString64Bytes txid = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            queryBuilder
                .Add("address", address)
                .AddFixedString("address-role", addressRole.ToFixedString())
                .Add("after-time", afterTime)
                .Add("before-time", beforeTime)
                .Add("currency-greater-than", currencyGreaterThan)
                .Add("currency-less-than", currencyLessThan)
                .Add("exclude-close-to", excludeCloseTo)
                .Add("limit", limit)
                .Add("max-round", maxRound)
                .Add("min-round", minRound)
                .AddFixedString("next", next)
                .Add("note-prefix", notePrefix)
                .Add("rekey-to", rekeyTo)
                .Add("round", round)
                .AddFixedString("sig-type", sigType.ToFixedString())
                .AddFixedString("tx-type", txType.ToFixedString())
                .AddFixedString("txid", txid)
                ;
            return await this.GetAsync($"/v2/assets/{assetId}/transactions{queryBuilder}");
        }

        public async UniTask<AlgoApiResponse<Block>> GetBlock(ulong round)
        {
            return await this.GetAsync($"/v2/blocks/{round}");
        }

        public async UniTask<AlgoApiResponse<TransactionsResponse>> GetTransactions(
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
            FixedString128Bytes next = default,
            string notePrefix = default,
            Optional<bool> rekeyTo = default,
            Optional<ulong> round = default,
            SignatureType sigType = default,
            TransactionType txType = default,
            FixedString64Bytes txid = default
        )
        {
            using var queryBuilder = new QueryBuilder(Allocator.Persistent);
            queryBuilder
                .Add("address", address)
                .AddFixedString("address-role", addressRole.ToFixedString())
                .Add("after-time", afterTime)
                .Add("application-id", applicationId)
                .Add("asset-id", assetId)
                .Add("before-time", beforeTime)
                .Add("currency-greater-than", currencyGreaterThan)
                .Add("currency-less-than", currencyLessThan)
                .Add("exclude-close-to", excludeCloseTo)
                .Add("limit", limit)
                .Add("max-round", maxRound)
                .Add("min-round", minRound)
                .AddFixedString("next", next)
                .Add("note-prefix", notePrefix)
                .Add("rekey-to", rekeyTo)
                .Add("round", round)
                .AddFixedString("sig-type", sigType.ToFixedString())
                .AddFixedString("tx-type", txType.ToFixedString())
                .AddFixedString("txid", txid)
                ;
            return await this.GetAsync("/v2/transactions" + queryBuilder.ToString());
        }

        public async UniTask<AlgoApiResponse<TransactionResponse>> GetTransaction(FixedString64Bytes txid)
        {
            return await this.GetAsync($"/v2/transactions/{txid}");
        }
    }
}
