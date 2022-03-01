using System;

namespace AlgoSdk.WalletConnect
{
    public interface IJsonRpcError
    {
        /// <summary>
        /// A String providing a short description of the error.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// A Number that indicates the error type that occurred.
        /// </summary>
        int Code { get; set; }

        /// <summary>
        /// A Primitive or Structured value that contains additional information about the error.
        /// </summary>
        AlgoApiObject Data { get; set; }
    }

    [AlgoApiObject]
    public partial struct JsonRpcError
        : IEquatable<JsonRpcError>
    {
        [AlgoApiField("code", null)]
        public int Code { get; set; }

        [AlgoApiField("message", null)]
        public string Message { get; set; }

        [AlgoApiField("data", null)]
        public AlgoApiObject Data { get; set; }

        public bool Equals(JsonRpcError other)
        {
            return Code == other.Code
                && StringComparer.Equals(Message, other.Message)
                && Data.Equals(other.Data)
                ;
        }
    }
}
