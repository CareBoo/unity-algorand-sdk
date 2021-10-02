using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ErrorResponse
        : IEquatable<ErrorResponse>
    {
        [AlgoApiField("data", null)]
        public string Data;

        [AlgoApiField("message", null)]
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
