using AlgoSdk;
using AlgoSdk.Algod;
using Algorand.Algod;
using System.Net.Http;
using Algorand;
using System;
using System.Collections.Generic;
using System.IO;

public class DotnetAlgodClient
    : IAlgodClient
{
    readonly string address;
    readonly string token;
    readonly Header[] headers;

    public string Address => address;

    public string Token => token;

    public string TokenHeader => "X-Algo-API-Token";

    public Header[] Headers => headers;

    public DotnetAlgodClient(string address, string token, params Header[] headers)
    {
        this.address = address.TrimEnd('/') + '/';
        this.token = token;
        this.headers = headers;
    }

    public DotnetAlgodClient(string address, params Header[] headers)
        : this(address, null, headers)
    {
    }

    public AlgoApiRequest.Sent<CatchpointAbortResponse> AbortCatchup(string catchpoint)
    {
        throw new System.NotImplementedException();
    }

    public AlgoApiRequest.Sent<AccountApplicationResponse> AccountApplicationInformation(string address, ulong applicationId, ResponseFormat format = ResponseFormat.None)
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.AccountApplicationInformationAsync(address, applicationId, format.ToDotNetFormat());
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<AccountAssetResponse> AccountAssetInformation(string address, ulong assetId, ResponseFormat format = ResponseFormat.None)
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.AccountAssetInformationAsync(address, assetId, format.ToDotNetFormat());
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<AccountResponse> AccountInformation(string address, ExcludeFields exclude = ExcludeFields.Unknown, ResponseFormat format = ResponseFormat.None)
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.AccountInformationAsync(address, exclude.ToString(), format.ToDotNetFormat());
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<PostParticipationResponse> AddParticipationKey(byte[] participationkey)
    {
        throw new NotImplementedException();
    }

    public AlgoApiRequest.Sent<ParticipationKeyResponse> AppendKeys(byte[] keymap, string participationId)
    {
        throw new NotImplementedException();
    }

    public AlgoApiRequest.Sent DeleteParticipationKeyByID(string participationId)
    {
        throw new NotImplementedException();
    }

    public AlgoApiRequest.Sent<ApplicationResponse> GetApplicationByID(ulong applicationId)
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.GetApplicationByIDAsync(applicationId);
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<AssetResponse> GetAssetByID(ulong assetId)
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.GetAssetByIDAsync(assetId);
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<AlgoSdk.BlockResponse> GetBlock(ulong round, ResponseFormat format = ResponseFormat.None)
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.GetBlockAsync(round, format.ToDotNetFormat());
        oneTimeClient.DisposeOnFinish().Forget();
        UnityEngine.Debug.Log("GetBlock");
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<AlgoApiObject> GetGenesis()
    {
        throw new NotImplementedException();
    }

    public AlgoApiRequest.Sent<ParticipationKeyResponse> GetParticipationKeyByID(string participationId)
    {
        throw new NotImplementedException();
    }

    public AlgoApiRequest.Sent<ParticipationKeysResponse> GetParticipationKeys()
    {
        throw new NotImplementedException();
    }

    public AlgoApiRequest.Sent<PendingTransactionsResponse> GetPendingTransactions(Optional<ulong> max = default, ResponseFormat format = ResponseFormat.None)
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.GetPendingTransactionsAsync(format.ToDotNetFormat(), max.HasValue ? max.Value : null);
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<PendingTransactionsResponse> GetPendingTransactionsByAddress(string address, Optional<ulong> max = default, ResponseFormat format = ResponseFormat.None)
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.GetPendingTransactionsByAddressAsync(address, format.ToDotNetFormat(), max.HasValue ? max.Value : null);
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<NodeStatusResponse> GetStatus()
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.GetStatusAsync();
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<SupplyResponse> GetSupply()
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.GetSupplyAsync();
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<VersionsResponse> GetVersion()
    {
        throw new System.NotImplementedException();
    }

    public AlgoApiRequest.Sent HealthCheck()
    {
        throw new System.NotImplementedException();
    }

    public AlgoApiRequest.Sent Metrics()
    {
        throw new System.NotImplementedException();
    }

    public AlgoApiRequest.Sent<PendingTransactionResponse> PendingTransactionInformation(string txid, ResponseFormat format = ResponseFormat.None)
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.PendingTransactionInformationAsync(txid, format.ToDotNetFormat());
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<PostTransactionsResponse> RawTransaction(byte[] rawtxn)
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.TransactionsAsync(
            Algorand.Utils.Encoder.DecodeFromMsgPack<List<Algorand.Algod.Model.Transactions.SignedTransaction>>(rawtxn)
            );
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<AlgoApiObject> ShutdownNode(Optional<ulong> timeout = default)
    {
        throw new System.NotImplementedException();
    }

    public AlgoApiRequest.Sent<CatchpointStartResponse> StartCatchup(string catchpoint)
    {
        throw new System.NotImplementedException();
    }

    public AlgoApiRequest.Sent<AlgoApiObject> SwaggerJSON()
    {
        throw new System.NotImplementedException();
    }

    public AlgoApiRequest.Sent<CompileResponse> TealCompile(byte[] source, Optional<bool> sourcemap = default)
    {
        if (sourcemap.HasValue)
            throw new NotImplementedException();
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.TealCompileAsync(new MemoryStream(source));
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<DisassembleResponse> TealDisassemble(byte[] source)
    {
        throw new System.NotImplementedException();
    }

    public AlgoApiRequest.Sent<DryrunResponse> TealDryrun(DryrunRequest request = default)
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.TealDryrunAsync(request.Convert().To<Algorand.Algod.Model.DryrunRequest>());
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<TransactionParametersResponse> TransactionParams()
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.TransactionParamsAsync();
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<NodeStatusResponse> WaitForBlock(ulong round)
    {
        var oneTimeClient = new AlgodApiClient(address, token, headers);
        oneTimeClient.Api.WaitForBlockAsync(round);
        oneTimeClient.DisposeOnFinish().Forget();
        return new AlgoApiRequest.Sent(oneTimeClient.SentWebRequest);
    }

    public AlgoApiRequest.Sent<TransactionProofResponse> GetTransactionProof(ulong round, string txid, string hashtype = null, ResponseFormat format = ResponseFormat.None)
    {
        throw new NotImplementedException();
    }

    public AlgoApiRequest.Sent<LightBlockHeaderProofResponse> GetLightBlockHeaderProof(ulong round)
    {
        throw new NotImplementedException();
    }

    public AlgoApiRequest.Sent<StateProofResponse> GetStateProof(ulong round)
    {
        throw new NotImplementedException();
    }

    public AlgoApiRequest.Sent<BlockHashResponse> GetBlockHash(ulong round)
    {
        throw new NotImplementedException();
    }
}
