using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace CareBoo.Algorand.SDK.Unity.Tests
{
    class RuntimeExampleTest
    {

        [Test]
        public void PlayModeSampleTestSimplePasses()
        {
            var expected = 4;
            var actual = 2 + 2;
            Assert.AreEqual(expected, actual);
        }

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityTest]
        public IEnumerator PlayModeSampleTestWithEnumeratorPasses()
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            var rb = go.AddComponent<Rigidbody>();
            yield return new WaitForSeconds(0.5f);
            Assert.Greater(rb.velocity.sqrMagnitude, 0f);
        }
    }
}
