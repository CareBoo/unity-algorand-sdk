using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Http;

namespace Algorand.Unity
{
    public class UnityHttpClient : HttpClient
    {
        public UnityHttpClient(
            string address
            )
            : base(new UnityHttpMessageHandler())
        {

        }

        public UnityHttpClient(
            string address,
            params (string header, string value)[] headers
            )
            : base(new UnityHttpMessageHandler())
        {
            if (!address.EndsWith('/'))
                address = address + '/';
            var addressUri = new Uri(address);
            if (!addressUri.IsAbsoluteUri)
                throw new ArgumentException("Given host must be an absolute path.", nameof(address));
            if (headers == null)
                throw new ArgumentNullException(nameof(headers));
        }
    }
}
