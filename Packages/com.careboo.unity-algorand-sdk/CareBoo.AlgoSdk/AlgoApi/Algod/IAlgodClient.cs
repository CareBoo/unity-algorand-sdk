using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IAlgodClient : ITokenizedApiClient
    {
        UniTask<AlgoApiResponse> GetGenesisInformation();
        UniTask<AlgoApiResponse> GetHealth();
        UniTask<AlgoApiResponse> GetMetrics();
        UniTask<AlgoApiResponse> GetSwaggerSpec();
        UniTask<AlgoApiResponse<Account>> GetAccountInformation(Address accountAddress);
        UniTask<AlgoApiResponse<PendingTransactions>> GetPendingTransactions(ulong max = 0);
        UniTask<AlgoApiResponse<PendingTransactions>> GetPendingTransactions(Address accountAddress, ulong max = 0);
        UniTask<AlgoApiResponse<PendingTransaction>> GetPendingTransaction(FixedString64Bytes txId);
        UniTask<AlgoApiResponse<Application>> GetApplication(ulong applicationId);
        UniTask<AlgoApiResponse<Asset>> GetAsset(ulong assetId);
        UniTask<AlgoApiResponse<BlockResponse>> GetBlock(ulong round);
        UniTask<AlgoApiResponse<MerkleProof>> GetMerkleProof(ulong round, FixedString64Bytes txid);
        UniTask<AlgoApiResponse<CatchupMessage>> StartCatchup(string catchpoint);
        UniTask<AlgoApiResponse<CatchupMessage>> AbortCatchup(string catchpoint);
        UniTask<AlgoApiResponse<LedgerSupply>> GetLedgerSupply();
        UniTask<AlgoApiResponse<TransactionIdResponse>> RegisterParticipationKeys(
            Address accountAddress,
            ulong fee = 1000,
            Optional<ulong> keyDilution = default,
            Optional<bool> noWait = default,
            Optional<bool> roundLastValid = default);
        UniTask<AlgoApiResponse> ShutDown(Optional<ulong> timeout = default);
        UniTask<AlgoApiResponse<Status>> GetCurrentStatus();
        UniTask<AlgoApiResponse<Status>> GetStatusAfterWaitingForRound(ulong round);
        UniTask<AlgoApiResponse<TealCompilationResult>> TealCompile(string source);
        UniTask<AlgoApiResponse<DryrunResults>> TealDryrun(Optional<DryrunRequest> request = default);
        UniTask<AlgoApiResponse<TransactionIdResponse>> SendTransaction(SignedTransaction rawTxn);
        UniTask<AlgoApiResponse<TransactionParams>> GetTransactionParams();
        UniTask<AlgoApiResponse<Version>> GetVersions();
    }
}
