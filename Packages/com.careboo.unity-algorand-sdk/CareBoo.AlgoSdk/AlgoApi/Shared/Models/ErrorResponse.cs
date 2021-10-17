using System;
using AlgoSdk.Formatters;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(ErrorResponseFormatter))]
    public struct ErrorResponse
        : IEquatable<ErrorResponse>
    {
        public string Data;

        public string Message;

        public ErrorResponse(string message, string data)
        {
            Message = message;
            Data = data;
        }

        public ErrorResponse(string message)
            : this(message, null)
        {
        }

        public bool Equals(ErrorResponse other)
        {
            return StringComparer.Equals(Data, other.Data)
                && StringComparer.Equals(Message, other.Message)
                ;
        }
    }
}
