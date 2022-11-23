using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Jobs;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
	/// Stores and encodes references to foreign accounts, apps, and assets when generating
    /// an ABI Method Call.
	/// </summary>
    public struct AbiReferences
        : INativeDisposable
    {
        private NativeOrderedSet<Address> accounts;
        private NativeOrderedSet<ulong> apps;
        private NativeOrderedSet<ulong> assets;

        public AbiReferences(Allocator allocator)
        {
            accounts = new NativeOrderedSet<Address>(1, allocator);
            accounts.Add(default);
            apps = new NativeOrderedSet<ulong>(1, allocator);
            apps.Add(default);
            assets = new NativeOrderedSet<ulong>(0, allocator);
        }

        public AbiReferences(Address sender, AppIndex appId, Allocator allocator)
        {
            accounts = new NativeOrderedSet<Address>(1, allocator);
            accounts.Add(sender);
            apps = new NativeOrderedSet<ulong>(1, allocator);
            apps.Add(appId);
            assets = new NativeOrderedSet<ulong>(0, allocator);
        }

        /// <summary>
        /// Get the index of the given asset or application and store it within this struct.
        /// </summary>
        /// <param name="value">The index of an asset or application.</param>
        /// <param name="referenceType">Is this an asset or application reference?</param>
        /// <returns>The index of the given asset or application.</returns>
        /// <exception cref="System.NotSupportedException">The given referenceType is not supported.</exception>
        public byte Encode(ulong value, AbiReferenceType referenceType)
        {
            switch (referenceType)
            {
                case AbiReferenceType.Asset:
                    return (byte)assets.AddIndexOf(value);
                case AbiReferenceType.Application:
                    return (byte)apps.AddIndexOf(value);
                default:
                    throw new System.NotSupportedException();
            }
        }

        /// <summary>
        /// Get the index of the given account and store it within this struct.
        /// </summary>
        /// <param name="value">The address of the account</param>
        /// <returns>An index of the account in the accounts section of the AppCallTxn.</returns>
        public byte Encode(Address value)
        {
            return (byte)accounts.AddIndexOf(value);
        }

        /// <summary>
        /// Get an array of the referenced accounts to be used as the <see cref="AppCallTxn.Accounts"/>.
        /// </summary>
        /// <returns>An array of account addresses.</returns>
        public Address[] GetForeignAccounts()
        {
            var result = accounts.Slice(1);
            return result.Count > 0 ? result.ToArray() : null;
        }

        /// <summary>
        /// Get an array of the referenced apps to be used as the <see cref="AppCallTxn.ForeignApps"/>.
        /// </summary>
        /// <returns>An array of app indices.</returns>
        public ulong[] GetForeignApps()
        {
            var result = apps.Slice(1);
            return result.Count > 0 ? result.ToArray() : null;
        }

        /// <summary>
        /// Get an array of the referenced assets to be used as the <see cref="AppCallTxn.ForeignAssets"/>.
        /// </summary>
        /// <returns>An array of asset indices.</returns>
        public ulong[] GetForeignAssets()
        {
            var result = assets.Slice(0);
            return result.Count > 0 ? result.ToArray() : null;
        }

        /// <inheritdoc />
        public JobHandle Dispose(JobHandle inputDeps)
        {
            return JobHandle.CombineDependencies(
                accounts.Dispose(inputDeps),
                apps.Dispose(inputDeps),
                assets.Dispose(inputDeps)
                );
        }

        /// <inheritdoc />
        public void Dispose()
        {
            accounts.Dispose();
            apps.Dispose();
            assets.Dispose();
        }
    }
}
