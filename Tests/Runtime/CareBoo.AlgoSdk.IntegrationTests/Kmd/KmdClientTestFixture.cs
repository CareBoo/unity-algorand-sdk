using System.Linq;
using System.Text;
using AlgoSdk;
using AlgoSdk.Algod;
using AlgoSdk.Kmd;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Networking;

[TestFixture]
public abstract class KmdClientTestFixture : AlgoApiClientTestFixture
{
    public const string WalletPassword = "";
    public const string WalletDriverName = "sqlite";
    public FixedString128Bytes walletHandleToken = default;
    public Wallet wallet;

    public KmdAccount kmdAccount;

    protected override AlgoServices RequiresServices => AlgoServices.Kmd;

    protected Address PublicKey { get; set; }

    protected override async UniTask SetUpAsync()
    {
        await InitWalletHandleToken();
    }

    protected override async UniTask TearDownAsync()
    {
        await ReleaseWalletHandleToken();
    }

    protected async UniTask InitWalletHandleToken()
    {
        if (walletHandleToken.Length > 0)
            return;
        var wallets = await ListWallets();
        wallet = wallets.Single();
        var initWalletHandleResponse = await AlgoApiClientSettings.Kmd.InitWalletHandleToken(wallet.Id, WalletPassword);
        walletHandleToken = initWalletHandleResponse.Payload.WalletHandleToken;
        var (keysErr, keysResponse) = await AlgoApiClientSettings.Kmd.ListKeys(walletHandleToken);
        AssertOkay(keysErr);
        PublicKey = keysResponse.Addresses.First();
        kmdAccount = new KmdAccount(AlgoApiClientSettings.Kmd, wallet.Id, WalletPassword, PublicKey, walletHandleToken);
    }

    protected async UniTask ReleaseWalletHandleToken()
    {
        if (walletHandleToken.Length > 0)
            await AlgoApiClientSettings.Kmd.ReleaseWalletHandleToken(walletHandleToken);
        walletHandleToken.Clear();
        wallet = default;
    }

    protected async UniTask<PostTransactionsResponse> MakePaymentTransaction(ulong amt)
    {
        var (error, txnParams) = await AlgoApiClientSettings.Algod.TransactionParams();
        AssertOkay(error);
        var txn = Transaction.Payment(
            sender: PublicKey,
            txnParams: txnParams,
            receiver: "RDSRVT3X6Y5POLDIN66TSTMUYIBVOMPEOCO4Y2CYACPFKDXZPDCZGVE4PQ",
            amount: amt
        );
        txn.Note = Encoding.UTF8.GetBytes("hello");
        var signedTxn = await Sign(txn);
        Debug.Log(System.Convert.ToBase64String(signedTxn));
        PostTransactionsResponse txidResponse = default;
        (error, txidResponse) = await AlgoApiClientSettings.Algod.RawTransaction(signedTxn);
        AssertOkay(error);
        return txidResponse;
    }

    protected async UniTask<byte[]> Sign<T>(T txn) where T : ITransaction
    {
        var (signError, signResponse) = await AlgoApiClientSettings.Kmd.SignTransaction(
            PublicKey,
            AlgoApiSerializer.SerializeMessagePack(txn),
            walletHandleToken,
            WalletPassword
        );
        AssertOkay(signError);
        return signResponse.SignedTransaction;
    }

    static async UniTask<Wallet[]> ListWallets()
    {
        var response = await AlgoApiClientSettings.Kmd.ListWallets();
        if (response.Status != UnityWebRequest.Result.Success)
        {
            Assert.Ignore(
                $"Ignoring test because {nameof(ListWallets)} response was {response.ResponseCode}: {response.Status}. Message:\n\"{response.Error.Message}\""
            );
        }
        return response.Payload.Wallets;
    }
}
