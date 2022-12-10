using System;

namespace Algorand.Unity.Net
{
    public static class AlgoApiClientExtensions
    {
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

                headers = new (string headers, string value)[1];
                headers[0] = (client.TokenHeader, client.Token);
                return new UnityHttpClient(address, timeout, headers);
            }

            if (string.IsNullOrEmpty(client.Token))
            {
                headers = new (string headers, string value)[client.Headers.Length];
                for (var i = 0; i < headers.Length; i++)
                    headers[i] = client.Headers[i];
                return new UnityHttpClient(address, timeout, headers);
            }

            headers = new (string headers, string value)[client.Headers.Length + 1];
            for (var i = 0; i < client.Headers.Length; i++)
                headers[i] = client.Headers[i];
            headers[client.Headers.Length] = (client.TokenHeader, client.Token);
            return new UnityHttpClient(address, timeout, headers);
        }
    }
}
