using System;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// Information regarding an Algorand account at a given round.
    /// </summary>
    [AlgoApiObject]
    public struct Account
        : IEquatable<Account>
    {
        /// <summary>
        /// The public key of the account.
        /// </summary>
        [AlgoApiField("address", null)]
        public Address Address;

        /// <summary>
        /// The amount of microalgos belonging to this account including pending rewards.
        /// </summary>
        [AlgoApiField("amount", null)]
        public ulong Amount;

        /// <summary>
        /// The amount of microalgos belonging to this account not including pending rewards.
        /// </summary>
        [AlgoApiField("amount-without-pending-rewards", null)]
        public ulong AmountWithoutPendingRewards;

        /// <summary>
        /// Local application data stored in this account.
        /// </summary>
        [AlgoApiField("apps-local-state", null)]
        public ApplicationLocalState[] ApplicationsLocalState;

        /// <summary>
        /// The total number of extra program pages for this account.
        /// </summary>
        [AlgoApiField("apps-total-extra-pages", null)]
        public ulong ApplicationsTotalExtraPages;

        /// <summary>
        /// The sum of all local and global schemas in this account.
        /// </summary>
        [AlgoApiField("apps-total-schema", null)]
        public StateSchema ApplicationsTotalSchema;

        /// <summary>
        /// Assets held by this account.
        /// </summary>
        [AlgoApiField("assets", null)]
        public AssetHolding[] Assets;

        /// <summary>
        /// The address against which signing should be checked.
        /// If empty, the address of the current account is used.
        /// This field can be updated in any transaction by setting the RekeyTo field.
        /// </summary>
        [AlgoApiField("auth-addr", null)]
        public Address AuthAddress;

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField("closed-at-round", null, readOnly: true)]
        public ulong ClosedAtRound;

        /// <summary>
        /// Parameters of applications created by this account including app global data.
        /// </summary>
        [AlgoApiField("created-apps", null)]
        public Application[] CreatedApplications;

        /// <summary>
        /// Parameters of assets created by this account.
        /// </summary>
        [AlgoApiField("created-assets", null)]
        public Asset[] CreatedAssets;

        [AlgoApiField("created-at-round", null, readOnly: true)]
        public ulong CreatedAtRound;

        [AlgoApiField("deleted", null, readOnly: true)]
        public Optional<bool> Deleted;

        [AlgoApiField("participation", null)]
        public AccountParticipation Participation;

        /// <summary>
        /// Amount of MicroAlgos of pending rewards in this account.
        /// </summary>
        [AlgoApiField("pending-rewards", null)]
        public ulong PendingRewards;

        /// <summary>
        /// Used as part of the rewards computation. Only applicable to accounts which are participating.
        /// </summary>
        [AlgoApiField("reward-base", null)]
        public ulong RewardBase;

        /// <summary>
        /// Total rewards of MicroAlgos the account has received, including pending rewards.
        /// </summary>
        [AlgoApiField("rewards", null)]
        public ulong Rewards;

        /// <summary>
        /// The round for which this information is relevant.
        /// </summary>
        [AlgoApiField("round", null)]
        public ulong Round;

        /// <summary>
        /// Indicates what type of signature is used by this account, must be one of:
        /// <list type="bullet">
        /// <item>sig</item>
        /// <item>msig</item>
        /// <item>lsig</item>
        /// </list>
        /// </summary>
        [AlgoApiField("sig-type", null)]
        public SignatureType SignatureType;

        /// <summary>
        /// Delegation status of the account's MicroAlgos
        /// <list type="table">
        /// <item>
        /// <term>Offline</term>
        /// <description>indicates that the associated account is delegated.</description>
        /// </item>
        /// <item>
        /// <term>Online</term>
        /// <description>indicates that the associated account used as part of the delegation pool.</description>
        /// </item>
        /// <item>
        /// <term>NotParticipating</term>
        /// <description>indicates that the associated account is neither a delegator nor a delegate.</description>
        /// </item>
        /// </list>
        /// </summary>
        [AlgoApiField("status", null)]
        public FixedString32Bytes Status;

        public bool Equals(Account other)
        {
            return Address.Equals(other.Address);
        }
    }
}
