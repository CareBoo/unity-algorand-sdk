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

        public bool IsError => Code >= 400;
    }
}
