using System.Collections;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine.TestTools;

namespace Algorand.Unity.Crypto.Tests
{
    public class PwHashTest
    {
        [UnityTest]
        public IEnumerator PwHashStoreJobThenVerifySucceeds()
        {
            using var password = new SecureString("password");
            var hash = new PwHash(PwHash.OpsLimit.Interactive, PwHash.MemLimit.Interactive);
            using var hashRef = new NativeReference<PwHash>(hash, Allocator.Persistent);
            var hashJob = PwHash.HashStore(password, hashRef);
            while (!hashJob.IsCompleted) yield return null;
            hashJob.Complete();

            var error = hashRef.Value.Verify(password);
            Assert.AreEqual(PasswordVerificationError.Success, error);
        }

        [Test]
        public void PwHashStoreThenVerifySucceeds()
        {
            using var password = new SecureString("password");
            var hash = new PwHash(PwHash.OpsLimit.Interactive, PwHash.MemLimit.Interactive);
            var storeError = hash.HashStore(password);
            Assert.AreEqual(PasswordStorageError.Success, storeError);
            var verifyError = hash.Verify(password);
            Assert.AreEqual(PasswordVerificationError.Success, verifyError);
        }

        [UnityTest]
        public IEnumerator PwHashStoreThenRehashWithNewParamsShouldReturnRequired()
        {
            using var password = new SecureString("hello world 123");
            using var hashRef = new NativeReference<PwHash>(PwHash.Interactive, Allocator.Persistent);
            var hashJob = PwHash.HashStore(password, hashRef);
            while (!hashJob.IsCompleted) yield return null;
            hashJob.Complete();

            var needsRehash = hashRef.Value.NeedsRehash(PwHash.OpsLimit.Moderate, PwHash.MemLimit.Moderate);
            Assert.AreEqual(PasswordNeedsRehashResult.NeedsRehash, needsRehash);
        }
    }
}
