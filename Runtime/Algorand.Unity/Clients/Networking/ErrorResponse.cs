using System;
using Algorand.Unity.Formatters;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// An Exception thrown from an <see cref="ErrorResponse"/>.
    /// </summary>
    public class AlgoApiException : Exception
    {
        private readonly ErrorResponse error;

        public AlgoApiException(ErrorResponse error) : base(error.Message)
        {
            this.error = error;
        }

        /// <summary>
        /// The <see cref="ErrorResponse"/> that threw this exception.
        /// </summary>
        public ErrorResponse Error => error;
    }

    /// <summary>
    /// An error response from algorand APIs with optional data field.
    /// </summary>
    [AlgoApiFormatter(typeof(ErrorResponseFormatter))]
    [Serializable]
    public partial struct ErrorResponse
        : IEquatable<ErrorResponse>
    {
        public string Data;

        public string Message;

        /// <summary>
        /// HTTP response code
        /// </summary>
        [Tooltip("HTTP response code")]
        public long Code;

        public ErrorResponse WithCode(long code)
        {
            Code = code;
            return this;
        }

        public bool Equals(ErrorResponse other)
        {
            return StringComparer.Equals(Data, other.Data)
                && StringComparer.Equals(Message, other.Message)
                && Code.Equals(other.Code)
                ;
        }

        public override string ToString()
        {
            return Message;
        }

        public void ThrowIfError()
        {
            if (this)
            {
                throw new AlgoApiException(this);
            }
        }

        public bool IsError => Code >= 400 || !string.IsNullOrWhiteSpace(Message);

        public static implicit operator bool(ErrorResponse error)
        {
            return error.IsError;
        }

        public static implicit operator string(ErrorResponse error)
        {
            return error.Message;
        }
    }
}
