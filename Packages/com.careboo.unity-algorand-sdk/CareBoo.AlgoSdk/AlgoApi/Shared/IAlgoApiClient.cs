using System;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IAlgoApiClient
    {
        string Address { get; }
        string Token { get; }
    }

    public static class AlgoApiClientExtensions
    {
        public static async UniTask<AlgoApiResponse> GetAsync<T>(this T client, string endpoint)
            where T : IAlgoApiClient
        {
            return await AlgoApiRequest.Get(client.Token, client.GetUrl(endpoint)).Send();
        }

        public static async UniTask<AlgoApiResponse> PostAsync<T, TBody>(this T client, string endpoint, TBody body)
            where T : IAlgoApiClient
            where TBody : struct, IEquatable<TBody>
        {
            using var json = AlgoApiSerializer.SerializeJson(body, Allocator.Persistent);
            return await AlgoApiRequest.Post(client.Token, client.GetUrl(endpoint), json).Send();
        }

        public static async UniTask<AlgoApiResponse> PostAsync<T>(this T client, string endpoint)
                where T : IAlgoApiClient
        {
            return await AlgoApiRequest.Post(client.Token, client.GetUrl(endpoint)).Send();
        }

        public static async UniTask<AlgoApiResponse> PostAsync<T>(this T client, string endpoint, NativeArray<byte>.ReadOnly data)
            where T : IAlgoApiClient
        {
            return await client.PostAsync(endpoint, data.ToArray());
        }

        public static async UniTask<AlgoApiResponse> PostAsync<T>(this T client, string endpoint, byte[] data)
            where T : IAlgoApiClient
        {
            return await AlgoApiRequest.Post(client.Token, client.GetUrl(endpoint), data).Send();
        }

        public static async UniTask<AlgoApiResponse> PostAsync<T>(this T client, string endpoint, string source)
            where T : IAlgoApiClient
        {
            return await AlgoApiRequest.Post(client.Token, client.GetUrl(endpoint), source).Send();
        }

        public static async UniTask<AlgoApiResponse> DeleteAsync<T>(this T client, string endpoint)
            where T : IAlgoApiClient
        {
            return await AlgoApiRequest.Delete(client.Token, client.GetUrl(endpoint)).Send();
        }

        public static async UniTask<AlgoApiResponse> DeleteAsync<T, TBody>(this T client, string endpoint, TBody body)
            where T : IAlgoApiClient
            where TBody : struct, IEquatable<TBody>
        {
            using var json = AlgoApiSerializer.SerializeJson(body, Allocator.Persistent);
            return await AlgoApiRequest.Delete(client.Token, client.GetUrl(endpoint), json).Send();
        }

        public static string GetUrl<T>(this T client, string endpoint)
            where T : IAlgoApiClient
        {
            return client.Address + endpoint;
        }
    }
}
