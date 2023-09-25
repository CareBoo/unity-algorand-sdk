using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Algorand.Unity.Crypto
{
    public enum PasswordStorageError
    {
        Success = 0,
        OutOfMemory = -1
    }

    public partial struct PwHash
    {
        public PasswordStorageError HashStore(SodiumString password)
        {
            unsafe
            {
                return (PasswordStorageError)sodium.crypto_pwhash_str(
                    GetUnsafePtr(),
                    password.GetUnsafePtr(),
                    (ulong)password.Length,
                    (ulong)opsLimit,
                    unchecked((UIntPtr)memLimit)
                );
            }
        }

        public static JobHandle HashStore(
            SodiumString password,
            NativeReference<PwHash> hash,
            JobHandle inputDeps = default
        )
        {
            var job = new HashStoreJob
            {
                Password = password,
                Hash = hash
            };
            return job.Schedule(inputDeps);
        }

        [BurstCompile]
        public struct HashStoreJob : IJob
        {
            public SodiumString Password;
            public NativeReference<PwHash> Hash;

            public void Execute()
            {
                var value = Hash.Value;
                var error = value.HashStore(Password);
                Hash.Value = error == PasswordStorageError.Success ? value : default;
            }
        }
    }
}
