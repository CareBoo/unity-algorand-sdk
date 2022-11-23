using System;

namespace Algorand.Unity.WalletConnect
{
    /// <summary>
    /// Error obtained from a <see cref="AlgoSignTxnsRequest"/>
    /// </summary>
    [AlgoApiObject]
    public partial struct SignTxnsError
        : IJsonRpcError
        , IEquatable<SignTxnsError>
    {
        [AlgoApiField("message")]
        public string Message { get; set; }

        /// <summary>
        /// The integer error code of the error.
        /// </summary>
        [AlgoApiField("code")]
        public int Code { get; set; }

        /// <summary>
        /// Any other useful information about the error.
        /// </summary>
        [AlgoApiField("data")]
        public AlgoApiObject Data { get; set; }

        public override string ToString()
        {
            return $"{Code} {ErrorCode}: {Message}";
        }

        /// <summary>
        /// The standard error code parsed from <see cref="Code"/>.
        /// </summary>
        /// <returns>
        /// <see cref="Code"/> cast to <see cref="SignTxnsErrorCode"/> if known, else <see cref="SignTxnsErrorCode.Unknown"/>.
        /// </returns>
        public SignTxnsErrorCode ErrorCode => Enum.IsDefined(typeof(SignTxnsErrorCode), Code)
            ? (SignTxnsErrorCode)Code
            : SignTxnsErrorCode.Unknown
            ;

        public bool Equals(SignTxnsError other)
        {
            throw new NotImplementedException();
        }

        public static implicit operator bool(SignTxnsError err)
        {
            return err.Message != null;
        }

        public static implicit operator string(SignTxnsError err)
        {
            return err.ToString();
        }

        public static implicit operator SignTxnsError(JsonRpcError err)
        {
            return new SignTxnsError
            {
                Message = err.Message,
                Code = err.Code,
                Data = err.Data
            };
        }
    }

    public enum SignTxnsErrorCode
    {
        /// <summary>
        /// Represents an unknown error code.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The user rejected the request.
        /// </summary>
        UserRejectedRequest = 4001,

        /// <summary>
        /// The requested operation and/or account has not been authorized by the user.
        /// </summary>
        Unauthorized = 4000,

        /// <summary>
        /// The wallet does not support the requested operation.
        /// </summary>
        UnsupportedOperation = 4200,

        /// <summary>
        /// The wallet does not support signing that many transactions at a time.
        /// </summary>
        TooManyTransactions = 4201,

        /// <summary>
        /// The wallet was not initialized properly beforehand.
        /// </summary>
        UninitializedWallet = 4202,

        /// <summary>
        /// The input provided is invalid.
        /// </summary>
        InvalidInput = 4300
    }
}
