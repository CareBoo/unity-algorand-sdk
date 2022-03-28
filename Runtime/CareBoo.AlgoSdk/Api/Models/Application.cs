using System;
using System.Text;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Application index and its parameters
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct Application
        : IEquatable<Application>
    {
        /// <summary>
        /// Bytes prefix when hashing this struct
        /// </summary>
        public static readonly byte[] HashPrefix = Encoding.UTF8.GetBytes("appID");

        /// <summary>
        /// Round when this application was created.
        /// </summary>
        [AlgoApiField("created-at-round")]
        [Tooltip("Round when this application was created.")]
        public ulong CreatedAtRound;

        /// <summary>
        /// Whether or not this application is currently deleted.
        /// </summary>
        [AlgoApiField("deleted")]
        [Tooltip("Whether or not this application is currently deleted.")]
        public Optional<bool> Deleted;

        /// <summary>
        /// Round when this application was deleted.
        /// </summary>
        [AlgoApiField("deleted-at-round")]
        [Tooltip("Round when this application was deleted.")]
        public ulong DeletedAtRound;

        /// <summary>
        /// [appidx] application index.
        /// </summary>
        [AlgoApiField("id")]
        [Tooltip("[appidx] application index.")]
        public ulong Id;

        /// <summary>
        /// [appparams] application parameters.
        /// </summary>
        [AlgoApiField("params")]
        [Tooltip("[appparams] application parameters.")]
        public ApplicationParams Params;

        public bool Equals(Application other)
        {
            return Id.Equals(other.Id);
        }

        /// <summary>
        /// Computes the <see cref="Address"/> from the <see cref="Application.Id"/> for this application.
        /// </summary>
        /// <returns><see cref="Optional{Address}.Empty"/> if the <see cref="Application.Id"/> is empty, otherwise an <see cref="Address"/>.</returns>
        public Optional<Address> GetAddress() =>
             Id > 0
                ? ComputeAddressFromId(Id)
                : default
                ;

        /// <summary>
        /// Computes the <see cref="Address"/> from an <see cref="Application.Id"/>.
        /// </summary>
        /// <param name="appIndex">An application id.</param>
        /// <returns>An <see cref="Address"/>.</returns>
        public static Address ComputeAddressFromId(ulong appIndex)
        {
            using var appIndexBytes = appIndex.ToBytesBigEndian(Allocator.Temp);
            var data = new NativeByteArray(HashPrefix.Length + appIndexBytes.Length, Allocator.Temp);
            try
            {
                data.CopyFrom(HashPrefix, 0);
                data.CopyFrom(appIndexBytes, HashPrefix.Length);
                var hash = Sha512.Hash256Truncated(data);
                return UnsafeUtility.As<Sha512_256_Hash, Address>(ref hash);
            }
            finally
            {
                data.Dispose();
            }
        }
    }
}
