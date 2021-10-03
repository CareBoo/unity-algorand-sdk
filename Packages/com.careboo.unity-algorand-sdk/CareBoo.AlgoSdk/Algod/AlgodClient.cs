using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public partial struct AlgodClient : IAlgodClient
    {
        readonly string address;
        readonly string token;

        public AlgodClient(string address, string token)
        {
            this.address = address[address.Length - 1] == '/'
                ? address.Substring(0, address.Length - 1)
                : address;
            this.token = token;
        }

        public async UniTask<AlgoApiResponse> GetGenesisInformation()
        {
            return await GetAsync("/genesis");
        }

        public async UniTask<AlgoApiResponse> GetHealth()
        {
            return await GetAsync("/health");
        }

        public async UniTask<AlgoApiResponse> GetMetrics()
        {
            return await GetAsync("/metrics");
        }

        public async UniTask<AlgoApiResponse> GetSwaggerSpec()
        {
            return await GetAsync("/swagger.json");
        }

        public async UniTask<AlgoApiResponse<Account>> GetAccountInformation(Address accountAddress)
        {
            return await GetAsync($"/v2/accounts/{accountAddress}");
        }

        public async UniTask<AlgoApiResponse<PendingTransactions>> GetPendingTransactions(ulong max = 0)
        {
            return await GetAsync($"/v2/transactions/pending?max={max}");
        }

        public async UniTask<AlgoApiResponse<PendingTransactions>> GetPendingTransactions(Address accountAddress, ulong max = 0)
        {
            return await GetAsync($"/v2/accounts/{accountAddress}/transactions/pending?max={max}");
        }

        public async UniTask<AlgoApiResponse<PendingTransaction>> GetPendingTransaction(TransactionId txid)
        {
            return await GetAsync($"/v2/transactions/pending/{txid}");
        }

        public async UniTask<AlgoApiResponse<Application>> GetApplication(ulong applicationId)
        {
            return await GetAsync($"/v2/applications/{applicationId}");
        }

        public async UniTask<AlgoApiResponse<Asset>> GetAsset(ulong assetId)
        {
            return await GetAsync($"/v2/assets/{assetId}");
        }

        public async UniTask<AlgoApiResponse<BlockResponse>> GetBlock(ulong round)
        {
            return await GetAsync($"/v2/blocks/{round}");
        }

        public async UniTask<AlgoApiResponse<MerkleProof>> GetMerkleProof(ulong round, TransactionId txid)
        {
            return await GetAsync($"/v2/blocks/{round}/transactions/{txid}/proof");
        }

        public async UniTask<AlgoApiResponse<CatchupMessage>> StartCatchup(string catchpoint)
        {
            return await PostAsync($"/v2/catchup/{catchpoint}");
        }

        public async UniTask<AlgoApiResponse<CatchupMessage>> AbortCatchup(string catchpoint)
        {
            return await DeleteAsync($"/v2/catchup/{catchpoint}");
        }

        public async UniTask<AlgoApiResponse<LedgerSupply>> GetLedgerSupply()
        {
            return await GetAsync("/v2/ledger/supply");
        }

        public async UniTask<AlgoApiResponse<TransactionId>> RegisterParticipationKeys(
            Address accountAddress,
            ulong fee = 1000,
            Optional<ulong> keyDilution = default,
            Optional<bool> noWait = default,
            Optional<bool> roundLastValid = default)
        {
            var endpoint = new NativeText("/v2/register-participation-keys/", Allocator.Temp);
            endpoint.Append(accountAddress.ToFixedString());
            var queryParams = new NativeList<FixedString64Bytes>(4, Allocator.Temp);
            if (fee != 1000)
            {
                var feeParam = new FixedString64Bytes("fee=");
                feeParam.Append(fee);
                queryParams.AddNoResize(feeParam);
            }
            if (keyDilution.HasValue)
            {
                var keyDilutionParam = new FixedString64Bytes("key-dilution=");
                keyDilutionParam.Append(keyDilution.Value);
                queryParams.AddNoResize(keyDilutionParam);
            }
            if (noWait.HasValue)
            {
                var noWaitParam = new FixedString64Bytes("no-wait=");
                noWaitParam.Append(noWait.Value);
                queryParams.AddNoResize(noWaitParam);
            }
            if (roundLastValid.HasValue)
            {
                var roundLastValidParam = new FixedString64Bytes("round-last-valid=");
                roundLastValidParam.Append(roundLastValid.Value);
                queryParams.AddNoResize(roundLastValidParam);
            }
            if (queryParams.Length > 0)
            {
                for (var i = 0; i < queryParams.Length; i++)
                {
                    endpoint.Append(i == 0 ? "?" : "&");
                    endpoint.Append(queryParams[i]);
                }
            }
            var endpointString = endpoint.ToString();
            endpoint.Dispose();
            queryParams.Dispose();
            return await PostAsync(endpointString);
        }

        public async UniTask<AlgoApiResponse> ShutDown(Optional<ulong> timeout = default)
        {
            return await PostAsync("/v2/shutdown");
        }

        public async UniTask<AlgoApiResponse<Status>> GetCurrentStatus()
        {
            return await GetAsync("/v2/status");
        }

        public async UniTask<AlgoApiResponse<Status>> GetStatusAfterWaitingForRound(ulong round)
        {
            return await GetAsync($"/v2/status/wait-for-block-after/{round}");
        }

        public async UniTask<AlgoApiResponse<TealCompilationResult>> TealCompile(string source)
        {
            return await PostAsync("/v2/teal/compile", source);
        }

        public async UniTask<AlgoApiResponse<DryrunResults>> TealDryrun(Optional<DryrunRequest> request = default)
        {
            const string endpoint = "/v2/teal/dryrun";
            using var data = new NativeList<byte>(Allocator.Persistent);
            AlgoApiSerializer.SerializeMessagePack(request.Value, data);
            return request.HasValue
                ? await PostAsync(endpoint, data.AsArray().AsReadOnly())
                : await PostAsync(endpoint);
        }

        public async UniTask<AlgoApiResponse<TransactionId>> SendTransaction(SignedTransaction rawTxn)
        {
            using var data = new NativeList<byte>(Allocator.Persistent);
            AlgoApiSerializer.SerializeMessagePack(rawTxn, data);
            return await PostAsync("/v2/transactions", data.AsArray().AsReadOnly());
        }

        public async UniTask<AlgoApiResponse<TransactionParams>> GetTransactionParams()
        {
            return await GetAsync("/v2/transactions/params");
        }

        public async UniTask<AlgoApiResponse<Version>> GetVersions()
        {
            return await GetAsync("/versions");
        }

        public async UniTask<AlgoApiResponse> GetAsync(string endpoint)
        {
            return await AlgoApiRequest.Get(token, GetUrl(endpoint)).Send();
        }

        public async UniTask<AlgoApiResponse> PostAsync(string endpoint)
        {
            return await AlgoApiRequest.Post(token, GetUrl(endpoint)).Send();
        }

        public async UniTask<AlgoApiResponse> PostAsync(string endpoint, NativeArray<byte>.ReadOnly data)
        {
            return await PostAsync(endpoint, data.ToArray());
        }

        public async UniTask<AlgoApiResponse> PostAsync(string endpoint, byte[] data)
        {
            return await AlgoApiRequest.Post(token, GetUrl(endpoint), data).Send();
        }

        public async UniTask<AlgoApiResponse> PostAsync(string endpoint, string source)
        {
            return await AlgoApiRequest.Post(token, GetUrl(endpoint), source).Send();
        }

        public async UniTask<AlgoApiResponse> DeleteAsync(string endpoint)
        {
            return await AlgoApiRequest.Delete(token, GetUrl(endpoint)).Send();
        }

        public string GetUrl(string endpoint)
        {
            return address + endpoint;
        }
    }
}
