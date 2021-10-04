using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ErrorResponse
        : IEquatable<ErrorResponse>
    {
        [AlgoApiField("error", "error")]
        public Optional<bool> Error;

        [AlgoApiField("data", "data")]
        public string Data;

        [AlgoApiField("message", "message")]
        public string Message;

        public ErrorResponse(string message, string data)
        {
            Message = message;
            Data = data;
            Error = default;
        }

        public ErrorResponse(string message)
            : this(message, null)
        {
        }

        public bool Equals(ErrorResponse other)
        {
            return StringComparer.Equals(Data, other.Data)
                && StringComparer.Equals(Message, other.Message)
                && Error.Equals(other.Error)
                ;
        }
    }
}
