using System;

namespace AlgoSdk.WalletConnect
{
    /// <summary>
    /// Error obtained from a <see cref="AlgoSignTxnsRequest"/>
    /// </summary>
    [AlgoApiObject]
    public struct SignTxnsError
        : JsonRpcError
        , IEquatable<SignTxnsError>
    {
        [AlgoApiField("message", null)]
        public string Message { get; set; }

        /// <summary>
        /// The integer error code of the error.
        /// </summary>
        [AlgoApiField("code", null)]
        public int RawErrorCode;

        /// <summary>
        /// Any other useful information about the error.
        /// </summary>
        [AlgoApiField("data", null)]
        public AlgoApiObject Data;

        /// <summary>
        /// The standard error code parsed from <see cref="RawErrorCode"/>.
        /// </summary>
        /// <returns>
        /// <see cref="RawErrorCode"/> cast to <see cref="SignTxnsErrorCode"/> if known, else <see cref="SignTxnsErrorCode.Unknown"/>.
        /// </returns>
        public SignTxnsErrorCode ErrorCode => Enum.IsDefined(typeof(SignTxnsErrorCode), RawErrorCode)
            ? (SignTxnsErrorCode)RawErrorCode
            : SignTxnsErrorCode.Unknown
            ;

        public bool Equals(SignTxnsError other)
        {
            throw new NotImplementedException();
        }
    }

    [AlgoApiFormatter(typeof(IntEnumFormatter<SignTxnsErrorCode>))]
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
