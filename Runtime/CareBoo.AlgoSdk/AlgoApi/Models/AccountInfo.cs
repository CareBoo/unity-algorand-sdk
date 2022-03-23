using System;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Information regarding an Algorand account at a given round.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct AccountInfo
        : IEquatable<AccountInfo>
    {
        /// <summary>
        /// The public key of the account.
        /// </summary>
        [AlgoApiField("address", null)]
        [Tooltip("The public key of the account.")]
        public Address Address;

        /// <summary>
        /// The amount of microalgos belonging to this account including pending rewards.
        /// </summary>
        [AlgoApiField("amount", null)]
        [Tooltip("The amount of microalgos belonging to this account including pending rewards.")]
        public ulong Amount;

        /// <summary>
        /// The amount of microalgos belonging to this account not including pending rewards.
        /// </summary>
        [AlgoApiField("amount-without-pending-rewards", null)]
        [Tooltip("The amount of microalgos belonging to this account not including pending rewards.")]
        public ulong AmountWithoutPendingRewards;

        /// <summary>
        /// Local application data stored in this account.
        /// </summary>
        [AlgoApiField("apps-local-state", null)]
        [Tooltip("Local application data stored in this account.")]
        public ApplicationLocalState[] ApplicationsLocalState;

        /// <summary>
        /// The total number of extra program pages for this account.
        /// </summary>
        [AlgoApiField("apps-total-extra-pages", null)]
        [Tooltip("The total number of extra program pages for this account.")]
        public ulong ApplicationsTotalExtraPages;

        /// <summary>
        /// The sum of all local and global schemas in this account.
        /// </summary>
        [AlgoApiField("apps-total-schema", null)]
        [Tooltip("The sum of all local and global schemas in this account.")]
        public StateSchema ApplicationsTotalSchema;

        /// <summary>
        /// Assets held by this account.
        /// </summary>
        [AlgoApiField("assets", null)]
        [Tooltip("Assets held by this account.")]
        public AssetHolding[] Assets;

        /// <summary>
        /// The address against which signing should be checked.
        /// If empty, the address of the current account is used.
        /// This field can be updated in any transaction by setting the RekeyTo field.
        /// </summary>
        [AlgoApiField("auth-addr", null)]
        [Tooltip("The address against which signing should be checked. If empty, the address of the current account is used. This field can be updated in any transaction by setting the RekeyTo field.")]
        public Address AuthAddress;

        /// <summary>
        /// Round during which this account was most recently closed.
        /// </summary>
        [AlgoApiField("closed-at-round", null, readOnly: true)]
        [Tooltip("Round during which this account was most recently closed.")]
        public ulong ClosedAtRound;

        /// <summary>
        /// Parameters of applications created by this account including app global data.
        /// </summary>
        [AlgoApiField("created-apps", null)]
        [Tooltip("Parameters of applications created by this account including app global data.")]
        public Application[] CreatedApplications;

        /// <summary>
        /// Parameters of assets created by this account.
        /// </summary>
        [AlgoApiField("created-assets", null)]
        [Tooltip("Parameters of assets created by this account.")]
        public Asset[] CreatedAssets;

        /// <summary>
        /// Round during which this account first appeared in a transaction.
        /// </summary>
        [AlgoApiField("created-at-round", null, readOnly: true)]
        [Tooltip("Round during which this account first appeared in a transaction.")]
        public ulong CreatedAtRound;

        /// <summary>
        /// Whether or not this account is currently closed.
        /// </summary>
        [AlgoApiField("deleted", null, readOnly: true)]
        [Tooltip("Whether or not this account is currently closed.")]
        public Optional<bool> Deleted;

        /// <summary>
        /// See <see cref="AccountParticipation"/>
        /// </summary>
        [AlgoApiField("participation", null)]
        public AccountParticipation Participation;

        /// <summary>
        /// Amount of MicroAlgos of pending rewards in this account.
        /// </summary>
        [AlgoApiField("pending-rewards", null)]
        [Tooltip("Amount of MicroAlgos of pending rewards in this account.")]
        public ulong PendingRewards;

        /// <summary>
        /// Used as part of the rewards computation. Only applicable to accounts which are participating.
        /// </summary>
        [AlgoApiField("reward-base", null)]
        [Tooltip("Used as part of the rewards computation. Only applicable to accounts which are participating.")]
        public ulong RewardBase;

        /// <summary>
        /// Total rewards of MicroAlgos the account has received, including pending rewards.
        /// </summary>
        [AlgoApiField("rewards", null)]
        [Tooltip("Total rewards of MicroAlgos the account has received, including pending rewards.")]
        public ulong Rewards;

        /// <summary>
        /// The round for which this information is relevant.
        /// </summary>
        [AlgoApiField("round", null)]
        [Tooltip("The round for which this information is relevant.")]
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
        [Tooltip("Indicates what type of signature is used by this account")]
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
        [Tooltip("Delegation status of the account's MicroAlgos")]
        public FixedString32Bytes Status;

        /// <summary>
        /// MicroAlgo balance required by the account.
        /// </summary>
        /// <remarks>
        /// The requirement grows based on asset and application usage.
        /// </remarks>
        [AlgoApiField("min-balance", null)]
        [Tooltip("Minimum balance of the account in MicroAlgos")]
        public ulong MinBalance;

        /// <summary>
        /// Estimate the minimum balance of the account in MicroAlgos.
        /// </summary>
        /// <remarks>
        /// See <see cref="MinBalance"/> for more details.
        /// </remarks>
        /// <returns>The sum of all the costs per data item in the account.</returns>
        public ulong EstimateMinBalance()
        {
            return new MinBalance(this).Estimate();
        }

        public bool Equals(AccountInfo other)
        {
            return Address.Equals(other.Address);
        }
    }
}
