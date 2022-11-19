using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Algorand.Algod;
using System;
using AlgoSdk;
using Algorand;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public class AlgodApiClient : IDisposable
{
    private readonly DefaultApi api;
    private readonly HttpClient client;
    private readonly UnityHttpMessageHandler messageHandler;

    public DefaultApi Api => api;
    public UnityWebRequestAsyncOperation SentWebRequest => messageHandler.LastAsyncOperation;

    public AlgodApiClient(
        string address,
        string token,
        params Header[] headers)
    {
        messageHandler = new UnityHttpMessageHandler();
        client = HttpClientConfigurator.ConfigureHttpClient(address, token, tokenHeader: "X-Algo-API-Token", shim: messageHandler);
        foreach (var (key, value) in headers)
        {
            client.DefaultRequestHeaders.Add(key, value);
        }
        api = new DefaultApi(client);
    }

    public void Dispose()
    {
        client.Dispose();
        messageHandler.Dispose();
    }

    public async UniTaskVoid DisposeOnFinish()
    {
        while (SentWebRequest == null)
        {
            await UniTask.Yield();
        }
        await SentWebRequest;
        Dispose();
    }
}
