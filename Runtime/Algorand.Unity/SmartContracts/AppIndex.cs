using System;
using System.Text;
using Algorand.Unity.Crypto;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// Identifier of an Application
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(WrappedValueFormatter<AppIndex, ulong>))]
    public partial struct AppIndex
        : IEquatable<AppIndex>
        , IEquatable<ulong>
        , IWrappedValue<ulong>
    {
        /// <summary>
        /// Bytes prefix when hashing this struct
        /// </summary>
        public static readonly byte[] HashPrefix = Encoding.UTF8.GetBytes("appID");

        [SerializeField] private ulong index;

        public ulong Index
        {
            get => index;
            set => index = value;
        }

        public AppIndex(ulong index)
        {
            this.index = index;
        }

        ulong IWrappedValue<ulong>.WrappedValue { get => Index; set => Index = value; }

        public bool Equals(AppIndex other)
        {
            return this == other;
        }

        public bool Equals(ulong other)
        {
            return this == other;
        }

        /// <summary>
        /// Return the escrow address of an application.
        /// </summary>
        /// <returns>The address corresponding to that application's escrow account.</returns>
        public Address GetAppAddress()
        {
            using var appIndexBytes = index.ToBytesBigEndian(Allocator.Temp);
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

        public static implicit operator ulong(AppIndex appIndex) => appIndex.Index;

        public static implicit operator AppIndex(ulong index) => new AppIndex(index);
    }
}
