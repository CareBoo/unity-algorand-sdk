using System;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// A client for accessing the indexer service
    /// </summary>
    /// <remarks>
    /// The indexer service is responsible for querying the blockchain
    /// </remarks>
    [Serializable]
    public partial struct IndexerClient
    {
        [SerializeField] private string address;

        [SerializeField] private string token;

        [SerializeField] private Header[] headers;

        /// <summary>
        /// Create a new indexer client with a token set for <see cref="TokenHeader"/>.
        /// </summary>
        /// <param name="address">url of the service, including the port, e.g. <c>"http://localhost:4001"</c></param>
        /// <param name="token">token used in authenticating to the service</param>
        /// <param name="headers">extra headers to add to the requests. e.g. <c>("x-api-key", "my-private-key")</c></param>
        public IndexerClient(string address, string token = null, params Header[] headers)
        {
            this.address = address.TrimEnd('/');
            this.token = token;
            this.headers = headers;
        }

        /// <summary>
        /// Create a new indexer client
        /// </summary>
        /// <param name="address">url of the service, including the port, e.g. <c>"http://localhost:4001"</c></param>
        /// <param name="headers">extra headers to add to the requests. e.g. <c>("x-api-key", "my-private-key")</c></param>
        public IndexerClient(string address, params Header[] headers) : this(address, null, headers)
        {
        }

        /// <inheritdoc />
        public string Address => address;

        /// <inheritdoc />
        public string Token => token;

        /// <inheritdoc />
        public string TokenHeader => "X-Indexer-API-Token";

        /// <inheritdoc />
        public Header[] Headers => headers;

        public Algorand.Indexer.CommonApi ToCommonApi()
        {
            return (Algorand.Indexer.CommonApi)this;
        }

        public Algorand.Indexer.SearchApi ToSearchApi()
        {
            return (Algorand.Indexer.SearchApi)this;
        }

        public Algorand.Indexer.LookupApi ToLookupApi()
        {
            return (Algorand.Indexer.LookupApi)this;
        }

        public static explicit operator Algorand.Indexer.CommonApi(IndexerClient indexer)
        {
            return new Algorand.Indexer.CommonApi(indexer.ToHttpClient());
        }

        public static explicit operator Algorand.Indexer.SearchApi(IndexerClient indexer)
        {
            return new Algorand.Indexer.SearchApi(indexer.ToHttpClient());
        }

        public static explicit operator Algorand.Indexer.LookupApi(IndexerClient indexer)
        {
            return new Algorand.Indexer.LookupApi(indexer.ToHttpClient());
        }
    }
}
