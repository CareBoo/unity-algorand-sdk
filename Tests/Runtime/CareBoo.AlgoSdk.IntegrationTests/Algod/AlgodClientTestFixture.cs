using System.Linq;
using System.Text;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public abstract class AlgodClientTestFixture : KmdClientTestFixture
{
    protected override AlgoServices RequiresServices => AlgoServices.Algod | base.RequiresServices;

    protected override async UniTask TearDownAsync()
    {
        var response = await AlgoApiClientSettings.Algod.GetPendingTransactionsByAddress(PublicKey);
        while (response.Payload.TopTransactions != null && response.Payload.TopTransactions.Length > 0)
        {
            Debug.Log($"Waiting for pending transactions:\n{JsonUtility.ToJson(response.Payload, true)}");
            await UniTask.Delay(100);
            response = await AlgoApiClientSettings.Algod.GetPendingTransactionsByAddress(PublicKey);
        }
        await base.TearDownAsync();
    }

    protected static async UniTask<Address[]> GetAddresses()
    {
        var genesisResponse = await AlgoApiClientSettings.Algod.GetGenesis();
        var genesisInfo = JsonUtility.FromJson<GenesisInformation>(genesisResponse.Payload);
        return genesisInfo.alloc
            .Where(a => a.comment.Contains("Wallet"))
            .Select(a => (Address)a.addr)
            .ToArray();
    }
}
