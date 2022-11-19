using System;
using Algorand.Unity;

namespace AlgoSdk
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

        public static UnityHttpClient ToHttpClient<T>(this T client)
            where T : IAlgoApiClient
        {
            var timeout = System.Threading.Timeout.InfiniteTimeSpan;
            return client.ToHttpClient(timeout);
        }

        public static UnityHttpClient ToHttpClient<T>(this T client, TimeSpan timeout)
            where T : IAlgoApiClient
        {
            var address = client.Address;
            (string headers, string value)[] headers;
            if (client.Headers == null)
            {
                if (string.IsNullOrEmpty(client.Token))
                {
                    return new UnityHttpClient(address, timeout);
                }
                else
                {
                    headers = new (string headers, string value)[1];
                    headers[0] = (client.TokenHeader, client.Token);
                    return new UnityHttpClient(address, timeout, headers);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(client.Token))
                {
                    headers = new (string headers, string value)[client.Headers.Length];
                    for (var i = 0; i < headers.Length; i++)
                        headers[i] = client.Headers[i];
                    return new UnityHttpClient(address, timeout, headers);
                }
                else
                {
                    headers = new (string headers, string value)[client.Headers.Length + 1];
                    for (var i = 0; i < client.Headers.Length; i++)
                        headers[i] = client.Headers[i];
                    headers[client.Headers.Length] = (client.TokenHeader, client.Token);
                    return new UnityHttpClient(address, timeout, headers);
                }
            }
        }
    }
}
