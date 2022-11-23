namespace Algorand.Unity
{
    /// <summary>
    /// An Algorand network. See <see href="https://developer.algorand.org/docs/get-details/algorand-networks/">this overview</see> on Algorand's networks.
    /// </summary>
    public enum AlgorandNetwork
    {
        /// <summary>
        /// No network selected
        /// </summary>
        None,

        /// <summary>
        /// The Algorand <see href="https://developer.algorand.org/docs/get-details/algorand-networks/testnet/">TestNet</see>
        /// </summary>
        TestNet,

        /// <summary>
        /// The Algorand <see href="https://developer.algorand.org/docs/get-details/algorand-networks/betanet/">BetaNet</see>
        /// </summary>
        BetaNet,

        /// <summary>
        /// The Algorand <see href="https://developer.algorand.org/docs/get-details/algorand-networks/mainnet/">MainNet</see>
        /// </summary>
        MainNet
    }
}
