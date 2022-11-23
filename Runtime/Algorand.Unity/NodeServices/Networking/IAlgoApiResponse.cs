using System.Diagnostics;
using System.Text;
using UnityEngine.Networking;
using static UnityEngine.Networking.UnityWebRequest;

namespace Algorand.Unity
{
    public interface IAlgoApiResponse
    {
        /// <summary>
        /// Raw downloaded data
        /// </summary>
        byte[] Data { get; }

        /// <summary>
        /// HTTP status code
        /// </summary>
        long ResponseCode { get; }

        /// <summary>
        /// A <see cref="Result"/> status for the request
        /// </summary>
        Result Status { get; }

        /// <summary>
        /// The <see cref="ContentType"/> found in the Response header
        /// </summary>
        ContentType ContentType { get; }

        /// <summary>
        /// An <see cref="ErrorResponse"/> that is populated if there is an error.
        /// Check <see cref="ErrorResponse.IsError"/> to see if there was an error.
        /// </summary>
        ErrorResponse Error { get; }

        /// <summary>
        /// Parses the <see cref="Data"/> into a string based on <see cref="ContentType"/>
        /// </summary>
        /// <returns>A string encoded from <see cref="Data"/></returns>
        string GetText();
    }

    public interface IAlgoApiResponse<T> : IAlgoApiResponse
    {
        /// <summary>
        /// The object deserialized from the response
        /// </summary>
        T Payload { get; }
    }
}
