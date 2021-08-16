using AlgoSdk.MsgPack;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IAlgodClient
    {
        UniTask<Response<string>> GetGenesisInformation();
        UniTask<Response> GetHealth();
        UniTask<Response<string>> GetMetrics();
        UniTask<Response<string>> GetSwaggerSpec();
        UniTask<Response<Account>> GetAccountInformation(Address accountAddress);
        UniTask<Response<PendingTransactions>> GetPendingTransactions();
        UniTask<Response<PendingTransactions>> GetPendingTransactions(Address accountAddress);
        UniTask<Response<PendingTransaction>> GetPendingTransaction(TransactionId txId);
        UniTask<Response<Application>> GetApplication(ulong applicationId);
        UniTask<Response<Asset>> GetAsset(ulong assetId);
        UniTask<Response<Block>> GetBlock(ulong round);
        UniTask<Response<MerkleProof>> GetMerkleProof(ulong round, TransactionId txid);
        UniTask<Response<CatchupMessage>> StartCatchup(in NativeText catchpoint);
        UniTask<Response<CatchupMessage>> AbortCatchup(in NativeText catchpoint);
        UniTask<Response<LedgerSupply>> GetLedgerSupply();
        UniTask<Response<TransactionId>> RegisterParticipationKeys(
            Address accountAddress,
            ulong fee = 1000,
            Optional<ulong> keyDilution = default,
            Optional<bool> noWait = default,
            Optional<bool> roundLastValid = default);
        UniTask<Response> ShutDown(Optional<ulong> timeout = default);
        UniTask<Response<Status>> GetCurrentStatus();
        UniTask<Response<Status>> GetStatusOfBlockAfterRound(ulong round);
        UniTask<Response<TealCompilationResult>> TealCompile(in NativeText source, Allocator allocator);
        UniTask<Response<DryrunResults>> TealDryrun(Optional<DryrunRequest> request = default);
        UniTask<Response<TransactionId>> BroadcastTransaction(RawTransaction rawTxn);
        UniTask<Response<TransactionParams>> GetTransactionParams();
        UniTask<Response<Version>> GetVersions();
    }
}
