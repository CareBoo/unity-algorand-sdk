using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IApiClient
    {
        string Address { get; }
        string Token { get; }
    }

    public static class ApiClientExtensions
    {
        public static async UniTask<AlgoApiResponse> GetAsync<T>(this T client, string endpoint)
            where T : IApiClient
        {
            return await AlgoApiRequest.Get(client.Token, client.GetUrl(endpoint)).Send();
        }

        public static async UniTask<AlgoApiResponse> PostAsync<T>(this T client, string endpoint)
            where T : IApiClient
        {
            return await AlgoApiRequest.Post(client.Token, client.GetUrl(endpoint)).Send();
        }

        public static async UniTask<AlgoApiResponse> PostAsync<T>(this T client, string endpoint, NativeArray<byte>.ReadOnly data)
            where T : IApiClient
        {
            return await client.PostAsync(endpoint, data.ToArray());
        }

        public static async UniTask<AlgoApiResponse> PostAsync<T>(this T client, string endpoint, byte[] data)
            where T : IApiClient
        {
            return await AlgoApiRequest.Post(client.Token, client.GetUrl(endpoint), data).Send();
        }

        public static async UniTask<AlgoApiResponse> PostAsync<T>(this T client, string endpoint, string source)
            where T : IApiClient
        {
            return await AlgoApiRequest.Post(client.Token, client.GetUrl(endpoint), source).Send();
        }

        public static async UniTask<AlgoApiResponse> DeleteAsync<T>(this T client, string endpoint)
            where T : IApiClient
        {
            return await AlgoApiRequest.Delete(client.Token, client.GetUrl(endpoint)).Send();
        }

        public static string GetUrl<T>(this T client, string endpoint)
            where T : IApiClient
        {
            return client.Address + endpoint;
        }
    }
}
