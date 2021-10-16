using System.Linq;
using AlgoSdk;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public abstract class AlgodClientTestFixture : AlgoApiClientTestFixture
{
    protected override AlgoServices RequiresServices => AlgoServices.Algod;

    protected override async UniTask SetUpAsync()
    {
        await CheckServices();
    }

    protected override async UniTask TearDownAsync()
    {
        var response = await algod.GetPendingTransactions();
        while (response.Payload.TotalTransactions > 0)
        {
            await UniTask.Delay(100);
            response = await algod.GetPendingTransactions();
        }
    }

    protected static async UniTask<Address[]> GetAddresses()
    {
        var genesisResponse = await algod.GetGenesisInformation();
        var genesisInfo = JsonUtility.FromJson<GenesisInformation>(genesisResponse.Payload.Json);
        return genesisInfo.alloc
            .Where(a => a.comment.Contains("Wallet"))
            .Select(a => (Address)a.addr)
            .ToArray();
    }
}
