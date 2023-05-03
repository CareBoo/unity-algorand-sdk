using AlgodAccount = Algorand.Unity.Algod.Account;
using IndexerAccount = Algorand.Unity.Indexer.Account;

namespace Algorand.Unity
{
    /// <summary>
    /// The minimum balance in micro algos required for a given amount of data associated with
    /// an account.
    /// </summary>
    public readonly struct MinBalance
    {
        /// <summary>
        /// The minimum balance required per Asset created or opted in.
        /// </summary>
        public const ulong AssetCost = 100_000L;

        /// <summary>
        /// The minimum balance required per application opted in.
        /// </summary>
        public const ulong ApplicationOptedInCost = 100_000L;

        /// <summary>
        /// The minimum balance required per application created.
        /// </summary>
        public const ulong ApplicationCreatedCost = 100_000L;

        /// <summary>
        /// The minimum balance required per extra application page.
        /// </summary>
        public const ulong ApplicationExtraPageCost = 100_000L;

        /// <summary>
        /// The minimum balance required per uint in application schema.
        /// </summary>
        public const ulong UintCost = 28_500L;

        /// <summary>
        /// The minimum balance required per byteslice in application schema.
        /// </summary>
        public const ulong ByteSliceCost = 50_000L;

        /// <summary>
        /// The minimum balance required per box.
        /// </summary>
        public const ulong BoxCost = 2_500L;

        /// <summary>
        /// The minimum balance required per box byte.
        /// </summary>
        public const ulong BoxByteCost = 400L;

        /// <summary>
        /// The number of assets opted in and created.
        /// </summary>
        public readonly ulong Assets;

        /// <summary>
        /// The number of applications opted in.
        /// </summary>
        public readonly ulong ApplicationsOptedIn;

        /// <summary>
        /// The number of applications created.
        /// </summary>
        public readonly ulong ApplicationsCreated;

        /// <summary>
        /// The total number of application extra pages.
        /// </summary>
        public readonly ulong ApplicationExtraPages;

        /// <summary>
        /// The total number of uints in application schema.
        /// </summary>
        public readonly ulong ApplicationSchemaUints;

        /// <summary>
        /// The total number of byte slices in application schema.
        /// </summary>
        public readonly ulong ApplicationSchemaByteSlices;

        /// <summary>
        /// The total number of boxes associated with the account.
        /// </summary>
        public readonly ulong Boxes;

        /// <summary>
        /// The total number of bytes associated with the boxes in the account.
        /// </summary>
        public readonly ulong BoxBytes;

        /// <summary>
        /// Get the minimum balance of an account.
        /// </summary>
        /// <param name="accountInfo">The <see cref="Algorand.Unity.Algod.Account"/> to obtain the minimum balance.</param>
        public MinBalance(AlgodAccount accountInfo)
        {
            Assets = (ulong)(accountInfo.Assets?.LongLength ?? 0L);
            ApplicationsOptedIn = (ulong)(accountInfo.AppsLocalState?.LongLength ?? 0L);
            ApplicationsCreated = (ulong)(accountInfo.CreatedApps?.LongLength ?? 0L);
            ApplicationExtraPages = accountInfo.AppsTotalExtraPages;
            ApplicationSchemaUints = accountInfo.AppsTotalSchema.NumUint;
            ApplicationSchemaByteSlices = accountInfo.AppsTotalSchema.NumByteSlice;
            Boxes = accountInfo.TotalBoxes;
            BoxBytes = accountInfo.TotalBoxBytes;
        }

        /// <summary>
        /// Get the minimum balance of an account.
        /// </summary>
        /// <param name="accountInfo">The <see cref="Algorand.Unity.Indexer.Account"/> to obtain the minimum balance.</param>
        public MinBalance(IndexerAccount accountInfo)
        {
            Assets = (ulong)(accountInfo.Assets?.LongLength ?? 0L);
            ApplicationsOptedIn = (ulong)(accountInfo.AppsLocalState?.LongLength ?? 0L);
            ApplicationsCreated = (ulong)(accountInfo.CreatedApps?.LongLength ?? 0L);
            ApplicationExtraPages = accountInfo.AppsTotalExtraPages;
            ApplicationSchemaUints = accountInfo.AppsTotalSchema.NumUint;
            ApplicationSchemaByteSlices = accountInfo.AppsTotalSchema.NumByteSlice;
            Boxes = accountInfo.TotalBoxes;
            BoxBytes = accountInfo.TotalBoxBytes;
        }

        /// <summary>
        /// Get the minimum balance from explicit data.
        /// </summary>
        /// <param name="assets">The number of assets opted in and created.</param>
        /// <param name="applicationsOptedIn">The number of applications opted in.</param>
        /// <param name="applicationsCreated">The number of applications created.</param>
        /// <param name="applicationExtraPages">The total number of extra application pages.</param>
        /// <param name="applicationSchemaUints">The total number of application schema uints.</param>
        /// <param name="applicationSchemaByteSlices">The total number of application schema byte slices.</param>
        /// <param name="boxes">The total number of boxes associated with the account.</param>
        /// <param name="boxBytes">The total number of bytes associated with the boxes in the account.</param>
        public MinBalance(
            ulong assets = 0,
            ulong applicationsOptedIn = 0,
            ulong applicationsCreated = 0,
            ulong applicationExtraPages = 0,
            ulong applicationSchemaUints = 0,
            ulong applicationSchemaByteSlices = 0,
            ulong boxes = 0,
            ulong boxBytes = 0
        )
        {
            Assets = assets;
            ApplicationsOptedIn = applicationsOptedIn;
            ApplicationsCreated = applicationsCreated;
            ApplicationExtraPages = applicationExtraPages;
            ApplicationSchemaUints = applicationSchemaUints;
            ApplicationSchemaByteSlices = applicationSchemaByteSlices;
            Boxes = boxes;
            BoxBytes = boxBytes;
        }

        /// <summary>
        /// Modify this min balance with a different amount of data.
        /// </summary>
        /// <param name="assets">The number of assets opted in and created.</param>
        /// <param name="applicationsOptedIn">The number of applications opted in.</param>
        /// <param name="applicationsCreated">The number of applications created.</param>
        /// <param name="applicationExtraPages">The total number of extra application pages.</param>
        /// <param name="applicationSchemaUints">The total number of application schema uints.</param>
        /// <param name="applicationSchemaByteSlices">The total number of application schema byte slices.</param>
        /// <param name="boxes">The total number of boxes associated with the account.</param>
        /// <param name="boxBytes">The total number of bytes associated with the boxes in the account.</param>
        /// <returns>A <see cref="MinBalance"/> with given parameters replaced.</returns>
        public MinBalance With(
            Optional<ulong> assets = default,
            Optional<ulong> applicationsOptedIn = default,
            Optional<ulong> applicationsCreated = default,
            Optional<ulong> applicationExtraPages = default,
            Optional<ulong> applicationSchemaUints = default,
            Optional<ulong> applicationSchemaByteSlices = default,
            Optional<ulong> boxes = default,
            Optional<ulong> boxBytes = default
        )
        {
            return new MinBalance(
                assets.Else(Assets),
                applicationsOptedIn.Else(ApplicationsOptedIn),
                applicationsCreated.Else(ApplicationsCreated),
                applicationExtraPages.Else(ApplicationExtraPages),
                applicationSchemaUints.Else(ApplicationSchemaUints),
                applicationSchemaByteSlices.Else(ApplicationSchemaByteSlices),
                boxes.Else(Boxes),
                boxBytes.Else(BoxBytes)
            );
        }

        /// <summary>
        /// Estimate the min balance with its current parameters.
        /// </summary>
        /// <returns>An estimate of the min balance in Micro Algos.</returns>
        public ulong Estimate()
        {
            return AssetCost * Assets
             + ApplicationOptedInCost * ApplicationsOptedIn
             + ApplicationCreatedCost * ApplicationsCreated
             + ApplicationExtraPageCost * ApplicationExtraPages
             + UintCost * ApplicationSchemaUints
             + ByteSliceCost * ApplicationSchemaByteSlices
             + BoxCost * Boxes
             + BoxByteCost * BoxBytes
             ;
        }
    }
}
