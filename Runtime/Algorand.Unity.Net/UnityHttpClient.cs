using System;
using System.Net.Http;

namespace Algorand.Unity.Net
{
    public class UnityHttpClient : HttpClient
    {
        public readonly UnityHttpMessageHandler MessageHandler;

        protected UnityHttpClient(UnityHttpMessageHandler messageHandler)
            : base(messageHandler)
        {
            MessageHandler = messageHandler;
        }

        public UnityHttpClient(
            string address
            )
            : this(address, System.Threading.Timeout.InfiniteTimeSpan)
        {
        }

        public UnityHttpClient(
            string address,
            TimeSpan timeout
            )
            : this(new UnityHttpMessageHandler())
        {
            BaseAddress = FormatAddress(address);
            Timeout = timeout;
        }

        public UnityHttpClient(
            string address,
            params (string header, string value)[] headers
            )
            : this(address, System.Threading.Timeout.InfiniteTimeSpan, headers)
        {
        }

        public UnityHttpClient(
            string address,
            TimeSpan timeout,
            params (string header, string value)[] headers
            )
            : this(new UnityHttpMessageHandler())
        {
            BaseAddress = FormatAddress(address);
            Timeout = timeout;

            if (headers == null)
            {
                return;
            }

            foreach (var (key, value) in headers)
            {
                DefaultRequestHeaders.Add(key, value);
            }
        }

        private static Uri FormatAddress(string address)
        {
            if (!address.EndsWith("/"))
                address += "/";
            var addressUri = new Uri(address);
            if (!addressUri.IsAbsoluteUri)
                throw new ArgumentException("Given host must be an absolute path.", nameof(address));
            return addressUri;
        }
    }
}