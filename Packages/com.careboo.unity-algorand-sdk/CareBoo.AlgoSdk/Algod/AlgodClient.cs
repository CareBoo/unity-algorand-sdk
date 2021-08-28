using Cysharp.Threading.Tasks;

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

        public async UniTask<AlgoApiResponse<Block>> GetBlock(ulong round)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<MerkleProof>> GetMerkleProof(ulong round, TransactionId txid)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<CatchupMessage>> StartCatchup(string catchpoint)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<CatchupMessage>> AbortCatchup(string catchpoint)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<LedgerSupply>> GetLedgerSupply()
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<TransactionId>> RegisterParticipationKeys(Address accountAddress, ulong fee = 1000, Optional<ulong> keyDilution = default, Optional<bool> noWait = default, Optional<bool> roundLastValid = default)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse> ShutDown(Optional<ulong> timeout = default)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<Status>> GetCurrentStatus()
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<Status>> GetStatusOfBlockAfterRound(ulong round)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<TealCompilationResult>> TealCompile(string source)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<DryrunResults>> TealDryrun(Optional<DryrunRequest> request = default)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<TransactionId>> BroadcastTransaction(RawSignedTransaction rawTxn)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<TransactionParams>> GetTransactionParams()
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse<Version>> GetVersions()
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<AlgoApiResponse> GetAsync(string endpoint)
        {
            return await AlgoApiRequest.Get(token, GetUrl(endpoint)).Send();
        }

        public async UniTask<AlgoApiResponse> PostAsync(string endpoint, string postData)
        {
            return await AlgoApiRequest.Post(token, GetUrl(endpoint), postData).Send();
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
