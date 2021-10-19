using System;
using System.Text;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Application
        : IEquatable<Application>
    {
        public static readonly byte[] HashPrefix = Encoding.UTF8.GetBytes("appID");

        [AlgoApiField("created-at-round", null)]
        public ulong CreatedAtRound;

        [AlgoApiField("deleted", null)]
        public Optional<bool> Deleted;

        [AlgoApiField("deleted-at-round", null)]
        public ulong DeletedAtRound;

        [AlgoApiField("id", null)]
        public ulong Id;

        [AlgoApiField("params", null)]
        public ApplicationParams Params;

        public bool Equals(Application other)
        {
            return Id.Equals(other.Id);
        }

        public Optional<Address> GetAddress() =>
             Id > 0
                ? ComputeAddressFromId(Id)
                : default
                ;

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
