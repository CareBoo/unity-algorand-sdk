using System;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IApiClient
    {
        string Address { get; }
    }

    public static class ApiClientExtensions
    {
        public static string GetUrl<T>(this T client, string endpoint)
            where T : IApiClient
        {
            return client.Address + endpoint;
        }
    }

    public interface ITokenizedApiClient : IApiClient
    {
        string Token { get; }
    }

    public static class TokenizedClientExtensions
    {
        public static async UniTask<AlgoApiResponse> GetAsync<T>(this T client, string endpoint)
            where T : ITokenizedApiClient
        {
            return await AlgoApiRequest.Get(client.GetUrl(endpoint))
                .SetToken(client.Token)
                .Send();
        }

        public static async UniTask<AlgoApiResponse> PostAsync<T, TBody>(this T client, string endpoint, TBody body)
            where T : ITokenizedApiClient
            where TBody : struct, IEquatable<TBody>
        {
            using var json = AlgoApiSerializer.SerializeJson(body, Allocator.Persistent);
            return await AlgoApiRequest.Post(client.GetUrl(endpoint), json)
                .SetToken(client.Token)
                .Send();
        }

        public static async UniTask<AlgoApiResponse> PostAsync<T>(this T client, string endpoint)
                where T : ITokenizedApiClient
        {
            return await AlgoApiRequest.Post(client.GetUrl(endpoint))
                .SetToken(client.Token)
                .Send();
        }

        public static async UniTask<AlgoApiResponse> PostAsync<T>(this T client, string endpoint, NativeArray<byte>.ReadOnly data)
            where T : ITokenizedApiClient
        {
            return await TokenizedClientExtensions.PostAsync(client, endpoint, data.ToArray());
        }

        public static async UniTask<AlgoApiResponse> PostAsync<T>(this T client, string endpoint, byte[] data)
            where T : ITokenizedApiClient
        {
            return await AlgoApiRequest.Post(client.GetUrl(endpoint), data)
                .SetToken(client.Token)
                .Send();
        }

        public static async UniTask<AlgoApiResponse> PostAsync<T>(this T client, string endpoint, string source)
            where T : ITokenizedApiClient
        {
            return await AlgoApiRequest.Post(client.GetUrl(endpoint), source)
                .SetToken(client.Token)
                .Send();
        }

        public static async UniTask<AlgoApiResponse> DeleteAsync<T>(this T client, string endpoint)
            where T : ITokenizedApiClient
        {
            return await AlgoApiRequest.Delete(client.GetUrl(endpoint))
                .SetToken(client.Token)
                .Send();
        }

        public static async UniTask<AlgoApiResponse> DeleteAsync<T, TBody>(this T client, string endpoint, TBody body)
            where T : ITokenizedApiClient
            where TBody : struct, IEquatable<TBody>
        {
            using var json = AlgoApiSerializer.SerializeJson(body, Allocator.Persistent);
            return await AlgoApiRequest.Delete(client.GetUrl(endpoint), json)
                .SetToken(client.Token)
                .Send();
        }
    }
}
