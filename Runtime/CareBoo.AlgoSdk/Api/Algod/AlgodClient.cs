using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// A client for accessing the algod service
    /// </summary>
    /// <remarks>
    /// The algod service is responsible for handling information
    /// required to create and send transactions.
    /// </remarks>
    [Serializable]
    public partial struct AlgodClient : IAlgodClient
    {
        [SerializeField]
        string address;

        [SerializeField]
        string token;

        [SerializeField]
        Header[] headers;

        /// <summary>
        /// Create a new algod client with a token set to <see cref="TokenHeader"/>.
        /// </summary>
        /// <param name="address">url of the algod service, including the port, e.g. <c>"http://localhost:4001"</c></param>
        /// <param name="token">token used in authenticating to the algod service</param>
        /// <param name="headers">extra headers to add to the requests. e.g. <c>("x-api-key, my-api-key")</c></param>
        public AlgodClient(string address, string token, params Header[] headers)
        {
            this.address = address.TrimEnd('/');
            this.token = token;
            this.headers = headers;
        }

        /// <summary>
        /// Create a new algod client without using a token.
        /// </summary>
        /// <param name="address">url of the algod service, including the port, e.g. <c>"http://localhost:4001"</c></param>
        /// <param name="headers">extra headers to add to the requests. e.g. <c>("x-api-key, my-api-key")</c></param>
        public AlgodClient(string address, params Header[] headers) : this(address, null, headers)
        {
        }

        public string Address => address;

        public string Token => token;

        public string TokenHeader => "X-Algo-API-Token";

        public Header[] Headers => headers;
    }
}
