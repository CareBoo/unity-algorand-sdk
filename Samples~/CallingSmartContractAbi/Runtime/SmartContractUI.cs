using System.Linq;
using Algorand.Unity.Experimental.Abi;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.CallingSmartContractAbi
{
    [RequireComponent(typeof(UIDocument))]
    public class SmartContractUI : MonoBehaviour
    {
        public IAsyncAccountSigner account { get; set; }
        public AlgodClient algod { get; set; }
        public AppIndex contractIndex { get; set; }
        public Contract contract { get; set; }
        public string error { get; set; }

        public ContractField contractField { get; protected set; }

        private void OnEnable()
        {
            var document = GetComponent<UIDocument>();

            if (!string.IsNullOrEmpty(error))
            {
                var root = document.rootVisualElement;
                var title = new Label("An error occurred trying to connect to your AlgoKit LocalNet.");
                var message = new Label(error);

                root.Add(title);
                root.Add(message);
                return;
            }

            if (contractIndex == 0)
            {
                return;
            }

            if (contractField == null)
            {
                contractField = new ContractField(contract, CallContractAsync);
                document.rootVisualElement.Add(contractField);
            }
        }

        private async UniTask<string> CallContractAsync(int methodIndex, IAbiValue[] methodArgs)
        {
            var confirmed = await MakeSmartContractCalls(methodIndex, methodArgs);
            var result = confirmed.Results[0];
            if (result.DecodeError != null)
                return result.DecodeError;
            return result.Method.Returns.Type != null
                    ? result.ReturnValue.ToString()
                    : "void"
                ;
        }

        private async UniTask<AtomicTxn.Confirmed> MakeSmartContractCalls(int methodIndex, IAbiValue[] methodArgs)
        {
            var txnParams = await GetSuggestedParams();
            var buildingAtomicTxn = Transaction.Atomic();
            var method = contract.Methods[methodIndex];
            var hasPaymentTxn = method.Arguments.Any(a => a.Type.IsTransaction());
            if (hasPaymentTxn)
            {
                buildingAtomicTxn.AddTxn(Transaction.Payment(account.Address, txnParams, account.Address, 1_000_000));
            }

            methodArgs = methodArgs.Where(m => m != null).ToArray();
            buildingAtomicTxn.AddMethodCall(account.Address, txnParams, contractIndex, method, methodArgs);

            var built = buildingAtomicTxn.Build();
            var signed = built.SignWithAsync(account);
            var submitted = await signed.Submit(algod);

            return await submitted.Confirm();
        }

        private async UniTask<TransactionParams> GetSuggestedParams()
        {
            var (error, txnParams) = await algod.TransactionParams();
            error.ThrowIfError();
            return txnParams;
        }
    }
}
