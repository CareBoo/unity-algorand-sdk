using System;
using System.Text;
using Algorand.Unity.Experimental.Abi;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Algorand.Unity.Samples.CallingSmartContractAbi
{
    public class CallingSmartContractAbi : MonoBehaviour
    {
        public AlgodClientAsset algod;

        public IndexerClientAsset indexer;

        public KmdClientAsset kmd;

        public ContractAsset contract;

        public TextAsset approvalTeal;

        public TextAsset clearTeal;

        public SmartContractUI smartContractUI;

        private IAsyncAccountSigner account;

        private MicroAlgos accountBalance = 0;

        private void Awake()
        {
            Debug.Assert(algod);
            Debug.Assert(indexer);
            Debug.Assert(kmd);
            Debug.Assert(contract);
            Debug.Assert(approvalTeal);
            Debug.Assert(clearTeal);
        }

        private void Start()
        {
            StartAsync().Forget();
        }

        private async UniTaskVoid StartAsync()
        {
            smartContractUI.enabled = false;
            try
            {
                account = await GetAccount();
                smartContractUI.account = account;
                smartContractUI.algod = algod.client;
                smartContractUI.contractIndex = await GetOrCreateContract();
                smartContractUI.contract = contract;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                smartContractUI.error = ex.Message ?? ex.ToString();
            }

            smartContractUI.enabled = true;
        }

        private async UniTask<IAsyncAccountSigner> GetAccount()
        {
            var (listWalletsErr, listWalletsResponse) = await kmd.client.ListWallets();
            listWalletsErr.ThrowIfError();
            var wallet = listWalletsResponse.Wallets[0];
            var (initHandleErr, initHandleResponse) = await kmd.client.InitWalletHandleToken(wallet.Id, "");
            initHandleErr.ThrowIfError();
            var (keysErr, keysResponse) = await kmd.client.ListKeys(initHandleResponse.WalletHandleToken);
            keysErr.ThrowIfError();
            await kmd.client.ReleaseWalletHandleToken(initHandleResponse.WalletHandleToken);
            return new KmdAccount(kmd.client, wallet.Id, "", keysResponse.Addresses[0]);
        }

        private async UniTask<AppIndex> GetOrCreateContract()
        {
            var approval = await CompileTeal(approvalTeal.text);
            var clear = await CompileTeal(clearTeal.text);
            var maybeExistingContract = await GetContract(approval, clear);
            return maybeExistingContract.Else(await CreateContract(approval, clear));
        }

        private async UniTask<Optional<AppIndex>> GetContract(CompiledTeal approval, CompiledTeal clear)
        {
            var (err, response) = await indexer.client.LookupAccountCreatedApplications(account.Address);
            err.ThrowIfError();
            if (response.Applications == null)
            {
                return default;
            }

            foreach (var application in response.Applications)
            {
                if (application.Params.ApprovalProgram.Equals(approval)
                    && application.Params.ClearStateProgram.Equals(clear)
                    && !application.Deleted.Else(false)
                    && !application.DeletedAtRound.HasValue)
                {
                    Debug.Log($"Found existing contract: {application.Id}");
                    return new Optional<AppIndex>((AppIndex)application.Id);
                }
            }

            return default;
        }

        private async UniTask<AppIndex> CreateContract(CompiledTeal approval, CompiledTeal clear)
        {
            var txnParams = await GetSuggestedParams();

            var txn = Transaction.AppCreate(
                account.Address,
                txnParams,
                approval,
                clear
            );
            var signedTxn = await txn.SignWithAsync(account);

            var txid = await SendTransaction(signedTxn);
            var (err, confirmedTxn) = await algod.client.WaitForConfirmation(txid);
            err.ThrowIfError();
            return confirmedTxn.ApplicationIndex.Value;
        }

        private async UniTask<byte[]> CompileTeal(string teal)
        {
            var (error, compiled) = await algod.client.TealCompile(Encoding.UTF8.GetBytes(teal));
            error.ThrowIfError();
            return System.Convert.FromBase64String(compiled.Result);
        }

        private async UniTask<TransactionParams> GetSuggestedParams()
        {
            var (error, txnParams) = await algod.client.TransactionParams();
            error.ThrowIfError();
            return txnParams;
        }

        private async UniTask<string> SendTransaction(byte[] signedTxn)
        {
            var (error, response) = await algod.client.RawTransaction(signedTxn);
            error.ThrowIfError();
            return response.TxId;
        }
    }
}
