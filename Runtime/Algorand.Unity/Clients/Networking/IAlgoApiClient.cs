using System;
using Algorand.Unity;

namespace Algorand.Unity
{
    public interface IAlgoApiClient
    {
        /// <summary>
        /// Address of the service, including the port
        /// </summary>
        /// <remarks>
        /// e.g. <c>"http://localhost:4001"</c>
        /// </remarks>
        string Address { get; }

        /// <summary>
        /// Token used in authenticating to the service
        /// </summary>
        string Token { get; }

        /// <summary>
        /// Request header to use for the authenticating <see cref="Token"/>
        /// </summary>
        string TokenHeader { get; }

        /// <summary>
        /// Additional headers to add to requests
        /// </summary>
        Header[] Headers { get; }
    }

    public static class TokenizedClientExtensions
    {
        public static string GetUrl<T>(this T client, string endpoint)
            where T : IAlgoApiClient
        {
            return $"{client.Address?.TrimEnd('/')}{endpoint}";
        }

        public static AlgoApiRequest Get<T>(this T client, string endpoint)
            where T : IAlgoApiClient
        {
            return AlgoApiRequest.Get(client.GetUrl(endpoint))
                .SetToken(client.TokenHeader, client.Token)
                .SetHeaders(client.Headers)
                ;
        }

        public static AlgoApiRequest Post<T>(this T client, string endpoint)
            where T : IAlgoApiClient
        {
            return AlgoApiRequest.Post(client.GetUrl(endpoint))
                .SetToken(client.TokenHeader, client.Token)
                .SetHeaders(client.Headers)
                ;
        }

        public static AlgoApiRequest Delete<T>(this T client, string endpoint)
            where T : IAlgoApiClient
        {
            return AlgoApiRequest.Delete(client.GetUrl(endpoint))
                .SetToken(client.TokenHeader, client.Token)
                .SetHeaders(client.Headers)
                ;
        }
    }
}
