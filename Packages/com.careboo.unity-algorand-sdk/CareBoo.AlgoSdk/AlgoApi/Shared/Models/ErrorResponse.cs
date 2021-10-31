using System;
using AlgoSdk.Formatters;

namespace AlgoSdk
{
    /// <summary>
    /// An error response from algorand APIs with optional data field.
    /// </summary>
    [AlgoApiFormatter(typeof(ErrorResponseFormatter))]
    public struct ErrorResponse
        : IEquatable<ErrorResponse>
    {
        public string Data;

        public string Message;

        /// <summary>
        /// HTTP response code
        /// </summary>
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

        public bool IsError => Code >= 400;

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
