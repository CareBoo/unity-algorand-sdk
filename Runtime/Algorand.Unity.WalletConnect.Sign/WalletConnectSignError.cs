namespace Algorand.Unity.WalletConnect.Sign
{
    /// <summary>
    ///     Error codes for WalletConnect Sign APIs.
    ///     https://docs.walletconnect.com/2.0/specs/clients/sign/error-codes
    /// </summary>
    public enum WalletConnectSignError
    {
        None = 0,

        // INVALID
        InvalidMethod = 1001,
        InvalidEvent = 1002,
        InvalidUpdateRequest = 1003,
        InvalidExtendRequest = 1004,
        InvalidSessionSettleRequest = 1005,

        // UNAUTHORIZED
        UnauthorizedMethod = 3001,
        UnauthorizedEvent = 3002,
        UnauthorizedUpdateRequest = 3003,
        UnauthorizedExtendRequest = 3004,
        UnauthorizedChain = 3005,

        // EIP-1193
        UserRejectedRequest = 4001,

        // REJECTED (CAIP-25)
        UserRejected = 5000,
        UserRejectedChains = 5001,
        UserRejectedMethods = 5002,
        UserRejectedEvents = 5003,
        UnsupportedChains = 5100,
        UnsupportedMethods = 5101,
        UnsupportedEvents = 5102,
        UnsupportedAccounts = 5103,
        UnsupportedNamespaceKey = 5104,

        // REASON
        UserDisconnected = 6000,

        // FAILURE
        SessionSettlementFailed = 7000,
        NoSessionForTopic = 7001,

        // SESSION REQUEST
        SessionRequestExpired = 8000
    }
}