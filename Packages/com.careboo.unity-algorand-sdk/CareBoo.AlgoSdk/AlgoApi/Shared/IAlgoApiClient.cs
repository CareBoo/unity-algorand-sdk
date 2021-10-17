using System;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IAlgoApiClient
    {
        string Address { get; }

        string Token { get; }

        string TokenHeader { get; }
    }

    public static class TokenizedClientExtensions
    {
        public static string GetUrl<T>(this T client, string endpoint)
            where T : IAlgoApiClient
        {
            return client.Address + endpoint;
        }

        public static AlgoApiRequest Get<T>(this T client, string endpoint)
            where T : IAlgoApiClient
        {
            return AlgoApiRequest.Get(client.GetUrl(endpoint))
                .SetToken(client.TokenHeader, client.Token)
                ;
        }

        public static AlgoApiRequest Post<T>(this T client, string endpoint)
            where T : IAlgoApiClient
        {
            return AlgoApiRequest.Post(client.GetUrl(endpoint))
                .SetToken(client.TokenHeader, client.Token)
                ;
        }

        public static AlgoApiRequest Delete<T>(this T client, string endpoint)
            where T : IAlgoApiClient
        {
            return AlgoApiRequest.Delete(client.GetUrl(endpoint))
                .SetToken(client.TokenHeader, client.Token)
                ;
        }
    }
}
